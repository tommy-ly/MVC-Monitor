using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using Moq;
using MvcMonitor.ErrorHandling;
using MvcMonitor.HttpHandlers;
using MvcMonitor.Models;
using MvcMonitor.Models.Factories;
using NUnit.Framework;

namespace MvcMonitor.Tests.HttpHandlers
{
    [TestFixture]
    public class ElmahHttpHandlerTests
    {
        private string _errorParameter;
        private Mock<HttpContextBase> _mockHttpContext;
        private Mock<IErrorProcessor> _mockElmahErrorProcessor;
        private string _errorId;
        private string _applicationSourceId;
        private string _infoUrl;
        private Mock<IElmahErrorDtoFactory> _mockElmahErrorFactory;
        private ElmahErrorRequest _fullElmahError;

        [SetUp]
        public void WhenReceivingAnElmahError()
        {
            _errorId = "sldjkfs";
            _applicationSourceId = "lsdjkfsd";
            _infoUrl = "gfegsldkf";
            _errorParameter = "sdjsdaf";

            _fullElmahError = new ElmahErrorRequest(null, null, null, null);

            var requestParams = new NameValueCollection
                {
                    {"errorId", _errorId}, 
                    {"sourceId", _applicationSourceId}, 
                    {"infoUrl", _infoUrl}, 
                    {"error", _errorParameter}
                };

            _mockHttpContext = new Mock<HttpContextBase>();
            _mockHttpContext.Setup(context => context.Request.Params).Returns(requestParams);

            _mockElmahErrorFactory = new Mock<IElmahErrorDtoFactory>();
            _mockElmahErrorFactory.Setup(factory => factory.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(_fullElmahError);

            _mockElmahErrorProcessor = new Mock<IErrorProcessor>();

            var configuredApplications = new List<string> { _applicationSourceId };

            new ElmahHttpHandler(_mockElmahErrorProcessor.Object, _mockElmahErrorFactory.Object, configuredApplications).ProcessRequest(_mockHttpContext.Object);
        }

        [Test]
        public void ThenTheElmahErrorDtoIsCreated()
        {
            _mockElmahErrorFactory.Verify(errorFactory => errorFactory.Create(_errorId, _applicationSourceId, _infoUrl, _errorParameter));
        }

        [Test]
        public void ThenTheErrorIsProcessed()
        {
            _mockElmahErrorProcessor.Verify(processor => processor.ProcessElmahError(_fullElmahError));
        }
    }

    [TestFixture]
    public class ElmahHttpHandlerWhenErrorIsNotInConfiguredApplicationsTests
    {
        private string _errorParameter;
        private Mock<HttpContextBase> _mockHttpContext;
        private Mock<IErrorProcessor> _mockElmahErrorProcessor;
        private string _errorId;
        private string _applicationSourceId;
        private string _infoUrl;
        private Mock<IElmahErrorDtoFactory> _mockElmahErrorFactory;
        private ElmahErrorRequest _fullElmahError;

        [SetUp]
        public void WhenReceivingAnElmahErrorFromUnknownSource()
        {
            _errorId = "sldjkfs";
            _applicationSourceId = "lsdjkfsd";
            _infoUrl = "gfegsldkf";
            _errorParameter = "sdjsdaf";

            _fullElmahError = new ElmahErrorRequest(null, null, null, null);

            var requestParams = new NameValueCollection
                {
                    {"errorId", _errorId}, 
                    {"sourceId", _applicationSourceId}, 
                    {"infoUrl", _infoUrl}, 
                    {"error", _errorParameter}
                };

            _mockHttpContext = new Mock<HttpContextBase>();
            _mockHttpContext.Setup(context => context.Request.Params).Returns(requestParams);

            _mockElmahErrorFactory = new Mock<IElmahErrorDtoFactory>();
            _mockElmahErrorFactory.Setup(factory => factory.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(_fullElmahError);

            _mockElmahErrorProcessor = new Mock<IErrorProcessor>();

            var configuredApplications = new List<string> { "SomeOtherApplication" };

            new ElmahHttpHandler(_mockElmahErrorProcessor.Object, _mockElmahErrorFactory.Object, configuredApplications).ProcessRequest(_mockHttpContext.Object);
        }

        [Test]
        public void ThenTheElmahErrorDtoIsNotCreated()
        {
            _mockElmahErrorFactory.Verify(errorFactory => errorFactory.Create(_errorId, _applicationSourceId, _infoUrl, _errorParameter), Times.Never());
        }

        [Test]
        public void ThenTheErrorIsNotProcessed()
        {
            _mockElmahErrorProcessor.Verify(processor => processor.ProcessElmahError(_fullElmahError), Times.Never());
        }
    }
}
