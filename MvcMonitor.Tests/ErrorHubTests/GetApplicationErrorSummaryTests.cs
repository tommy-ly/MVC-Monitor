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
    public class GetApplicationErrorSummaryTests
    {
        private Mock<ISummaryProviderFactory> _mockSummaryProviderFactory;
        private Mock<ISummaryProvider> _mockSummaryProvider;
        private List<string> _applications;
        private ErrorModel _latestErrorForAllApplications;
        private ErrorSummaryCollection _applicationSummery;
        private int _totalErrorCountForAllApplications;
        private List<int> _applicationErrorCounts;
        private List<ErrorModel> _applicationErrors;

        [SetUp]
        public void WhenGettingTheApplicationErrorSummaries()
        {
            _totalErrorCountForAllApplications = 3;
            _latestErrorForAllApplications = new ErrorModel();

            _applications = new List<string>()
                {
                    "asdjkhasdad",
                    "asdasjdbs",
                    "sdnxcgs"
                };

            _applicationErrorCounts = new List<int>()
                {
                    10,
                    20,
                    30
                };

            _applicationErrors = new List<ErrorModel>
                {
                    new ErrorModel() {Time = new DateTime(2014, 12, 1), ExceptionType = "sjdfbsdf"},
                    new ErrorModel() {Time = new DateTime(2013, 12, 1), ExceptionType = "fhsdfsda"},
                    new ErrorModel() {Time = new DateTime(2012, 12, 1), ExceptionType = "ksjndngg"}
                };

            _mockSummaryProvider = new Mock<ISummaryProvider>();
            
            _mockSummaryProvider
                .Setup(provider => provider.GetLatestError())
                .Returns(_latestErrorForAllApplications);

            _mockSummaryProvider
                .Setup(provider => provider.GetTotalErrorCount())
                .Returns(_totalErrorCountForAllApplications);

            _mockSummaryProvider
                .Setup(provider => provider.GetTotalErrorCountForApplication(_applications[0]))
                .Returns(_applicationErrorCounts[0]);

            _mockSummaryProvider
                .Setup(provider => provider.GetTotalErrorCountForApplication(_applications[1]))
                .Returns(_applicationErrorCounts[1]);

            _mockSummaryProvider
                .Setup(provider => provider.GetTotalErrorCountForApplication(_applications[2]))
                .Returns(_applicationErrorCounts[2]);

            _mockSummaryProvider
                .Setup(provider => provider.GetLatestErrorForApplication(_applications[0]))
                .Returns(_applicationErrors[0]);

            _mockSummaryProvider
                .Setup(provider => provider.GetLatestErrorForApplication(_applications[1]))
                .Returns(_applicationErrors[1]);

            _mockSummaryProvider
                .Setup(provider => provider.GetLatestErrorForApplication(_applications[2]))
                .Returns(_applicationErrors[2]);


            _mockSummaryProviderFactory = new Mock<ISummaryProviderFactory>();
            _mockSummaryProviderFactory
                .Setup(factory => factory.Create())
                .Returns(_mockSummaryProvider.Object);
            
            _applicationSummery = new ErrorHub(_mockSummaryProviderFactory.Object,_applications, null).GetApplicationErrorSummary();
            
        }

        [Test]
        public void ThenTheLatestErrorIsFetched()
        {
            _mockSummaryProvider.Verify(provider => provider.GetLatestError());
        }

        [Test]
        public void ThenTheTotalNumberOfErrorsIsFetched()
        {
            _mockSummaryProvider.Verify(provider => provider.GetTotalErrorCount());
        }

        [Test]
        public void ThenTheLatestErrorIsFetchedForEachApplication()
        {
            foreach (var application in _applications)
            {
                _mockSummaryProvider.Verify(provider => provider.GetLatestErrorForApplication(application));
            }
        }

        [Test]
        public void ThenTheLatestErrorForAllApplicationsIsPopulated()
        {
            Assert.That(_applicationSummery.LatestError, Is.EqualTo(_latestErrorForAllApplications));
        }

        [Test]
        public void ThenTheTotalNumberOfErrorsIsCorrect()
        {
            Assert.That(_applicationSummery.TotalCount , Is.EqualTo(_totalErrorCountForAllApplications));
        }

        [Test]
        public void ThenTheCountOfApplicationErrorSummariesIsCorrect()
        {
            Assert.That(_applicationSummery.ErrorSummaries.Count, Is.EqualTo(_applications.Count));
        }

        [Test]
        public void ThenTheApplicationErrorSummariesContainsCorrectApplicationName()
        {
                Assert.That(_applicationSummery.ErrorSummaries[0].Application, Is.EqualTo(_applications[0]));
                Assert.That(_applicationSummery.ErrorSummaries[1].Application, Is.EqualTo(_applications[1]));
                Assert.That(_applicationSummery.ErrorSummaries[2].Application, Is.EqualTo(_applications[2]));
        }

        [Test]
        public void ThenTheApplicationErrorSummariesContainsCorrectErrorCount()
        {
            Assert.That(_applicationSummery.ErrorSummaries[0].ErrorCount, Is.EqualTo(_applicationErrorCounts[0]));
            Assert.That(_applicationSummery.ErrorSummaries[1].ErrorCount, Is.EqualTo(_applicationErrorCounts[1]));
            Assert.That(_applicationSummery.ErrorSummaries[2].ErrorCount, Is.EqualTo(_applicationErrorCounts[2]));
        }

        [Test]
        public void ThenTheApplicationErrorSummariesContainsCorrectErrorTimes()
        {
            Assert.That(_applicationSummery.ErrorSummaries[0].Latest, Is.EqualTo(_applicationErrors[0].Time));
            Assert.That(_applicationSummery.ErrorSummaries[1].Latest, Is.EqualTo(_applicationErrors[1].Time));
            Assert.That(_applicationSummery.ErrorSummaries[2].Latest, Is.EqualTo(_applicationErrors[2].Time));
        }

        [Test]
        public void ThenTheApplicationErrorSummariesContainsCorrectErrorTypes()
        {
            Assert.That(_applicationSummery.ErrorSummaries[0].LatestErrorType, Is.EqualTo(_applicationErrors[0].ExceptionType));
            Assert.That(_applicationSummery.ErrorSummaries[1].LatestErrorType, Is.EqualTo(_applicationErrors[1].ExceptionType));
            Assert.That(_applicationSummery.ErrorSummaries[2].LatestErrorType, Is.EqualTo(_applicationErrors[2].ExceptionType));
        }
    }
}
