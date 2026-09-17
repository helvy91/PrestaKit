using PrestaKit.Errors;
using System.Net;

namespace PrestaKit.Exceptions
{
    public class PrestaShopAuthException : PrestaShopApiException
    {
        public PrestaShopAuthException(
            HttpStatusCode httpStatusCode, 
            IReadOnlyList<PrestashopError> errors, 
            string? responseBody = null, 
            Uri? requestUri = null) : base(httpStatusCode, errors, responseBody, requestUri)
        {
        }
    }
}
