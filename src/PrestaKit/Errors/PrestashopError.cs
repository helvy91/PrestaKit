namespace PrestaKit.Errors
{
    /// <summary>A single error returned by PrestaShop in a failed API response.</summary>
    public sealed class PrestaShopError
    {
        /// <summary>The PrestaShop error code, if provided.</summary>
        public int? Code { get; set; }

        /// <summary>The error message.</summary>
        public string? Message { get; set; }
    }
}
