using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MailKit.Search;
using MimeKit;

namespace EmailSync
{
    internal class Pop3Imap : Pop3Client, IImapClient, IList<IMailFolder>
    {
        private readonly FolderNamespaceCollection personal_namespaces;
        private IList<IMailFolder> list_implementation;

        public Pop3Imap()
        {
            personal_namespaces = new FolderNamespaceCollection();
            personal_namespaces.Add(new FolderNamespace('/', "INBOX"));
            list_implementation = new List<IMailFolder>();
            list_implementation.Add(new Pop3MailFolder(this));
        }

        public IList<IMailFolder> GetFolders(FolderNamespace @namespace, StatusItems items = StatusItems.None, bool subscribedOnly = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return this;
        }

        public FolderNamespaceCollection PersonalNamespaces => personal_namespaces;

        #region NOT_IMPLEMENTED
        
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

        public Task SetMetadataAsync(MetadataCollection metadata,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }
        


        public FolderNamespaceCollection SharedNamespaces { get; }
        public FolderNamespaceCollection OtherNamespaces { get; }
        public bool SupportsQuotas { get; }
        public HashSet<ThreadingAlgorithm> ThreadingAlgorithms { get; }
        public IMailFolder Inbox { get; }
        public event EventHandler<AlertEventArgs> Alert;
        public event EventHandler<FolderCreatedEventArgs> FolderCreated;
        public event EventHandler<MetadataChangedEventArgs> MetadataChanged;
        
        #endregion

        //--------------------------------------------------------------------------------
        // IIMapClient
        
        #region NOT_IMPLEMENTED
        
        public void Compress(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task CompressAsync(CancellationToken cancellationToken = new CancellationToken())
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
        
        public new ImapCapabilities Capabilities { get; set; }
        public uint? AppendLimit { get; }
        public int InternationalizationLevel { get; }
        public AccessRights Rights { get; }
        public bool IsIdle { get; }

        #endregion
        
        //--------------------------------------------------------------------------------
        // IList<IMailFolder>
            
        public IEnumerator<IMailFolder> GetEnumerator()
        {
            return list_implementation.GetEnumerator();
        }

        public void Add(IMailFolder item)
        {
            list_implementation.Add(item);
        }

        public void Clear()
        {
            list_implementation.Clear();
        }

        public bool Contains(IMailFolder item)
        {
            return list_implementation.Contains(item);
        }

        public void CopyTo(IMailFolder[] array, int arrayIndex)
        {
            list_implementation.CopyTo(array, arrayIndex);
        }

        public bool Remove(IMailFolder item)
        {
            return list_implementation.Remove(item);
        }

        public bool IsReadOnly => list_implementation.IsReadOnly;

        public int IndexOf(IMailFolder item)
        {
            return list_implementation.IndexOf(item);
        }

        public void Insert(int index, IMailFolder item)
        {
            list_implementation.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list_implementation.RemoveAt(index);
        }

        public IMailFolder this[int index]
        {
            get => list_implementation[index];
            set => list_implementation[index] = value;
        }
    }

    //--------------------------------------------------------------------------------
    // IMailFolder

    internal class Pop3MailFolder : IMailFolder
    {
        private Pop3Client client;
        private IMailFolder mail_folder_implementation;

        public Pop3MailFolder(Pop3Client client)
        {
            this.client = client;
        }

        public string Name => "INBOX";
        public int Count => client.Count;

        public FolderAccess Open(FolderAccess access, CancellationToken cancellationToken = new CancellationToken())
        {
            return access;
        }

        public MimeMessage GetMessage(int index, CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            return client.GetMessage(index, cancellationToken);
        }

        #region NOT_IMPLEMENTED
        
        public IEnumerator<MimeMessage> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Supports(FolderFeature feature)
        {
            throw new NotImplementedException();
        }

