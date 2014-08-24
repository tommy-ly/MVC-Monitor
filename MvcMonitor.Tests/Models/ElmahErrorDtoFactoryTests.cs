using System;
using System.Text;
using Moq;
using MvcMonitor.Models;
using MvcMonitor.Models.Factories;
using NUnit.Framework;

namespace MvcMonitor.Tests.Models
{
    public class ElmahErrorDtoFactoryTests
    {
        private string _encodedErrorParameter;
        private Mock<IElmahErrorDetailDtoFactory> _mockElmahErrorDetailFactory;
        private ElmahErrorDetailDto _errorDetails;
        private ElmahErrorRequest _errorRequest;
        private string _errorId;
        private string _sourceId;
        private string _infoUrl;
        private string _decodedErrorParameter;

        [SetUp]
        public void WhenCreatingAnElmahErrorModel()
        {
            _errorId = "sdflkdfgg";
            _sourceId = "lsdkjfsdf";
            _decodedErrorParameter = "lsdfkdf";
            _encodedErrorParameter = Convert.ToBase64String(Encoding.UTF8.GetBytes(_decodedErrorParameter));
            _infoUrl = "lsdjkfsdfd";
            _errorDetails = new ElmahErrorDetailDto();

            _mockElmahErrorDetailFactory = new Mock<IElmahErrorDetailDtoFactory>();
            _mockElmahErrorDetailFactory.Setup(factory => factory.Create(It.IsAny<string>())).Returns(_errorDetails);

            _errorRequest = new ElmahErrorDtoFactory(_mockElmahErrorDetailFactory.Object).Create(_errorId, _sourceId, _infoUrl, _encodedErrorParameter);
        }

        [Test]
        public void ThenTheErrorDetailsAreCreated()
        {
            _mockElmahErrorDetailFactory.Verify(factory => factory.Create(_decodedErrorParameter));
        }

        [Test]
        public void ThenTheErrorDetailsAreSetInTheErrorModel()
        {
            Assert.That(_errorRequest.Error, Is.EqualTo(_errorDetails));
        }

        [Test]
        public void ThenTheErrorIdIsSetInTheErrorModel()
        {
            Assert.That(_errorRequest.ErrorId, Is.EqualTo(_errorId));
        }

        [Test]
        public void ThenTheSourceApplicationIdIsSetInTheErrorModel()
        {
            Assert.That(_errorRequest.SourceApplicationId, Is.EqualTo(_sourceId));
        }

        [Test]
        public void ThenTheInfoUrlIsSetInTheErrorModel()
        {
            Assert.That(_errorRequest.InfoUrl, Is.EqualTo(_infoUrl));
        }
    }
}
