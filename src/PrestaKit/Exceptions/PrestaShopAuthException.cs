using PrestaKit.Errors;
using System.Net;

namespace PrestaKit.Exceptions
{
    /// <summary>
    /// Thrown when the API key is missing, invalid, or lacks permission for the
    /// requested resource (HTTP 401 or 403).
    /// </summary>
    public sealed class PrestaShopAuthException : PrestaShopApiException
    {
        /// <summary>Creates a new <see cref="PrestaShopAuthException"/>.</summary>
        /// <param name="httpStatusCode">The HTTP status code returned by PrestaShop (401 or 403).</param>
        /// <param name="errors">The parsed errors, or an empty list if none.</param>
        /// <param name="responseBody">The raw response body, if available.</param>
        /// <param name="requestUri">The request URI, if available.</param>
        public PrestaShopAuthException(
            HttpStatusCode httpStatusCode, 
            IReadOnlyList<PrestaShopError> errors, 
            string? responseBody = null, 
            Uri? requestUri = null) : base(httpStatusCode, errors, responseBody, requestUri)
        {
        }
    }
}
