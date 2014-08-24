using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using Moq;
using MvcMonitor.Models;
using MvcMonitor.Models.Factories;
using MvcMonitor.StackTrace;
using NUnit.Framework;

namespace MvcMonitor.Tests.Models
{
    [TestFixture]
    public class ErrorModelFactoryTests
    {
        private ErrorModel _result;
        private string _errorId;
        private string _sourceApplicationId;
        private string _infoUrl;
        private string _exceptionMessage;
        private string _exceptionStackTrace;
        private string _fullExceptionType;
        private string _exceptionSource;
        private string _host;
        private string _requestMethod;
        private string _serverApplicationPath;
        private string _applicationPathTranslated;
        private string _serverName;
        private int _serverPort;
        private string _serverPortSecure;
        private string _time;
        private string _url;
        private string _userAgent;
        private string _username;
        private string _httpStatusCode;
        private Mock<IStatusCodeFactory> _mockStatusCodeFactory;
        private HttpStatusCode _statusCode;
        private string _queryString;
        private string _exceptionType;
        private Mock<IStackTraceProcessor> _mockStackTraceProcessor;
        private List<string> _localLocations;

        [SetUp]
        public void WhenBuildingModelFromDto()
        {
            _sourceApplicationId = "sdfefsa";
            _errorId = Guid.NewGuid().ToString();
            _exceptionMessage = "sdkjfsdfasdaw";
            _infoUrl = "sdfsdfwesf";
            _exceptionSource = "laskdwq";
            _exceptionStackTrace = "sodfsdlfmkw ;lasd;akdd";
            _exceptionType = "THIS_TYPE";
            _fullExceptionType = "sdklfsdfsdfwe.exc." + _exceptionType;
            _host = "lsakjdwada";
            _requestMethod = "skljfsseded";
            _serverApplicationPath = "fsdfdfweff";
            _applicationPathTranslated = "slfksefadwqedq";
            _serverName = "ksdwdasda";
            _serverPort = 4564;
            _serverPortSecure = "sdkfnsdfwed";
            _time = DateTime.UtcNow.ToString();
            _url = "sldkfwesadffse";
            _userAgent = "lsnsefawdqwew";
            _username = "skldfwqeqwe";
            _httpStatusCode = "04935";
            _statusCode = HttpStatusCode.PaymentRequired;
            _queryString = "skdjfnsdfsd";

            _localLocations = new List<string> { "location1", "location2" };

            var serverVariablesDto = new ServerVariablesDto
                {
                    APPL_PHYSICAL_PATH = _serverApplicationPath,
                    PATH_TRANSLATED = _applicationPathTranslated,
                    REQUEST_METHOD = _requestMethod,
                    SERVER_NAME = _serverName,
                    SERVER_PORT = _serverPort.ToString(CultureInfo.InvariantCulture),
                    SERVER_PORT_SECURE = _serverPortSecure,
                    URL = _url,
                    HTTP_USER_AGENT = _userAgent,
                    QUERY_STRING = _queryString
                };

            var elmahErrorDetailDto = new ElmahErrorDetailDto
                {
                    message = _exceptionMessage,
                    detail = _exceptionStackTrace,
                    source = _exceptionSource,
                    type = _fullExceptionType,
                    host = _host,
                    time = _time,
                    serverVariables = serverVariablesDto,
                    user = _username,
                    statusCode = _httpStatusCode
                };

            var elmahErrorDto = new ElmahErrorRequest(_errorId, _sourceApplicationId, _infoUrl, elmahErrorDetailDto);

            _mockStatusCodeFactory = new Mock<IStatusCodeFactory>();
            _mockStatusCodeFactory.Setup(factory => factory.Create(It.IsAny<string>())).Returns(_statusCode);

            _mockStackTraceProcessor = new Mock<IStackTraceProcessor>();
            _mockStackTraceProcessor.Setup(processor => processor.GetLocalLocations(It.IsAny<string>()))
                                    .Returns(new StackTraceLocationResult() {Locations = _localLocations});

            _result = new ErrorModelFactory(_mockStatusCodeFactory.Object, _mockStackTraceProcessor.Object)
                .Create(elmahErrorDto);
        }
        
