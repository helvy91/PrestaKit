using System.ComponentModel.DataAnnotations;

namespace PrestaKit
{
    public sealed class PrestaShopClientOptions
    {
        [Required] public Uri? BaseUrl { get; set; }
        [Required(AllowEmptyStrings = false)] public string? ApiKey { get; set; }
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);
    }
}
