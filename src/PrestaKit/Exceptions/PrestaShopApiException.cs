using PrestaKit.Errors;
using System.Net;

namespace PrestaKit.Exceptions
{
    public class PrestaShopApiException : PrestaShopException
    {
        public HttpStatusCode HttpStatusCode { get; private set; }
        public IReadOnlyList<PrestashopError> Errors { get; set; }
        public string? ResponseBody { get; set; }
        public Uri? RequestUri { get; set; }

        public PrestaShopApiException(
            HttpStatusCode httpStatusCode,
            IReadOnlyList<PrestashopError> errors,
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
            IReadOnlyList<PrestashopError> errors,
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
