namespace PrestaKit.Exceptions
{
    /// <summary>Thrown when a PrestaShop response cannot be parsed. Carries the raw payload.</summary>
    public sealed class PrestaShopSerializationException : PrestaShopException
    {
        /// <summary>The raw response content that failed to parse.</summary>
        public string XmlContent { get; }

        /// <summary>Creates a new <see cref="PrestaShopSerializationException"/>.</summary>
        /// <param name="message">The error message.</param>
        /// <param name="xmlContent">The raw response content that failed to parse.</param>
        public PrestaShopSerializationException(string message, string xmlContent)
            : base(message)
        {
            XmlContent = xmlContent;
        }

        /// <summary>Creates a new <see cref="PrestaShopSerializationException"/>.</summary>
        /// <param name="message">The error message.</param>
        /// <param name="xmlContent">The raw response content that failed to parse.</param>
        /// <param name="innerException">The underlying parsing exception.</param>
        public PrestaShopSerializationException(string message, string xmlContent, Exception innerException)
            : base(message, innerException)
        {
            XmlContent = xmlContent;
        }
    }
}
