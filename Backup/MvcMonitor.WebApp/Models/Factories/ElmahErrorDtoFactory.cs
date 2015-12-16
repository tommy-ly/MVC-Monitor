using System;
using System.Text;

namespace MvcMonitor.Models.Factories
{
    public class ElmahErrorDtoFactory : IElmahErrorDtoFactory
    {
        private readonly IElmahErrorDetailDtoFactory _elmahErrorDetailDtoFactory;

        public ElmahErrorDtoFactory() : this(new ElmahErrorDetailDtoFactory())
        {
        }

        public ElmahErrorDtoFactory(IElmahErrorDetailDtoFactory elmahErrorDetailDtoFactory)
        {
            _elmahErrorDetailDtoFactory = elmahErrorDetailDtoFactory;
        }

        public ElmahErrorRequest Create(string errorId, string sourceApplicationId, string infoUrl, string errorDetails)
        {
            var errorDetailModel = _elmahErrorDetailDtoFactory.Create(FromBase64(errorDetails));

            return new ElmahErrorRequest(errorId, sourceApplicationId, infoUrl, errorDetailModel);
        }

        private static string FromBase64(string errorDetails)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(errorDetails));
        }
    }
}