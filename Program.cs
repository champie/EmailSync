using System;
using System.Collections;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics;
using System.Linq;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MailKit.Security;
using MimeKit;

namespace EmailSync
{
    internal class Program
    {
        public enum TYPE
        {
            POP3,
            IMAP
        }

        internal class CONNECT_OPTS
        {
            public string host;
            public string password;
            public int port;
            public SecureSocketOptions ssl;

            public TYPE type;
            public string user;
        }

        internal class _CONNECT : IDisposable
        {
            private ImapClient imap;
            private Pop3Client pop3;

            public _CONNECT(CONNECT_OPTS connect_opts)
            {
            }

            public void Dispose()
            {
                imap?.Dispose();
                pop3?.Dispose();
            }

            public IEnumerable<FOLDER> GetFolders()
            {
                throw new NotImplementedException();
            }

            public FOLDER MakeOrOpenFloder(string name)
            {
                throw new NotImplementedException();
            }

            public void Disconnect(bool b)
            {
                throw new NotImplementedException();
            }

            public MimeMessage GetMessage(in int i)
            {
                throw new NotImplementedException();
            }
        }

        internal class FOLDER
        {
            public string Name { get; set; }

            public int GetNumMessages()
            {
                throw new NotImplementedException();
            }

            public IList<IMessageSummary> GetMessageSummaries()
            {
                throw new NotImplementedException();
                // folder.Fetch(0, -1, MessageSummaryItems.Full);
            }

            public IList<string> GetMessageIds()
            {
                throw new NotImplementedException();
            }

            public void Append(MimeMessage message)
            {
                throw new NotImplementedException();
            }
        }
        
        public class OPTS
        {
            public TYPE srcType { get; set; }
            public string srcHost { get; set; }
            public int srcPort { get; set; }
            public string srcUser { get; set; }
            public string srcPass { get; set; }
            public SecureSocketOptions srcSsl { get; set; }
            public CONNECT_OPTS src_connect_opts { get; set; }
            public TYPE dstType { get; set; }
            public string dstHost { get; set; }
            public int dstPort { get; set; }
            public string dstUser { get; set; }
            public string dstPass { get; set; }
            public SecureSocketOptions dstSsl { get; set; }
            public CONNECT_OPTS dst_connect_opts { get; set; }
        }

        private static int Main(string[] args)
        {
            // Create a root command with some options
            var rootCommand = new RootCommand
            {
                new Option<TYPE>(
                    "--src-type",
                    () => TYPE.IMAP,
                    "Type of source server (POP3 or IMAP)"),
                new Option<string>(
                    "--src-mailbox",
                    "Source mail folder (IMAP only)"),
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
                new Option<CONNECT_OPTS>(
                    "--src",
                    "Complete specification for mail server to read from"),

                new Option<TYPE>(
                    "--dst-type",
                    () => TYPE.IMAP,
                    "Type of destination server (POP3 not supported, only IMAP)"),
                new Option<string>(
                    "--dstsrc-mailbox",
                    "Folder to write to"),
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
                new Option<CONNECT_OPTS>(
                    "--dst",
                    "Complete specification for mail server to write to"),

                new Option<bool>(
                    "--recursive",
                    () => false,
                    "")
            };

            rootCommand.Description = "EmailSync program";


            rootCommand.Handler = CommandHandler.Create((OPTS opts) =>
            {
                sync_mailbox(
                    new CONNECT_OPTS
                    {
                        type = opts.srcType,
                        host = opts.srcHost,
                        port = opts.srcPort,
                        user = opts.srcUser,
                        password = opts.srcPass,
                        ssl = opts.srcSsl
                    },
                    new CONNECT_OPTS
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

        private static void sync_mailbox(CONNECT_OPTS srcOpts, CONNECT_OPTS dstOpts)
        {
            Debug.Assert(dstOpts.type == TYPE.IMAP,
                "Only IMAP destinations are supported at the moment");
            using (var src_client = connect(srcOpts))
            {
                using (var dst_client = connect(dstOpts))
                {
                    foreach (var ns in src_client.PersonalNamespaces)
                    foreach (var src_folder in src_client.GetFolders(ns))
                    {
                        var dst_folder = dst_client.GetFolder(src_folder.Name);
                        var dst_message_ids = GetMessageIds(dst_folder);
                        for (var i = 0; i < src_folder.Count; i++)
                        {
                            var message = src_folder.GetMessage(i);
                            if (!dst_message_ids.Contains(message.MessageId))
                            {
                                dst_folder.Append(message);
                            }
                            else
                            {
                                //TODO Check if message has changed
                            }
                        }
                    }
                    dst_client.Disconnect(true);
                }
                src_client.Disconnect(true);
            }
        }

        private static List<string> GetMessageIds(IMailFolder dst_folder)
        {
            throw new NotImplementedException();
        }

        private static IImapClient connect(CONNECT_OPTS opts)
        {
            IImapClient dst_client;
            switch (opts.type)
            {
                case TYPE.POP3:
                    dst_client = new Pop3Imap();
                    dst_client.Connect(opts.host, opts.port, opts.ssl);
                    dst_client.Authenticate(opts.user, opts.password);
                    return dst_client;
                    break;
                case TYPE.IMAP:
                    dst_client = new ImapClient();
                    dst_client.Connect(opts.host, opts.port, opts.ssl);
                    dst_client.Authenticate(opts.user, opts.password);
                    return dst_client;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void test_imap(CONNECT_OPTS opts)
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

        private static void test_pop3(CONNECT_OPTS opts)
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
