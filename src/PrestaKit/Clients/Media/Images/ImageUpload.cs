namespace PrestaKit.Clients.Media.Images;

public sealed class ImageUpload
{
    public required Stream Content { get; init; }
    public required string FileName { get; init; }

    internal string ContentType => Path.GetExtension(FileName).ToLowerInvariant() switch
    {
        ".jpg" or ".jpeg" => "image/jpeg",
        ".png" => "image/png",
        ".gif" => "image/gif",
        ".webp" => "image/webp",
        _ => "application/octet-stream",
    };
}