using System.Net;

namespace MvcMonitor.Models.Factories
{
    public class StatusCodeFactory : IStatusCodeFactory
    {
        public HttpStatusCode Create(string httpStatusCode)
        {
            int statusCode;

            if (!int.TryParse(httpStatusCode, out statusCode))
            {
                return HttpStatusCode.Unused;
            }

            return (HttpStatusCode)statusCode;
        }
    }
}