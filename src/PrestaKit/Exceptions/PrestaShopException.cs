namespace PrestaKit.Exceptions
{
    /// <summary>Base type for all exceptions thrown by PrestaKit.</summary>
    public abstract class PrestaShopException : Exception
    {
        /// <summary>Creates a new <see cref="PrestaShopException"/>.</summary>
        /// <param name="message">The error message.</param>
        protected PrestaShopException(string message) : base(message) { }

        /// <summary>Creates a new <see cref="PrestaShopException"/>.</summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The underlying exception that caused this one.</param>
        protected PrestaShopException(string message, Exception innerException) : base(message, innerException) { }
    }
}
