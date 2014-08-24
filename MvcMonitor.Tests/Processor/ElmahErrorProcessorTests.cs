using Moq;
using MvcMonitor.Broadcaster;
using MvcMonitor.Data.Repositories;
using MvcMonitor.ErrorHandling;
using MvcMonitor.Models;
using MvcMonitor.Models.Factories;
using NUnit.Framework;

namespace MvcMonitor.Tests.Processor
{
    [TestFixture]
    public class ElmahErrorProcessorTests
    {
        private Mock<IErrorModelFactory> _mockErrorModelFactory;
        private ElmahErrorRequest _elmahErrorRequest;
        private ErrorModel _errorModel;
        private Mock<ISignalrBroadcasterFactory> _mockSignalrBroadcasterFactory;
        private Mock<ISignalrBroadcaster> _mockBroadcaster;
        private Mock<IErrorRepositoryFactory> _mockErrorRepositoryFactory;
        private Mock<IErrorRepository> _mockErrorRepository;

        [SetUp]
        public void WhenProcessingAnError()
        {
            _elmahErrorRequest = new ElmahErrorRequest("", "", "", new ElmahErrorDetailDto());
            _errorModel = new ErrorModel();

            _mockErrorModelFactory = new Mock<IErrorModelFactory>();
            _mockErrorModelFactory.Setup(factory => factory.Create(It.IsAny<ElmahErrorRequest>())).Returns(_errorModel);

            _mockBroadcaster = new Mock<ISignalrBroadcaster>();

            _mockSignalrBroadcasterFactory = new Mock<ISignalrBroadcasterFactory>();
            _mockSignalrBroadcasterFactory.Setup(factory => factory.Create()).Returns(_mockBroadcaster.Object);

            _mockErrorRepository = new Mock<IErrorRepository>();

            _mockErrorRepositoryFactory = new Mock<IErrorRepositoryFactory>();
            _mockErrorRepositoryFactory
                .Setup(factory => factory.GetRepository())
                .Returns(_mockErrorRepository.Object);

            var errorProcessor = new ErrorProcessor(
                _mockErrorModelFactory.Object, 
                _mockSignalrBroadcasterFactory.Object,
                _mockErrorRepositoryFactory.Object);
            
            errorProcessor.ProcessElmahError(_elmahErrorRequest);
        }

        [Test]
        public void ThenTheErrorModelTranslatorCreatesTheErrorModel()
        {
            _mockErrorModelFactory.Verify(trans => trans.Create(_elmahErrorRequest));
        }

        [Test]
        public void ThenTheRepositoryIsCreated()
        {
            _mockErrorRepositoryFactory.Verify(repo => repo.GetRepository());
        }

        [Test]
        public void ThenTheErrorIsPersisted()
        {
            _mockErrorRepository.Verify(repo => repo.Add(_errorModel));
        }

        [Test]
        public void ThenTheSignalrBroadcasterIsCreated()
        {
            _mockSignalrBroadcasterFactory.Verify(factory => factory.Create());
        }

        [Test]
        public void ThenTheErrorIsBroadcast()
        {
            _mockBroadcaster.Verify(broadcaster => broadcaster.ErrorReceived(_errorModel));
        }
    }
}
