using PrestaKit.Entities;

namespace PrestaKit.Clients.Media.Attachments
{
    public interface IAttachmentFileClient   
    {
        Task<Attachment> UploadAsync(AttachmentUpload file, CancellationToken ct = default); 
        Task<byte[]> DownloadAsync(long id, CancellationToken ct = default);                
        Task<Attachment> ReplaceFileAsync(long id, AttachmentUpload file, CancellationToken ct = default);
    }
}
