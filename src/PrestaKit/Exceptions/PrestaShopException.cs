namespace PrestaKit.Exceptions
{
    public class PrestaShopException : Exception
    {
        public PrestaShopException(string message) : base(message) { }

        public PrestaShopException(string message, Exception innerException) : base(message, innerException) { }
    }
}