        [Test]
        public void ThenTheApplicationIsSet()
        {
            Assert.That(_result.Application, Is.EqualTo(_sourceApplicationId));
        }

        [Test]
        public void ThenTheErrorIdIsSet()
        {
            Assert.That(_result.ErrorId, Is.EqualTo(new Guid(_errorId)));
        }

        [Test]
        public void ThenTheExceptionMessageIdIsSet()
        {
            Assert.That(_result.ExceptionMessage, Is.EqualTo(_exceptionMessage));
        }

        [Test]
        public void ThenTheExceptionSourceIsSet()
        {
            Assert.That(_result.ExceptionSource, Is.EqualTo(_exceptionSource));
        }

        [Test]
        public void ThenTheExceptionStackTraceIsSet()
        {
            Assert.That(_result.ExceptionStackTrace, Is.EqualTo(_exceptionStackTrace));
        }

        [Test]
        public void ThenTheExceptionTypeIsSet()
        {
            Assert.That(_result.ExceptionType, Is.EqualTo(_exceptionType));
        }

        [Test]
        public void ThenTheHostIsSet()
        {
            Assert.That(_result.Host, Is.EqualTo(_host));
        }

        [Test]
        public void ThenTheQueryStringIsSet()
        {
            Assert.That(_result.QueryString, Is.EqualTo(_queryString));
        }

        [Test]
        public void ThenTheRequestMethodIsSet()
        {
            Assert.That(_result.RequestMethod, Is.EqualTo(_requestMethod));
        }

        [Test]
        public void ThenTheServerApplicationPathIsSet()
        {
            Assert.That(_result.ServerApplicationPath, Is.EqualTo(_serverApplicationPath));
        }

        [Test]
        public void ThenThePathTranslatedIsSet()
        {
            Assert.That(_result.ServerApplicationPathTranslated, Is.EqualTo(_applicationPathTranslated));
        }

        [Test]
        public void ThenTheServerNameIsSet()
        {
            Assert.That(_result.ServerName, Is.EqualTo(_serverName));
        }

        [Test]
        public void ThenTheServerPortIsSet()
        {
            Assert.That(_result.ServerPort, Is.EqualTo(_serverPort));
        }

        [Test]
        public void ThenTheStatusCodeFactoryCreatesTheStatusCode()
        {
            _mockStatusCodeFactory.Verify(factory => factory.Create(_httpStatusCode));
        }

        [Test]
        public void ThenTheStatusCodeIsSet()
        {
            Assert.That(_result.StatusCode, Is.EqualTo(_statusCode));
        }

        [Test]
        public void ThenTheServerPortSecureIsSet()
        {
            Assert.That(_result.ServerPortSecure, Is.EqualTo(_serverPortSecure));
        }

        [Test]
        public void ThenTheTimeIsSet()
        {
            Assert.That(_result.Time, Is.EqualTo(DateTime.Parse(_time).ToUniversalTime()));
        }

        [Test]
        public void ThenTheUrlIsSet()
        {
            Assert.That(_result.Url, Is.EqualTo(_url));
        }

        [Test]
        public void ThenTheUserAgentIsSet()
        {
            Assert.That(_result.UserAgent, Is.EqualTo(_userAgent));
        }

        [Test]
        public void ThenTheUsernameIsSet()
        {
            Assert.That(_result.Username, Is.EqualTo(_username));
        }

        [Test]
        public void ThenTheLocationIsFetchedFromTheStackTrace()
        {
            _mockStackTraceProcessor.Verify(processor => processor.GetLocalLocations(_exceptionStackTrace));
        }

        [Test]
        public void ThenTheLocationPropertyIsSet()
        {
            Assert.That(_result.ExceptionLocations, Is.EqualTo(_localLocations));
        }
    }
}
