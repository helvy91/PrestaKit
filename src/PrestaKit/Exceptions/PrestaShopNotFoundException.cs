using PrestaKit.Errors;
using System.Net;

namespace PrestaKit.Exceptions
{
    /// <summary>Thrown when a requested resource does not exist (404).</summary>
    public sealed class PrestaShopNotFoundException : PrestaShopApiException
    {
        /// <summary>Creates a new <see cref="PrestaShopNotFoundException"/>.</summary>
        public PrestaShopNotFoundException(
            HttpStatusCode httpStatusCode,
            IReadOnlyList<PrestaShopError> errors,
            string? responseBody = null,
            Uri? requestUri = null) : base(httpStatusCode, errors, responseBody, requestUri) { }
    }
}
