using System;
using System.Collections.Generic;
using Moq;
using MvcMonitor.Data.Providers;
using MvcMonitor.Data.Providers.Factories;
using MvcMonitor.ErrorHandling;
using MvcMonitor.Models;
using NUnit.Framework;

namespace MvcMonitor.Tests.ErrorHubTests
{
    [TestFixture]
    public class GetPagedErrorsTests
    {
        private Mock<IIndexProviderFactory> _mockIndexProviderFactory;
        private int _page;
        private int _pageSize;
        private string _filterFrom;
        private string _filterTo;
        private string _filterUser;
        private string _filterApplication;
        private string _filterLocation;
        private Mock<IIndexProvider> _mockIndexProvider;
        private PagedList<ErrorModel> _result;
        private PagedList<ErrorModel> _providerResult;

        [SetUp]
        public void WhenFetchingErrorsForTheIndexPage()
        {
            _page = 2;
            _pageSize = 10;
            _filterFrom = "2014-02-25";
            _filterTo = "2014-02-28";
            _filterUser = "skfjnsdf";
            _filterApplication = "sjfe";
            _filterLocation = "sfisbdf";

            _providerResult = new PagedList<ErrorModel>(10, 5, 100,
                                                        new List<ErrorModel> {new ErrorModel {Id = Guid.NewGuid()}});

            _mockIndexProvider = new Mock<IIndexProvider>();
            _mockIndexProvider
                .Setup(provider => provider.GetErrors(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<string>(), 
                    It.IsAny<string>(), It.IsAny<string>()))
                .Returns(_providerResult);

            _mockIndexProviderFactory = new Mock<IIndexProviderFactory>();
            _mockIndexProviderFactory
                .Setup(factory => factory.Create())
                .Returns(_mockIndexProvider.Object);

            var hub = new ErrorHub(null, null, _mockIndexProviderFactory.Object);

            
            _result = hub.GetPagedErrors(_page, _pageSize, _filterFrom, _filterTo, _filterUser, _filterApplication, _filterLocation);
        }

        [Test]
        public void ThenTheIndexProviderFetchesTheErrors()
        {
            _mockIndexProvider.Verify(provider => provider.GetErrors(_page, _pageSize,
                It.Is<DateTime>(dateFrom => dateFrom == DateTime.Parse(_filterFrom)), 
                It.Is<DateTime>(dateTo => dateTo.Year == 2014 && dateTo.Month == 2 && dateTo.Day == 28 && dateTo.Hour == 23 
                     && dateTo.Minute == 59 && dateTo.Second == 59),
                _filterUser, _filterApplication, _filterLocation));
        }

        [Test]
        public void ThenThePagedListIsReturned()
        {
            Assert.That(_result, Is.EqualTo(_providerResult));
        }
    }

    [TestFixture("")]
    [TestFixture((string)null)]
    [TestFixture("INVALID_$GG09gegRGjw9rs££56")]
    public class GetPagedErrorsWithNoDateFilterTests
    {
        private readonly string _emptyDateField;
        private Mock<IIndexProviderFactory> _mockIndexProviderFactory;
        private int _page;
        private int _pageSize;
        private string _filterFrom;
        private string _filterTo;
        private string _filterUser;
        private string _filterApplication;
        private string _filterLocation;
        private Mock<IIndexProvider> _mockIndexProvider;
        private PagedList<ErrorModel> _result;
        private PagedList<ErrorModel> _providerResult;

        public GetPagedErrorsWithNoDateFilterTests(string emptyDateField)
        {
            _emptyDateField = emptyDateField;
        }

        [SetUp]
        public void WhenFetchingErrorsForTheIndexPage()
        {
            _page = 2;
            _pageSize = 10;
            _filterFrom = _emptyDateField;
            _filterTo = _emptyDateField;
            _filterUser = "skfjnsdf";
            _filterApplication = "sjfe";
            _filterLocation = "sfisbdf";

            _providerResult = new PagedList<ErrorModel>(10, 5, 100,
                                                        new List<ErrorModel> { new ErrorModel { Id = Guid.NewGuid() } });

            _mockIndexProvider = new Mock<IIndexProvider>();
            _mockIndexProvider
                .Setup(provider => provider.GetErrors(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<string>()))
                .Returns(_providerResult);

            _mockIndexProviderFactory = new Mock<IIndexProviderFactory>();
            _mockIndexProviderFactory
                .Setup(factory => factory.Create())
                .Returns(_mockIndexProvider.Object);

            var hub = new ErrorHub(null, null, _mockIndexProviderFactory.Object);


            _result = hub.GetPagedErrors(_page, _pageSize, _filterFrom, _filterTo, _filterUser, _filterApplication, _filterLocation);
        }

        [Test]
        public void ThenTheIndexProviderFetchesTheErrorsWithNullDateFilters()
        {
            _mockIndexProvider.Verify(provider => provider.GetErrors(_page, _pageSize, null, null, _filterUser, _filterApplication, _filterLocation));
        }

        [Test]
        public void ThenThePagedListIsReturned()
        {
            Assert.That(_result, Is.EqualTo(_providerResult));
        }
    }
}
