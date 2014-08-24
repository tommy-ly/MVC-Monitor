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
    public class GetErrorLocationsTests
    {
        private List<string> _applications;
        private Mock<ISummaryProvider> _mockSummaryProvider;
        private List<ErrorLocationModel> _errorLocationSummaries;

        [SetUp]
        public void WhenGettingErrorLocations()
        {
            _applications = new List<string> { "app1", "app2", "app3" };

            _mockSummaryProvider = new Mock<ISummaryProvider>();

            _mockSummaryProvider
                .Setup(provider => provider.GetErrorLocationsForApplication(_applications[0]))
                .Returns(new[] {"loc1", "loc2", "loc2"});

            _mockSummaryProvider
                .Setup(provider => provider.GetErrorLocationsForApplication(_applications[1]))
                .Returns(new[] { "loc3", "loc4", "loc3", "loc3" });

            _mockSummaryProvider
                .Setup(provider => provider.GetErrorLocationsForApplication(_applications[2]))
                .Returns(new[] { "loc5", "loc5" });

            var mockSummaryProviderFactory = new Mock<ISummaryProviderFactory>();
            mockSummaryProviderFactory
                .Setup(factory => factory.Create())
                .Returns(_mockSummaryProvider.Object);

            var errorHub = new ErrorHub(mockSummaryProviderFactory.Object, _applications, null);

            _errorLocationSummaries = errorHub.GetErrorLocations();
        }

        [Test]
        public void ThenTheLatestErrorsAreFetchedForEachApplications()
        {
            foreach (var application in _applications)
            {
                var applicationToCheck = application;
                _mockSummaryProvider.Verify(provider => provider.GetErrorLocationsForApplication(applicationToCheck));
            }
        }

        [Test]
        public void ThenTheCorrectErrorLocationModelsAreReturned()
        {
            Assert.That(_errorLocationSummaries[0].Application, Is.EqualTo("app1"));
            Assert.That(_errorLocationSummaries[0].Location, Is.EqualTo("loc1"));
            Assert.That(_errorLocationSummaries[0].Occurences, Is.EqualTo(1));

            Assert.That(_errorLocationSummaries[1].Application, Is.EqualTo("app1"));
            Assert.That(_errorLocationSummaries[1].Location, Is.EqualTo("loc2"));
            Assert.That(_errorLocationSummaries[1].Occurences, Is.EqualTo(2));

            Assert.That(_errorLocationSummaries[2].Application, Is.EqualTo("app2"));
            Assert.That(_errorLocationSummaries[2].Location, Is.EqualTo("loc3"));
            Assert.That(_errorLocationSummaries[2].Occurences, Is.EqualTo(3));

            Assert.That(_errorLocationSummaries[3].Application, Is.EqualTo("app2"));
            Assert.That(_errorLocationSummaries[3].Location, Is.EqualTo("loc4"));
            Assert.That(_errorLocationSummaries[3].Occurences, Is.EqualTo(1));

            Assert.That(_errorLocationSummaries[4].Application, Is.EqualTo("app3"));
            Assert.That(_errorLocationSummaries[4].Location, Is.EqualTo("loc5"));
            Assert.That(_errorLocationSummaries[4].Occurences, Is.EqualTo(2));
        }
    }
}
