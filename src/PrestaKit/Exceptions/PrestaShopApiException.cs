using PrestaKit.Errors;
using System.Net;

namespace PrestaKit.Exceptions
{
    /// <summary>Thrown when the PrestaShop API rejects a request (non-success status).</summary>
    public class PrestaShopApiException : PrestaShopException
    {
        /// <summary>The HTTP status code returned by PrestaShop.</summary>
        public HttpStatusCode HttpStatusCode { get; }

        /// <summary>The parsed errors returned by PrestaShop, if any.</summary>
        public IReadOnlyList<PrestaShopError> Errors { get; }

        /// <summary>The raw response body, when available. Useful for failures that carry no structured errors.</summary>
        public string? ResponseBody { get; }

        /// <summary>The URI of the request that failed, when available.</summary>
        public Uri? RequestUri { get; }

        /// <summary>Creates a new <see cref="PrestaShopApiException"/>.</summary>
        /// <param name="httpStatusCode">The HTTP status code returned by PrestaShop.</param>
        /// <param name="errors">The parsed errors, or an empty list if none.</param>
        /// <param name="responseBody">The raw response body, if available.</param>
        /// <param name="requestUri">The request URI, if available.</param>
        public PrestaShopApiException(
            HttpStatusCode httpStatusCode,
            IReadOnlyList<PrestaShopError> errors,
            string? responseBody = null,
            Uri? requestUri = null) : base(BuildMessage(httpStatusCode, errors, responseBody))
        {
            HttpStatusCode = httpStatusCode;
            Errors = errors;
            ResponseBody = responseBody;
            RequestUri = requestUri;
        }

        private static string BuildMessage(
            HttpStatusCode httpStatusCode,
            IReadOnlyList<PrestaShopError> errors,
            string? responseBody = null)
        {
            if (!errors.Any())
            {
                return $"PrestaShop request failed. API returned status code {(int)httpStatusCode} ({httpStatusCode}).";
            }

            var firstError = errors[0];
            var detail = firstError.Code.HasValue ? $"[{firstError.Code.Value}] {firstError.Message}" : firstError.Message;

            var message = errors.Count == 1 ? $"PrestaShop request failed ({(int)httpStatusCode}): {detail}" :
                                              $"PrestaShop request failed ({(int)httpStatusCode}): {detail} (+{errors.Count - 1} more)";
            return message;
        }
    }
}
