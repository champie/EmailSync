using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MailKit.Net.Proxy;
using MailKit.Security;

namespace EmailSync
{
    internal class Pop3AsImap : IImapClient
    {
        private IPop3Client pop3_client;
        
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Connect(string host, int port, bool useSsl, CancellationToken cancellationToken = new CancellationToken())
        {
            pop3_client.Connect(host, port, useSsl, cancellationToken);
        }

        public Task ConnectAsync(string host, int port, bool useSsl, CancellationToken cancellationToken = new CancellationToken())
        {
            return pop3_client.ConnectAsync(host, port, useSsl, cancellationToken);
        }

        public void Connect(string host, int port = 0, SecureSocketOptions options = SecureSocketOptions.Auto, CancellationToken cancellationToken = new CancellationToken())
        {
            pop3_client.Connect(host, port, options, cancellationToken);
        }

        public Task ConnectAsync(string host, int port = 0, SecureSocketOptions options = SecureSocketOptions.Auto, CancellationToken cancellationToken = new CancellationToken())
        {
            return pop3_client.ConnectAsync(host, port, options, cancellationToken);
        }

        public void Connect(Socket socket, string host, int port = 0, SecureSocketOptions options = SecureSocketOptions.Auto,
            CancellationToken cancellationToken = new CancellationToken())
        {
            pop3_client.Connect(socket, host, port, options, cancellationToken);
        }

        public Task ConnectAsync(Socket socket, string host, int port = 0, SecureSocketOptions options = SecureSocketOptions.Auto,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return pop3_client.ConnectAsync(socket, host, port, options, cancellationToken);
        }

        public void Connect(Stream stream, string host, int port = 0, SecureSocketOptions options = SecureSocketOptions.Auto,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task ConnectAsync(Stream stream, string host, int port = 0, SecureSocketOptions options = SecureSocketOptions.Auto,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void Authenticate(ICredentials credentials, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task AuthenticateAsync(ICredentials credentials, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void Authenticate(Encoding encoding, ICredentials credentials, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task AuthenticateAsync(Encoding encoding, ICredentials credentials, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void Authenticate(Encoding encoding, string userName, string password, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task AuthenticateAsync(Encoding encoding, string userName, string password, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void Authenticate(string userName, string password, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task AuthenticateAsync(string userName, string password, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void Authenticate(SaslMechanism mechanism, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task AuthenticateAsync(SaslMechanism mechanism, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void Disconnect(bool quit, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task DisconnectAsync(bool quit, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void NoOp(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task NoOpAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public object SyncRoot { get; }
        public SslProtocols SslProtocols { get; set; }
        public X509CertificateCollection ClientCertificates { get; set; }
        public bool CheckCertificateRevocation { get; set; }
        public RemoteCertificateValidationCallback ServerCertificateValidationCallback { get; set; }
        public IPEndPoint LocalEndPoint { get; set; }
        public IProxyClient ProxyClient { get; set; }
        public HashSet<string> AuthenticationMechanisms { get; }
        public bool IsAuthenticated { get; }
        public bool IsConnected { get; }
        public bool IsSecure { get; }
        public int Timeout { get; set; }
        public event EventHandler<ConnectedEventArgs> Connected;
        public event EventHandler<DisconnectedEventArgs> Disconnected;
        public event EventHandler<AuthenticatedEventArgs> Authenticated;
        public void EnableQuickResync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task EnableQuickResyncAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IMailFolder GetFolder(SpecialFolder folder)
        {
            throw new NotImplementedException();
        }

        public IMailFolder GetFolder(FolderNamespace @namespace)
        {
            throw new NotImplementedException();
        }

        public IList<IMailFolder> GetFolders(FolderNamespace @namespace, bool subscribedOnly, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<IMailFolder>> GetFoldersAsync(FolderNamespace @namespace, bool subscribedOnly, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<IMailFolder> GetFolders(FolderNamespace @namespace, StatusItems items = StatusItems.None, bool subscribedOnly = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<IMailFolder>> GetFoldersAsync(FolderNamespace @namespace, StatusItems items = StatusItems.None, bool subscribedOnly = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IMailFolder GetFolder(string path, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IMailFolder> GetFolderAsync(string path, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public string GetMetadata(MetadataTag tag, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<string> GetMetadataAsync(MetadataTag tag, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public MetadataCollection GetMetadata(IEnumerable<MetadataTag> tags, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<MetadataCollection> GetMetadataAsync(IEnumerable<MetadataTag> tags, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public MetadataCollection GetMetadata(MetadataOptions options, IEnumerable<MetadataTag> tags, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<MetadataCollection> GetMetadataAsync(MetadataOptions options, IEnumerable<MetadataTag> tags, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void SetMetadata(MetadataCollection metadata, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task SetMetadataAsync(MetadataCollection metadata, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public FolderNamespaceCollection PersonalNamespaces { get; }
        public FolderNamespaceCollection SharedNamespaces { get; }
        public FolderNamespaceCollection OtherNamespaces { get; }
        public bool SupportsQuotas { get; }
        public HashSet<ThreadingAlgorithm> ThreadingAlgorithms { get; }
        public IMailFolder Inbox { get; }
        public event EventHandler<AlertEventArgs> Alert;
        public event EventHandler<FolderCreatedEventArgs> FolderCreated;
        public event EventHandler<MetadataChangedEventArgs> MetadataChanged;
        public void Compress(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task CompressAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void EnableUTF8(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task EnableUTF8Async(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public ImapImplementation Identify(ImapImplementation clientImplementation, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<ImapImplementation> IdentifyAsync(ImapImplementation clientImplementation, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void Idle(CancellationToken doneToken, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task IdleAsync(CancellationToken doneToken, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void Notify(bool status, IList<ImapEventGroup> eventGroups, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task NotifyAsync(bool status, IList<ImapEventGroup> eventGroups, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void DisableNotify(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task DisableNotifyAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public ImapCapabilities Capabilities { get; set; }
        public uint? AppendLimit { get; }
        public int InternationalizationLevel { get; }
        public AccessRights Rights { get; }
        public bool IsIdle { get; }
    }
}