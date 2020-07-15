using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MailKit.Security;
using MimeKit;

namespace EmailSync
{
    internal class Program
    {
        internal class CONNECTION_OPTIONS
        {
            public enum TYPE
            {
                POP3,
                IMAP
            }

            public string host;
            public string password;
            public int port;
            public SecureSocketOptions ssl;

            public TYPE type;
            public string user;
        }

        public class OPTS
        {
            public CONNECTION_OPTIONS.TYPE srcType { get; set; }
            public string srcHost { get; set; }
            public int srcPort { get; set; }
            public string srcUser { get; set; }
            public string srcPass { get; set; }
            public SecureSocketOptions srcSsl { get; set; }
            public CONNECTION_OPTIONS srcOptions { get; set; }
            public CONNECTION_OPTIONS.TYPE dstType { get; set; }
            public string dstHost { get; set; }
            public int dstPort { get; set; }
            public string dstUser { get; set; }
            public string dstPass { get; set; }
            public SecureSocketOptions dstSsl { get; set; }
            public CONNECTION_OPTIONS dstOptions { get; set; }
        }

        private static int Main(string[] args)
        {
            // Create a root command with some options
            var rootCommand = new RootCommand
            {
                new Option<CONNECTION_OPTIONS.TYPE>(
                    "--src-type",
                    () => CONNECTION_OPTIONS.TYPE.IMAP,
                    "Type of source server (POP3 or IMAP)"),
                new Option<string>(
                    "--src-host",
                    "Address of host to read from"),
                new Option<int>(
                    "--src-port",
                    () => -1,
                    "Port to use on source server"),
                new Option<string>(
                    "--src-user",
                    "User on source server"),
                new Option<string>(
                    "--src-pass",
                    "password on source server"),
                new Option<SecureSocketOptions>(
                    "--src-ssl",
                    () => SecureSocketOptions.SslOnConnect,
                    "SSL for source server (None, SslOnConnect)"),
                new Option<CONNECTION_OPTIONS>(
                    "--src",
                    "Complete specification for mail server to read from"),

                new Option<CONNECTION_OPTIONS.TYPE>(
                    "--dst-type",
                    () => CONNECTION_OPTIONS.TYPE.IMAP,
                    "Type of destination server (POP3 not supported, only IMAP)"),
                new Option<string>(
                    "--dst-host",
                    "Address of host to write to"),
                new Option<int>(
                    "--dst-port",
                    () => -1,
                    "Port to use on destination server"),
                new Option<string>(
                    "--dst-user",
                    "User on destination server"),
                new Option<string>(
                    "--dst-pass",
                    "password on destination server"),
                new Option<SecureSocketOptions>(
                    "--dst-ssl",
                    () => SecureSocketOptions.SslOnConnect,
                    "SSL for destination server (None, SslOnConnect)"),
                new Option<CONNECTION_OPTIONS>(
                    "--dst",
                    "Complete specification for mail server to write to")
            };

            rootCommand.Description = "EmailSync program";


            rootCommand.Handler = CommandHandler.Create((OPTS opts) =>
            {
                sync_mailbox(
                    new CONNECTION_OPTIONS
                    {
                        type = opts.srcType,
                        host = opts.srcHost,
                        port = opts.srcPort,
                        user = opts.srcUser,
                        password = opts.srcPass,
                        ssl = opts.srcSsl
                    },
                    new CONNECTION_OPTIONS
                    {
                        type = opts.dstType,
                        host = opts.dstHost,
                        port = opts.dstPort,
                        user = opts.dstUser,
                        password = opts.dstPass,
                        ssl = opts.dstSsl
                    });
                return 0;
            });

            // Parse the incoming args and invoke the handler
            return rootCommand.InvokeAsync(args).Result;


            // test_imap();
            // test_pop3();
        }

        private static IMailSpool connect(CONNECTION_OPTIONS srcOpts)
        {
            var client = srcOpts.type == CONNECTION_OPTIONS.TYPE.POP3
                ? new Pop3Client()
                : (IMailSpool) new ImapClient();

            client.Connect(srcOpts.host, srcOpts.port, srcOpts.ssl);
            client.Authenticate(srcOpts.user, srcOpts.password);
            return client;
        }

