using System;
using System.Linq;
using MvcMonitor.StackTrace;

namespace MvcMonitor.Models.Factories
{
    public class ErrorModelFactory : IErrorModelFactory
    {
        private readonly IStatusCodeFactory _statusCodeFactory;
        private readonly IStackTraceProcessor _stackTraceProcessor;

        public ErrorModelFactory() : this(new StatusCodeFactory(), new StackTraceProcessor())
        {
        }

        public ErrorModelFactory(IStatusCodeFactory statusCodeFactory, IStackTraceProcessor stackTraceProcessor)
        {
            _statusCodeFactory = statusCodeFactory;
            _stackTraceProcessor = stackTraceProcessor;
        }

        public ErrorModel Create(ElmahErrorRequest elmahErrorRequest)
        {
            var statusCode = _statusCodeFactory.Create(elmahErrorRequest.Error.statusCode);

            var stacktrace = elmahErrorRequest.Error.detail;

            var stackTraceProcessResult = _stackTraceProcessor.GetLocalLocations(stacktrace);

            return new ErrorModel()
                {
                    Application = elmahErrorRequest.SourceApplicationId,
                    ErrorId = new Guid(elmahErrorRequest.ErrorId),
                    ExceptionLocations = stackTraceProcessResult.Locations,                   
                    ExceptionMessage = elmahErrorRequest.Error.message,
                    ExceptionSource = elmahErrorRequest.Error.source,
                    ExceptionStackTrace = stacktrace,
                    ExceptionType = elmahErrorRequest.Error.type.Split('.').Last(),
                    Host = elmahErrorRequest.Error.host,
                    QueryString = elmahErrorRequest.Error.serverVariables.QUERY_STRING,
                    RequestMethod = elmahErrorRequest.Error.serverVariables.REQUEST_METHOD,
                    ServerApplicationPath = elmahErrorRequest.Error.serverVariables.APPL_PHYSICAL_PATH,
                    ServerApplicationPathTranslated = elmahErrorRequest.Error.serverVariables.PATH_TRANSLATED,
                    ServerName = elmahErrorRequest.Error.serverVariables.SERVER_NAME,
                    ServerPort = int.Parse(elmahErrorRequest.Error.serverVariables.SERVER_PORT),
                    ServerPortSecure = elmahErrorRequest.Error.serverVariables.SERVER_PORT_SECURE,
                    StatusCode = statusCode,
                    Time = DateTime.Parse(elmahErrorRequest.Error.time).ToUniversalTime(),
                    Url = elmahErrorRequest.Error.serverVariables.URL,
                    UserAgent = elmahErrorRequest.Error.serverVariables.HTTP_USER_AGENT,
                    Username = elmahErrorRequest.Error.user
                };
        }
    }
}