namespace PrestaKit.Exceptions
{
    public class PrestaShopSerializationException : PrestaShopException
    {
        public string XmlContent { get; private set; }

        public PrestaShopSerializationException(string message, string xmlContent) 
            : base(message)
        {
            XmlContent = xmlContent;
        }

        public PrestaShopSerializationException(string message, string xmlContent, Exception innerException) 
            : base(message, innerException)
        {
            XmlContent = xmlContent;
        }
    }
}
