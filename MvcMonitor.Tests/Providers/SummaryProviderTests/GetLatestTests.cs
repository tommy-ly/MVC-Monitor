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
    public class GetLatestTests
    {
        private Mock<IErrorRepository> _mockErrorRepository;
        private ErrorModel _result;
        private ErrorModel _repositoryResponseItem;
        private DateTime _utcNow;

        [SetUp]
        public void WhenFetchingLatestErrorForAllApplications()
        {
            _repositoryResponseItem = new ErrorModel {Id = Guid.NewGuid()};

            _utcNow = new DateTime(2014, 5, 28, 10, 0, 0);

            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider
                .Setup(provider => provider.UtcNow())
                .Returns(_utcNow);

            _mockErrorRepository = new Mock<IErrorRepository>();
            _mockErrorRepository
                .Setup(repo => repo.GetPaged(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), 
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new PagedList<ErrorModel>(1, 1, 1, new List<ErrorModel> { _repositoryResponseItem }));

            _result = new SummaryProvider(_mockErrorRepository.Object, dateTimeProvider.Object, null).GetLatestError();
        }

        [Test]
        public void ThenTheErrorRespositoryIsCalledWithCorrectParameters()
        {
            var expectedStartDate = _utcNow.AddHours(-24);

            _mockErrorRepository.Verify(repo => repo.GetPaged(0, 1, expectedStartDate, _utcNow, null, null, null));
        }

        [Test]
        public void ThenTheErrorIsReturned()
        {
            Assert.That(_result, Is.EqualTo(_repositoryResponseItem));
        }
    }

    [TestFixture]
    public class GetLatestWhenNoErrorsExistTests
    {
        private Mock<IErrorRepository> _mockErrorRepository;
        private ErrorModel _result;
        private DateTime _utcNow;

        [SetUp]
        public void WhenFetchingLatestErrorForAllApplicationsAndNoErrorsExist()
        {
            _utcNow = new DateTime(2014, 5, 28, 10, 0, 0);

            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider
                .Setup(provider => provider.UtcNow())
                .Returns(_utcNow);

            _mockErrorRepository = new Mock<IErrorRepository>();
            _mockErrorRepository
                .Setup(repo => repo.GetPaged(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(),
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new PagedList<ErrorModel>(1, 1, 0, new List<ErrorModel>()));

            _result = new SummaryProvider(_mockErrorRepository.Object, dateTimeProvider.Object, null).GetLatestError();
        }

        [Test]
        public void ThenTheErrorRespositoryIsCalledWithCorrectParameters()
        {
            var expectedFromDate = _utcNow.AddHours(-24);
            _mockErrorRepository.Verify(repo => repo.GetPaged(0, 1, expectedFromDate, _utcNow, null, null, null));
        }

        [Test]
        public void ThenNullIsReturned()
        {
            Assert.That(_result, Is.Null);
        }
    }


    [TestFixture]
    public class GetLatestForApplicationTests
    {
        private Mock<IErrorRepository> _mockErrorRepository;
        private ErrorModel _result;
        private ErrorModel _repositoryResponseItem;
        private string _application;
        private DateTime _utcNow;

        [SetUp]
        public void WhenFetchingLatestErrorForAllApplications()
        {
            _application = "gdsffefdf";

            _repositoryResponseItem = new ErrorModel { Id = Guid.NewGuid() };

            _utcNow = new DateTime(2014, 5, 28, 10, 0, 0);
            
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider
                .Setup(provider => provider.UtcNow())
                .Returns(_utcNow);

            _mockErrorRepository = new Mock<IErrorRepository>();
            _mockErrorRepository
                .Setup(repo => repo.GetPaged(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(),
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new PagedList<ErrorModel>(1, 1, 1, new List<ErrorModel> { _repositoryResponseItem }));

            _result = new SummaryProvider(_mockErrorRepository.Object, dateTimeProvider.Object, null).GetLatestErrorForApplication(_application);
        }

        [Test]
        public void ThenTheErrorRespositoryIsCalledWithCorrectParameters()
        {
            var expectedFromDate = _utcNow.AddHours(-24);

            _mockErrorRepository.Verify(repo => repo.GetPaged(0, 1, expectedFromDate, _utcNow, _application, null, null));
        }

        [Test]
        public void ThenTheErrorIsReturned()
        {
            Assert.That(_result, Is.EqualTo(_repositoryResponseItem));
        }
    }

    [TestFixture]
    public class GetLatestForApplicationWhenNoErrorsExistTests
    {
        private Mock<IErrorRepository> _mockErrorRepository;
        private ErrorModel _result;
        private string _application;
        private DateTime _utcNow;

        [SetUp]
        public void WhenFetchingLatestErrorForAllApplicationsAndNoErrorsExist()
        {
            _application = "lsfsdfsdwer";

            _utcNow = new DateTime(2014, 5, 28, 10, 0, 0);

            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider
                .Setup(provider => provider.UtcNow())
                .Returns(_utcNow);

            _mockErrorRepository = new Mock<IErrorRepository>();
            _mockErrorRepository
                .Setup(repo => repo.GetPaged(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(),
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new PagedList<ErrorModel>(1, 1, 0, new List<ErrorModel>()));

            _result = new SummaryProvider(_mockErrorRepository.Object, dateTimeProvider.Object, null).GetLatestErrorForApplication(_application);
        }

        [Test]
        public void ThenTheErrorRespositoryIsCalledWithCorrectParameters()
        {
            var expectedFromDate = _utcNow.AddHours(-24);
            _mockErrorRepository.Verify(repo => repo.GetPaged(0, 1, expectedFromDate, _utcNow, _application, null, null));
        }

        [Test]
        public void ThenNullIsReturned()
        {
            Assert.That(_result, Is.Null);
        }
    }
}