        private static void sync_mailbox(CONNECTION_OPTIONS srcOpts, CONNECTION_OPTIONS dstOpts)
        {
            Debug.Assert(dstOpts.type == CONNECTION_OPTIONS.TYPE.IMAP,
                "Only IMAP destinations are supported at the moment");
            using (var src_client = connect(srcOpts))
            {
                var messages = new List<MimeMessage>();
                for (var i = 0; i < src_client.Count; i++)
                {
                    var message = src_client.GetMessage(i);
                    Console.WriteLine("Subject: {0}", message.Subject);
                    messages.Add(message);
                }

                using (var dst_client = (IImapClient) connect(dstOpts))
                {
                    dst_client.Connect(dstOpts.host, dstOpts.port, dstOpts.ssl);
                    dst_client.Authenticate(dstOpts.user, dstOpts.password);

                    var folders = dst_client.GetFolders(new FolderNamespace('/', ""));
                    var folder = folders.FirstOrDefault(folders => folders.FullName.Equals("INBOX"));
                    if (folder == null)
                    {
                        Console.WriteLine("No INBOX aborting");
                        return;
                    }

                    folder.Open(FolderAccess.ReadWrite);

                    Console.WriteLine($"Mailbox: {folder.Name}");
                    Console.WriteLine($" messages: {folder.Recent}/{folder.Count}");

                    const MessageSummaryItems EVERYTHING = (MessageSummaryItems) (-1);
                    var summaries = folder.Fetch(0, -1, EVERYTHING);
                    var dst_message_ids = new List<string>();
                    foreach (var summary in summaries)
                    {
                        var message = folder.GetMessage(summary.UniqueId);
                        dst_message_ids.Add(message.MessageId);
                    }
                    // for (int i = 0; i < folder.Count; i++)
                    // {
                    //  dst_messages.Add(folder.GetMessage(i));
                    // }
                    // var dst_message_ids = (summaries.Select(summary => summary.Headers?.FirstOrDefault(h => h.Field.Equals("MessageID")))
                    // 	.Where(message_id => message_id != null)
                    // 	.Select(message_id => message_id.Value)).ToList();

                    // var summaries = folder.Fetch(0, -1, MessageSummaryItems.Full);
                    foreach (var message in messages)
                        if (dst_message_ids.Contains(message.MessageId))
                        {
                            Console.WriteLine($"Update(?) msg:{message.MessageId}");
                        }
                        else
                        {
                            Console.WriteLine($"Copy msg:{message.MessageId}");
                            folder.Append(message);
                        }

                    dst_client.Disconnect(true);
                }

                src_client.Disconnect(true);
            }
        }

        private static void test_imap(CONNECTION_OPTIONS opts)
        {
            using (var dst_client = new ImapClient())
            {
                dst_client.Connect(opts.host, opts.port, opts.ssl);

                dst_client.Authenticate(opts.user, opts.password);

                foreach (var ns in dst_client.PersonalNamespaces)
                {
                    var folders = dst_client.GetFolders(ns);

                    foreach (var folder in folders)
                    {
                        folder.Open(FolderAccess.ReadOnly);

                        Console.WriteLine($"Mailbox: {folder.Name}");
                        Console.WriteLine($" messages: {folder.Recent}/{folder.Count}");
                        if (!folder.Name.Equals("INBOX")) continue;

                        var summaries = folder.Fetch(0, -1, MessageSummaryItems.Full);
                        foreach (var summary in summaries)
                            Console.WriteLine("[summary] {0:D2}: {1}", summary.Index, summary.Envelope.Subject);

                        for (var i = 0; i < folder.Count; i++)
                        {
                            var message = folder.GetMessage(i);
                            Console.WriteLine("Subject: {0}", message.Subject);
                        }
                    }
                }

                dst_client.Disconnect(true);
            }
        }

        private static void test_pop3(CONNECTION_OPTIONS opts)
        {
            using (var src_client = new Pop3Client())
            {
                src_client.Connect(opts.host, opts.port, opts.ssl);

                src_client.Authenticate(opts.user, opts.password);

                for (var i = 0; i < src_client.Count; i++)
                {
                    var message = src_client.GetMessage(i);
                    Console.WriteLine("Subject: {0}", message.Subject);
                }

                src_client.Disconnect(true);
            }
        }
    }
}
