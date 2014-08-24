using System;
using System.Collections.Generic;
using Moq;
using MvcMonitor.Data.Providers;
using MvcMonitor.Data.Repositories;
using MvcMonitor.Models;
using NUnit.Framework;

namespace MvcMonitor.Tests.Providers.IndexProviderTests
{
    [TestFixture]
    public class GetErrorsTests
    {
        private Mock<IErrorRepositoryFactory> _mockErrorRepositoryFactory;
        private int _pageNumber;
        private int _pageSize;
        private Mock<IErrorRepository> _mockErrorRepository;

        [SetUp]
        public void WhenGettingPagedErrorsForTheIndex()
        {
            _pageNumber = 4;
            _pageSize = 20;

            _mockErrorRepository = new Mock<IErrorRepository>();

            _mockErrorRepository
                .Setup( repo => repo.GetPaged(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(),
                                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new PagedList<ErrorModel>(0, 1, 100, new List<ErrorModel>()));

            _mockErrorRepositoryFactory = new Mock<IErrorRepositoryFactory>();
            _mockErrorRepositoryFactory
                .Setup(factory => factory.GetRepository())
                .Returns(_mockErrorRepository.Object);

            var provider = new IndexProvider(_mockErrorRepositoryFactory.Object);
            
            provider.GetErrors(_pageNumber, _pageSize, DateTime.Now.AddHours(-10), DateTime.Now, "lsndfsdf", "ksdfsdf", "ksffdf");
        }

        [Test]
        public void ThenTheRepositoryIsCreated()
        {
            _mockErrorRepositoryFactory.Verify(repo => repo.GetRepository());
        }

        [Test]
        public void ThenTheRepositoryFetchesTheErrorsWithCorrectStartIndexAndPageSize()
        {
            var expectedStartIndex = (_pageNumber - 1)*_pageSize;

            _mockErrorRepository.Verify(repo => repo.GetPaged(expectedStartIndex, _pageSize, 
                It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
        }
    }
}