        public FolderAccess Open(FolderAccess access, uint uidValidity, ulong highestModSeq, IList<UniqueId> uids,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<FolderAccess> OpenAsync(FolderAccess access, uint uidValidity, ulong highestModSeq, IList<UniqueId> uids,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<FolderAccess> OpenAsync(FolderAccess access, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void Close(bool expunge = false, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task CloseAsync(bool expunge = false, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IMailFolder Create(string name, bool isMessageFolder, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IMailFolder> CreateAsync(string name, bool isMessageFolder, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IMailFolder Create(string name, IEnumerable<SpecialFolder> specialUses, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IMailFolder> CreateAsync(string name, IEnumerable<SpecialFolder> specialUses, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IMailFolder Create(string name, SpecialFolder specialUse, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IMailFolder> CreateAsync(string name, SpecialFolder specialUse, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void Rename(IMailFolder parent, string name, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task RenameAsync(IMailFolder parent, string name, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void Delete(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void Subscribe(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task SubscribeAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task UnsubscribeAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<IMailFolder> GetSubfolders(StatusItems items, bool subscribedOnly = false, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<IMailFolder>> GetSubfoldersAsync(StatusItems items, bool subscribedOnly = false, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<IMailFolder> GetSubfolders(bool subscribedOnly = false, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<IMailFolder>> GetSubfoldersAsync(bool subscribedOnly = false, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IMailFolder GetSubfolder(string name, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IMailFolder> GetSubfolderAsync(string name, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void Check(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task CheckAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void Status(StatusItems items, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task StatusAsync(StatusItems items, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public AccessControlList GetAccessControlList(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<AccessControlList> GetAccessControlListAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public AccessRights GetAccessRights(string name, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<AccessRights> GetAccessRightsAsync(string name, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public AccessRights GetMyAccessRights(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<AccessRights> GetMyAccessRightsAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void AddAccessRights(string name, AccessRights rights, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task AddAccessRightsAsync(string name, AccessRights rights, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void RemoveAccessRights(string name, AccessRights rights, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task RemoveAccessRightsAsync(string name, AccessRights rights, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void SetAccessRights(string name, AccessRights rights, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task SetAccessRightsAsync(string name, AccessRights rights, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void RemoveAccess(string name, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task RemoveAccessAsync(string name, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public FolderQuota GetQuota(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<FolderQuota> GetQuotaAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public FolderQuota SetQuota(uint? messageLimit, uint? storageLimit, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<FolderQuota> SetQuotaAsync(uint? messageLimit, uint? storageLimit, CancellationToken cancellationToken = new CancellationToken())
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

        public void Expunge(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task ExpungeAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void Expunge(IList<UniqueId> uids, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task ExpungeAsync(IList<UniqueId> uids, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public UniqueId? Append(MimeMessage message, MessageFlags flags = MessageFlags.None, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<UniqueId?> AppendAsync(MimeMessage message, MessageFlags flags = MessageFlags.None, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public UniqueId? Append(MimeMessage message, MessageFlags flags, DateTimeOffset date, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<UniqueId?> AppendAsync(MimeMessage message, MessageFlags flags, DateTimeOffset date, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public UniqueId? Append(MimeMessage message, MessageFlags flags, DateTimeOffset? date, IList<Annotation> annotations,
            CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<UniqueId?> AppendAsync(MimeMessage message, MessageFlags flags, DateTimeOffset? date, IList<Annotation> annotations,
            CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public UniqueId? Append(FormatOptions options, MimeMessage message, MessageFlags flags = MessageFlags.None,
            CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<UniqueId?> AppendAsync(FormatOptions options, MimeMessage message, MessageFlags flags = MessageFlags.None,
            CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public UniqueId? Append(FormatOptions options, MimeMessage message, MessageFlags flags, DateTimeOffset date,
            CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<UniqueId?> AppendAsync(FormatOptions options, MimeMessage message, MessageFlags flags, DateTimeOffset date,
            CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public UniqueId? Append(FormatOptions options, MimeMessage message, MessageFlags flags, DateTimeOffset? date, IList<Annotation> annotations,
            CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<UniqueId?> AppendAsync(FormatOptions options, MimeMessage message, MessageFlags flags, DateTimeOffset? date, IList<Annotation> annotations,
            CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public IList<UniqueId> Append(IList<MimeMessage> messages, IList<MessageFlags> flags, CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UniqueId>> AppendAsync(IList<MimeMessage> messages, IList<MessageFlags> flags, CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public IList<UniqueId> Append(IList<MimeMessage> messages, IList<MessageFlags> flags, IList<DateTimeOffset> dates, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UniqueId>> AppendAsync(IList<MimeMessage> messages, IList<MessageFlags> flags, IList<DateTimeOffset> dates, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public IList<UniqueId> Append(FormatOptions options, IList<MimeMessage> messages, IList<MessageFlags> flags, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UniqueId>> AppendAsync(FormatOptions options, IList<MimeMessage> messages, IList<MessageFlags> flags, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public IList<UniqueId> Append(FormatOptions options, IList<MimeMessage> messages, IList<MessageFlags> flags, IList<DateTimeOffset> dates, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UniqueId>> AppendAsync(FormatOptions options, IList<MimeMessage> messages, IList<MessageFlags> flags, IList<DateTimeOffset> dates, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public UniqueId? Replace(UniqueId uid, MimeMessage message, MessageFlags flags = MessageFlags.None, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<UniqueId?> ReplaceAsync(UniqueId uid, MimeMessage message, MessageFlags flags = MessageFlags.None, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public UniqueId? Replace(UniqueId uid, MimeMessage message, MessageFlags flags, DateTimeOffset date,
            CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<UniqueId?> ReplaceAsync(UniqueId uid, MimeMessage message, MessageFlags flags, DateTimeOffset date,
            CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public UniqueId? Replace(FormatOptions options, UniqueId uid, MimeMessage message, MessageFlags flags = MessageFlags.None,
            CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<UniqueId?> ReplaceAsync(FormatOptions options, UniqueId uid, MimeMessage message, MessageFlags flags = MessageFlags.None,
            CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public UniqueId? Replace(FormatOptions options, UniqueId uid, MimeMessage message, MessageFlags flags, DateTimeOffset date,
            CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<UniqueId?> ReplaceAsync(FormatOptions options, UniqueId uid, MimeMessage message, MessageFlags flags, DateTimeOffset date,
            CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public UniqueId? Replace(int index, MimeMessage message, MessageFlags flags = MessageFlags.None, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<UniqueId?> ReplaceAsync(int index, MimeMessage message, MessageFlags flags = MessageFlags.None, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public UniqueId? Replace(int index, MimeMessage message, MessageFlags flags, DateTimeOffset date,
            CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<UniqueId?> ReplaceAsync(int index, MimeMessage message, MessageFlags flags, DateTimeOffset date,
            CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public UniqueId? Replace(FormatOptions options, int index, MimeMessage message, MessageFlags flags = MessageFlags.None,
            CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<UniqueId?> ReplaceAsync(FormatOptions options, int index, MimeMessage message, MessageFlags flags = MessageFlags.None,
            CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public UniqueId? Replace(FormatOptions options, int index, MimeMessage message, MessageFlags flags, DateTimeOffset date,
            CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<UniqueId?> ReplaceAsync(FormatOptions options, int index, MimeMessage message, MessageFlags flags, DateTimeOffset date,
            CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public UniqueId? CopyTo(UniqueId uid, IMailFolder destination, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<UniqueId?> CopyToAsync(UniqueId uid, IMailFolder destination, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public UniqueIdMap CopyTo(IList<UniqueId> uids, IMailFolder destination, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<UniqueIdMap> CopyToAsync(IList<UniqueId> uids, IMailFolder destination, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public UniqueId? MoveTo(UniqueId uid, IMailFolder destination, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<UniqueId?> MoveToAsync(UniqueId uid, IMailFolder destination, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public UniqueIdMap MoveTo(IList<UniqueId> uids, IMailFolder destination, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<UniqueIdMap> MoveToAsync(IList<UniqueId> uids, IMailFolder destination, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void CopyTo(int index, IMailFolder destination, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task CopyToAsync(int index, IMailFolder destination, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void CopyTo(IList<int> indexes, IMailFolder destination, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task CopyToAsync(IList<int> indexes, IMailFolder destination, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void MoveTo(int index, IMailFolder destination, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task MoveToAsync(int index, IMailFolder destination, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void MoveTo(IList<int> indexes, IMailFolder destination, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task MoveToAsync(IList<int> indexes, IMailFolder destination, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<IMessageSummary> Fetch(IList<UniqueId> uids, MessageSummaryItems items, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<IMessageSummary>> FetchAsync(IList<UniqueId> uids, MessageSummaryItems items, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<IMessageSummary> Fetch(IList<UniqueId> uids, MessageSummaryItems items, IEnumerable<HeaderId> headers, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<IMessageSummary>> FetchAsync(IList<UniqueId> uids, MessageSummaryItems items, IEnumerable<HeaderId> headers, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<IMessageSummary> Fetch(IList<UniqueId> uids, MessageSummaryItems items, IEnumerable<string> headers, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<IMessageSummary>> FetchAsync(IList<UniqueId> uids, MessageSummaryItems items, IEnumerable<string> headers, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<IMessageSummary> Fetch(IList<UniqueId> uids, ulong modseq, MessageSummaryItems items, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<IMessageSummary>> FetchAsync(IList<UniqueId> uids, ulong modseq, MessageSummaryItems items, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<IMessageSummary> Fetch(IList<UniqueId> uids, ulong modseq, MessageSummaryItems items, IEnumerable<HeaderId> headers,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<IMessageSummary>> FetchAsync(IList<UniqueId> uids, ulong modseq, MessageSummaryItems items, IEnumerable<HeaderId> headers,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<IMessageSummary> Fetch(IList<UniqueId> uids, ulong modseq, MessageSummaryItems items, IEnumerable<string> headers,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<IMessageSummary>> FetchAsync(IList<UniqueId> uids, ulong modseq, MessageSummaryItems items, IEnumerable<string> headers,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<IMessageSummary> Fetch(IList<int> indexes, MessageSummaryItems items, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<IMessageSummary>> FetchAsync(IList<int> indexes, MessageSummaryItems items, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<IMessageSummary> Fetch(IList<int> indexes, MessageSummaryItems items, IEnumerable<HeaderId> headers, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<IMessageSummary>> FetchAsync(IList<int> indexes, MessageSummaryItems items, IEnumerable<HeaderId> headers, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<IMessageSummary> Fetch(IList<int> indexes, MessageSummaryItems items, IEnumerable<string> headers, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<IMessageSummary>> FetchAsync(IList<int> indexes, MessageSummaryItems items, IEnumerable<string> headers, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<IMessageSummary> Fetch(IList<int> indexes, ulong modseq, MessageSummaryItems items, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<IMessageSummary>> FetchAsync(IList<int> indexes, ulong modseq, MessageSummaryItems items, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<IMessageSummary> Fetch(IList<int> indexes, ulong modseq, MessageSummaryItems items, IEnumerable<HeaderId> headers,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<IMessageSummary>> FetchAsync(IList<int> indexes, ulong modseq, MessageSummaryItems items, IEnumerable<HeaderId> headers,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<IMessageSummary> Fetch(IList<int> indexes, ulong modseq, MessageSummaryItems items, IEnumerable<string> headers,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<IMessageSummary>> FetchAsync(IList<int> indexes, ulong modseq, MessageSummaryItems items, IEnumerable<string> headers,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<IMessageSummary> Fetch(int min, int max, MessageSummaryItems items, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<IMessageSummary>> FetchAsync(int min, int max, MessageSummaryItems items, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<IMessageSummary> Fetch(int min, int max, MessageSummaryItems items, IEnumerable<HeaderId> headers, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<IMessageSummary>> FetchAsync(int min, int max, MessageSummaryItems items, IEnumerable<HeaderId> headers,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<IMessageSummary> Fetch(int min, int max, MessageSummaryItems items, IEnumerable<string> headers, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<IMessageSummary>> FetchAsync(int min, int max, MessageSummaryItems items, IEnumerable<string> headers,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<IMessageSummary> Fetch(int min, int max, ulong modseq, MessageSummaryItems items, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<IMessageSummary>> FetchAsync(int min, int max, ulong modseq, MessageSummaryItems items, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<IMessageSummary> Fetch(int min, int max, ulong modseq, MessageSummaryItems items, IEnumerable<HeaderId> headers,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<IMessageSummary>> FetchAsync(int min, int max, ulong modseq, MessageSummaryItems items, IEnumerable<HeaderId> headers,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<IMessageSummary> Fetch(int min, int max, ulong modseq, MessageSummaryItems items, IEnumerable<string> headers,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<IMessageSummary>> FetchAsync(int min, int max, ulong modseq, MessageSummaryItems items, IEnumerable<string> headers,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public HeaderList GetHeaders(UniqueId uid, CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<HeaderList> GetHeadersAsync(UniqueId uid, CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public HeaderList GetHeaders(UniqueId uid, BodyPart part, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<HeaderList> GetHeadersAsync(UniqueId uid, BodyPart part, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public HeaderList GetHeaders(int index, CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<HeaderList> GetHeadersAsync(int index, CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public HeaderList GetHeaders(int index, BodyPart part, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<HeaderList> GetHeadersAsync(int index, BodyPart part, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public MimeMessage GetMessage(UniqueId uid, CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<MimeMessage> GetMessageAsync(UniqueId uid, CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<MimeMessage> GetMessageAsync(int index, CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public MimeEntity GetBodyPart(UniqueId uid, BodyPart part, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<MimeEntity> GetBodyPartAsync(UniqueId uid, BodyPart part, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public MimeEntity GetBodyPart(int index, BodyPart part, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<MimeEntity> GetBodyPartAsync(int index, BodyPart part, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Stream GetStream(UniqueId uid, int offset, int count, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> GetStreamAsync(UniqueId uid, int offset, int count, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Stream GetStream(int index, int offset, int count, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> GetStreamAsync(int index, int offset, int count, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Stream GetStream(UniqueId uid, BodyPart part, int offset, int count, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> GetStreamAsync(UniqueId uid, BodyPart part, int offset, int count, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Stream GetStream(int index, BodyPart part, int offset, int count, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> GetStreamAsync(int index, BodyPart part, int offset, int count, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Stream GetStream(UniqueId uid, string section, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> GetStreamAsync(UniqueId uid, string section, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Stream GetStream(UniqueId uid, string section, int offset, int count, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> GetStreamAsync(UniqueId uid, string section, int offset, int count, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Stream GetStream(int index, string section, CancellationToken cancellationToken = new CancellationToken(), ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> GetStreamAsync(int index, string section, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Stream GetStream(int index, string section, int offset, int count, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> GetStreamAsync(int index, string section, int offset, int count, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            throw new NotImplementedException();
        }

        public void AddFlags(UniqueId uid, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task AddFlagsAsync(UniqueId uid, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void AddFlags(UniqueId uid, MessageFlags flags, HashSet<string> keywords, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task AddFlagsAsync(UniqueId uid, MessageFlags flags, HashSet<string> keywords, bool silent,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void AddFlags(IList<UniqueId> uids, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task AddFlagsAsync(IList<UniqueId> uids, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void AddFlags(IList<UniqueId> uids, MessageFlags flags, HashSet<string> keywords, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task AddFlagsAsync(IList<UniqueId> uids, MessageFlags flags, HashSet<string> keywords, bool silent,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void RemoveFlags(UniqueId uid, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task RemoveFlagsAsync(UniqueId uid, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void RemoveFlags(UniqueId uid, MessageFlags flags, HashSet<string> keywords, bool silent,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task RemoveFlagsAsync(UniqueId uid, MessageFlags flags, HashSet<string> keywords, bool silent,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void RemoveFlags(IList<UniqueId> uids, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task RemoveFlagsAsync(IList<UniqueId> uids, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void RemoveFlags(IList<UniqueId> uids, MessageFlags flags, HashSet<string> keywords, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task RemoveFlagsAsync(IList<UniqueId> uids, MessageFlags flags, HashSet<string> keywords, bool silent,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void SetFlags(UniqueId uid, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task SetFlagsAsync(UniqueId uid, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void SetFlags(UniqueId uid, MessageFlags flags, HashSet<string> keywords, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task SetFlagsAsync(UniqueId uid, MessageFlags flags, HashSet<string> keywords, bool silent,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void SetFlags(IList<UniqueId> uids, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task SetFlagsAsync(IList<UniqueId> uids, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void SetFlags(IList<UniqueId> uids, MessageFlags flags, HashSet<string> keywords, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task SetFlagsAsync(IList<UniqueId> uids, MessageFlags flags, HashSet<string> keywords, bool silent,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<UniqueId> AddFlags(IList<UniqueId> uids, ulong modseq, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<UniqueId>> AddFlagsAsync(IList<UniqueId> uids, ulong modseq, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<UniqueId> AddFlags(IList<UniqueId> uids, ulong modseq, MessageFlags flags, HashSet<string> keywords, bool silent,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<UniqueId>> AddFlagsAsync(IList<UniqueId> uids, ulong modseq, MessageFlags flags, HashSet<string> keywords, bool silent,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<UniqueId> RemoveFlags(IList<UniqueId> uids, ulong modseq, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<UniqueId>> RemoveFlagsAsync(IList<UniqueId> uids, ulong modseq, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<UniqueId> RemoveFlags(IList<UniqueId> uids, ulong modseq, MessageFlags flags, HashSet<string> keywords, bool silent,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<UniqueId>> RemoveFlagsAsync(IList<UniqueId> uids, ulong modseq, MessageFlags flags, HashSet<string> keywords, bool silent,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<UniqueId> SetFlags(IList<UniqueId> uids, ulong modseq, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<UniqueId>> SetFlagsAsync(IList<UniqueId> uids, ulong modseq, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<UniqueId> SetFlags(IList<UniqueId> uids, ulong modseq, MessageFlags flags, HashSet<string> keywords, bool silent,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<UniqueId>> SetFlagsAsync(IList<UniqueId> uids, ulong modseq, MessageFlags flags, HashSet<string> keywords, bool silent,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void AddFlags(int index, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task AddFlagsAsync(int index, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void AddFlags(int index, MessageFlags flags, HashSet<string> keywords, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task AddFlagsAsync(int index, MessageFlags flags, HashSet<string> keywords, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void AddFlags(IList<int> indexes, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task AddFlagsAsync(IList<int> indexes, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void AddFlags(IList<int> indexes, MessageFlags flags, HashSet<string> keywords, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task AddFlagsAsync(IList<int> indexes, MessageFlags flags, HashSet<string> keywords, bool silent,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void RemoveFlags(int index, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task RemoveFlagsAsync(int index, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void RemoveFlags(int index, MessageFlags flags, HashSet<string> keywords, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task RemoveFlagsAsync(int index, MessageFlags flags, HashSet<string> keywords, bool silent,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void RemoveFlags(IList<int> indexes, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task RemoveFlagsAsync(IList<int> indexes, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void RemoveFlags(IList<int> indexes, MessageFlags flags, HashSet<string> keywords, bool silent,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task RemoveFlagsAsync(IList<int> indexes, MessageFlags flags, HashSet<string> keywords, bool silent,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void SetFlags(int index, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task SetFlagsAsync(int index, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void SetFlags(int index, MessageFlags flags, HashSet<string> keywords, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task SetFlagsAsync(int index, MessageFlags flags, HashSet<string> keywords, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void SetFlags(IList<int> indexes, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task SetFlagsAsync(IList<int> indexes, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void SetFlags(IList<int> indexes, MessageFlags flags, HashSet<string> keywords, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task SetFlagsAsync(IList<int> indexes, MessageFlags flags, HashSet<string> keywords, bool silent,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<int> AddFlags(IList<int> indexes, ulong modseq, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<int>> AddFlagsAsync(IList<int> indexes, ulong modseq, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<int> AddFlags(IList<int> indexes, ulong modseq, MessageFlags flags, HashSet<string> keywords, bool silent,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<int>> AddFlagsAsync(IList<int> indexes, ulong modseq, MessageFlags flags, HashSet<string> keywords, bool silent,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<int> RemoveFlags(IList<int> indexes, ulong modseq, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<int>> RemoveFlagsAsync(IList<int> indexes, ulong modseq, MessageFlags flags, bool silent,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<int> RemoveFlags(IList<int> indexes, ulong modseq, MessageFlags flags, HashSet<string> keywords, bool silent,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<int>> RemoveFlagsAsync(IList<int> indexes, ulong modseq, MessageFlags flags, HashSet<string> keywords, bool silent,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<int> SetFlags(IList<int> indexes, ulong modseq, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<int>> SetFlagsAsync(IList<int> indexes, ulong modseq, MessageFlags flags, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<int> SetFlags(IList<int> indexes, ulong modseq, MessageFlags flags, HashSet<string> keywords, bool silent,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<int>> SetFlagsAsync(IList<int> indexes, ulong modseq, MessageFlags flags, HashSet<string> keywords, bool silent,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void AddLabels(UniqueId uid, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task AddLabelsAsync(UniqueId uid, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void AddLabels(IList<UniqueId> uids, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task AddLabelsAsync(IList<UniqueId> uids, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void RemoveLabels(UniqueId uid, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task RemoveLabelsAsync(UniqueId uid, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void RemoveLabels(IList<UniqueId> uids, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task RemoveLabelsAsync(IList<UniqueId> uids, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void SetLabels(UniqueId uid, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task SetLabelsAsync(UniqueId uid, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void SetLabels(IList<UniqueId> uids, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task SetLabelsAsync(IList<UniqueId> uids, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<UniqueId> AddLabels(IList<UniqueId> uids, ulong modseq, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<UniqueId>> AddLabelsAsync(IList<UniqueId> uids, ulong modseq, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<UniqueId> RemoveLabels(IList<UniqueId> uids, ulong modseq, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<UniqueId>> RemoveLabelsAsync(IList<UniqueId> uids, ulong modseq, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<UniqueId> SetLabels(IList<UniqueId> uids, ulong modseq, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<UniqueId>> SetLabelsAsync(IList<UniqueId> uids, ulong modseq, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void AddLabels(int index, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task AddLabelsAsync(int index, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void AddLabels(IList<int> indexes, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task AddLabelsAsync(IList<int> indexes, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void RemoveLabels(int index, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task RemoveLabelsAsync(int index, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void RemoveLabels(IList<int> indexes, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task RemoveLabelsAsync(IList<int> indexes, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void SetLabels(int index, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task SetLabelsAsync(int index, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void SetLabels(IList<int> indexes, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task SetLabelsAsync(IList<int> indexes, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<int> AddLabels(IList<int> indexes, ulong modseq, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<int>> AddLabelsAsync(IList<int> indexes, ulong modseq, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<int> RemoveLabels(IList<int> indexes, ulong modseq, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<int>> RemoveLabelsAsync(IList<int> indexes, ulong modseq, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<int> SetLabels(IList<int> indexes, ulong modseq, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<int>> SetLabelsAsync(IList<int> indexes, ulong modseq, IList<string> labels, bool silent, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void Store(UniqueId uid, IList<Annotation> annotations, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task StoreAsync(UniqueId uid, IList<Annotation> annotations, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void Store(IList<UniqueId> uids, IList<Annotation> annotations, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task StoreAsync(IList<UniqueId> uids, IList<Annotation> annotations, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<UniqueId> Store(IList<UniqueId> uids, ulong modseq, IList<Annotation> annotations, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<UniqueId>> StoreAsync(IList<UniqueId> uids, ulong modseq, IList<Annotation> annotations, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void Store(int index, IList<Annotation> annotations, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task StoreAsync(int index, IList<Annotation> annotations, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void Store(IList<int> indexes, IList<Annotation> annotations, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task StoreAsync(IList<int> indexes, IList<Annotation> annotations, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<int> Store(IList<int> indexes, ulong modseq, IList<Annotation> annotations, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<int>> StoreAsync(IList<int> indexes, ulong modseq, IList<Annotation> annotations, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<UniqueId> Search(SearchQuery query, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<UniqueId>> SearchAsync(SearchQuery query, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<UniqueId> Search(IList<UniqueId> uids, SearchQuery query, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<UniqueId>> SearchAsync(IList<UniqueId> uids, SearchQuery query, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public SearchResults Search(SearchOptions options, SearchQuery query, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<SearchResults> SearchAsync(SearchOptions options, SearchQuery query, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public SearchResults Search(SearchOptions options, IList<UniqueId> uids, SearchQuery query, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<SearchResults> SearchAsync(SearchOptions options, IList<UniqueId> uids, SearchQuery query, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<UniqueId> Sort(SearchQuery query, IList<OrderBy> orderBy, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<UniqueId>> SortAsync(SearchQuery query, IList<OrderBy> orderBy, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<UniqueId> Sort(IList<UniqueId> uids, SearchQuery query, IList<OrderBy> orderBy, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<UniqueId>> SortAsync(IList<UniqueId> uids, SearchQuery query, IList<OrderBy> orderBy, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public SearchResults Sort(SearchOptions options, SearchQuery query, IList<OrderBy> orderBy, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<SearchResults> SortAsync(SearchOptions options, SearchQuery query, IList<OrderBy> orderBy, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public SearchResults Sort(SearchOptions options, IList<UniqueId> uids, SearchQuery query, IList<OrderBy> orderBy,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<SearchResults> SortAsync(SearchOptions options, IList<UniqueId> uids, SearchQuery query, IList<OrderBy> orderBy,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<MessageThread> Thread(ThreadingAlgorithm algorithm, SearchQuery query, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<MessageThread>> ThreadAsync(ThreadingAlgorithm algorithm, SearchQuery query, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IList<MessageThread> Thread(IList<UniqueId> uids, ThreadingAlgorithm algorithm, SearchQuery query, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IList<MessageThread>> ThreadAsync(IList<UniqueId> uids, ThreadingAlgorithm algorithm, SearchQuery query, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public object SyncRoot { get; }
        public IMailFolder ParentFolder { get; }
        public FolderAttributes Attributes { get; }
        public AnnotationAccess AnnotationAccess { get; }
        public AnnotationScope AnnotationScopes { get; }
        public uint MaxAnnotationSize { get; }
        public MessageFlags PermanentFlags { get; }
        public MessageFlags AcceptedFlags { get; }
        public char DirectorySeparator { get; }
        public FolderAccess Access { get; }
        public bool IsNamespace { get; }
        public string FullName { get; }
        public string Id { get; }
        public bool IsSubscribed { get; }
        public bool IsOpen { get; }
        public bool Exists { get; }
        public bool SupportsModSeq { get; }
        public ulong HighestModSeq { get; }
        public uint UidValidity { get; }
        public UniqueId? UidNext { get; }
        public uint? AppendLimit { get; }
        public ulong? Size { get; }
        public int FirstUnread { get; }
        public int Unread { get; }
        public int Recent { get; }
        public HashSet<ThreadingAlgorithm> ThreadingAlgorithms { get; }
        public event EventHandler<EventArgs> Opened;
        public event EventHandler<EventArgs> Closed;
        public event EventHandler<EventArgs> Deleted;
        public event EventHandler<FolderRenamedEventArgs> Renamed;
        public event EventHandler<EventArgs> Subscribed;
        public event EventHandler<EventArgs> Unsubscribed;
        public event EventHandler<MessageEventArgs> MessageExpunged;
        public event EventHandler<MessagesVanishedEventArgs> MessagesVanished;
        public event EventHandler<MessageFlagsChangedEventArgs> MessageFlagsChanged;
        public event EventHandler<MessageLabelsChangedEventArgs> MessageLabelsChanged;
        public event EventHandler<AnnotationsChangedEventArgs> AnnotationsChanged;
        public event EventHandler<MessageSummaryFetchedEventArgs> MessageSummaryFetched;
        public event EventHandler<MetadataChangedEventArgs> MetadataChanged;
        public event EventHandler<ModSeqChangedEventArgs> ModSeqChanged;
        public event EventHandler<EventArgs> HighestModSeqChanged;
        public event EventHandler<EventArgs> UidNextChanged;
        public event EventHandler<EventArgs> UidValidityChanged;
        public event EventHandler<EventArgs> IdChanged;
        public event EventHandler<EventArgs> SizeChanged;
        public event EventHandler<EventArgs> CountChanged;
        public event EventHandler<EventArgs> RecentChanged;
        public event EventHandler<EventArgs> UnreadChanged;

        #endregion

    }
}