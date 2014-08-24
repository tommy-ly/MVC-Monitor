using System;
using System.Collections.Generic;
using Moq;
using MvcMonitor.Data.Providers;
using MvcMonitor.Data.Repositories;
using MvcMonitor.Models;
using MvcMonitor.Utilities;
using NUnit.Framework;

namespace MvcMonitor.Tests.Providers.SummaryProviderTests
{
    [TestFixture]
    public class GetTotalErrorCountTests
    {
        private Mock<IErrorRepository> _mockErrorRepository;
        private ErrorModel _repositoryResponseItem;
        private int _repoErrorCount;
        private int _result;
        private Mock<IDateTimeProvider> _mockDateTimeProvider;
        private DateTime _utcNow;

        [SetUp]
        public void WhenFetchingLatestErrorForAllApplications()
        {
            _utcNow = new DateTime(2014, 2, 19, 12, 30, 0, 0);
            _repositoryResponseItem = new ErrorModel {Id = Guid.NewGuid()};

            _mockDateTimeProvider = new Mock<IDateTimeProvider>();
            _mockDateTimeProvider
                .Setup(provider => provider.UtcNow())
                .Returns(_utcNow);

            _mockErrorRepository = new Mock<IErrorRepository>();
            _repoErrorCount = 123;
            _mockErrorRepository
                .Setup(repo => repo.GetPaged(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), 
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new PagedList<ErrorModel>(1, 1, _repoErrorCount, new List<ErrorModel> { _repositoryResponseItem }));

            _result = new SummaryProvider(_mockErrorRepository.Object, _mockDateTimeProvider.Object, null).GetTotalErrorCount();
        }

        [Test]
        public void ThenTheUtcNowTimeIsFetched()
        {
            _mockDateTimeProvider.Verify(provider => provider.UtcNow());
        }

        [Test]
        public void ThenTheErrorRespositoryIsCalledWithCorrectParameters()
        {
            _mockErrorRepository.Verify(repo => repo.GetPaged(0, 1, _utcNow.AddHours(-24), _utcNow, null, null, null));
        }

        [Test]
        public void ThenTheErrorIsReturned()
        {
            Assert.That(_result, Is.EqualTo(_repoErrorCount));
        }
    }

    [TestFixture]
    public class GetTotalErrorCountForApplicationTests
    {
        private Mock<IErrorRepository> _mockErrorRepository;
        private ErrorModel _repositoryResponseItem;
        private int _repoErrorCount;
        private int _result;
        private Mock<IDateTimeProvider> _mockDateTimeProvider;
        private DateTime _utcNow;
        private string _applicationName;

        [SetUp]
        public void WhenFetchingLatestErrorForAllApplications()
        {
            _applicationName = "sfsdkfnsdf";
            _utcNow = new DateTime(2014, 2, 19, 12, 30, 0, 0);
            _repositoryResponseItem = new ErrorModel { Id = Guid.NewGuid() };

            _mockDateTimeProvider = new Mock<IDateTimeProvider>();
            _mockDateTimeProvider
                .Setup(provider => provider.UtcNow())
                .Returns(_utcNow);

            _mockErrorRepository = new Mock<IErrorRepository>();
            _repoErrorCount = 123;
            _mockErrorRepository
                .Setup(repo => repo.GetPaged(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(),
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new PagedList<ErrorModel>(1, 1, _repoErrorCount, new List<ErrorModel> { _repositoryResponseItem }));

            _result = new SummaryProvider(_mockErrorRepository.Object, _mockDateTimeProvider.Object, null).GetTotalErrorCountForApplication(_applicationName);
        }

        [Test]
        public void ThenTheUtcNowTimeIsFetched()
        {
            _mockDateTimeProvider.Verify(provider => provider.UtcNow());
        }

        [Test]
        public void ThenTheErrorRespositoryIsCalledWithCorrectParameters()
        {
            _mockErrorRepository.Verify(repo => repo.GetPaged(0, 1, _utcNow.AddHours(-24), _utcNow, _applicationName, null, null));
        }

        [Test]
        public void ThenTheErrorIsReturned()
        {
            Assert.That(_result, Is.EqualTo(_repoErrorCount));
        }
    }
}
