using PrestaKit.Errors;
using System.Net;

namespace PrestaKit.Exceptions
{
    public class PrestaShopNotFoundException : PrestaShopApiException
    {
        public PrestaShopNotFoundException(
            HttpStatusCode httpStatusCode, 
            IReadOnlyList<PrestashopError> errors, 
            string? responseBody = null, 
            Uri? requestUri = null) : base(httpStatusCode, errors, responseBody, requestUri) { }
    }
}
