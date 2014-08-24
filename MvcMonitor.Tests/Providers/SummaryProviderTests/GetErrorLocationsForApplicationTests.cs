using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using MvcMonitor.Data.Providers;
using MvcMonitor.Data.Repositories;
using MvcMonitor.Models;
using MvcMonitor.StackTrace;
using MvcMonitor.Utilities;
using NUnit.Framework;

namespace MvcMonitor.Tests.Providers.SummaryProviderTests
{
    [TestFixture]
    public class GetErrorLocationsForApplicationTests
    {
        private Mock<IErrorRepository> _mockErrorRepository;
        private DateTime _now;
        private string _application;
        private List<ErrorModel> _applicationErrors;
        private Mock<IStackTraceProcessor> _mockStackTraceProcessor;
        private IEnumerable<string> _result;

        [SetUp]
        public void WhenFetchingErrorLocationsForAnApplication()
        {
            _now = new DateTime(2014, 3, 30, 13, 58, 0);

            _application = "app1";

            _applicationErrors = new List<ErrorModel>()
            {
                new ErrorModel() {ExceptionStackTrace = "skdfnsdf"},
                new ErrorModel() {ExceptionStackTrace = "sfenwfdf"}
            };

            _mockErrorRepository = new Mock<IErrorRepository>();
            _mockErrorRepository
                .Setup(repo => repo.Get(It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<string>(), null, null))
                .Returns(_applicationErrors);

            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            mockDateTimeProvider
                .Setup(provider => provider.UtcNow())
                .Returns(_now);

            _mockStackTraceProcessor = new Mock<IStackTraceProcessor>();
            _mockStackTraceProcessor
                .Setup(processor => processor.GetLocalLocations(_applicationErrors[0].ExceptionStackTrace))
                .Returns(new StackTraceLocationResult() {Locations = new List<string> {"C:\\SomeDir\\Err0Location1.cs line 123", "C:\\SomeOtherDir\\Err0Location2.cs line 456"}});

            _mockStackTraceProcessor
                .Setup(processor => processor.GetLocalLocations(_applicationErrors[1].ExceptionStackTrace))
                .Returns(new StackTraceLocationResult() { Locations = new List<string> { "D:\\SomeDir\\Err1Location1.cs line 789", "D:\\SomeDir\\Err1Location2.cs line 0" } });

            var summaryProvider = new SummaryProvider(_mockErrorRepository.Object, mockDateTimeProvider.Object, _mockStackTraceProcessor.Object);
            _result = summaryProvider.GetErrorLocationsForApplication(_application);
        }

        [Test]
        public void ThenTheErrorsAreFetched()
        {
            _mockErrorRepository.Verify(factory => factory.Get(_now.AddHours(-24), _now, _application, null, null));
        }

        [Test]
        public void ThenTheStackTracesAreProcessedForEachError()
        {
            _mockStackTraceProcessor.Verify(processor => processor.GetLocalLocations(_applicationErrors[0].ExceptionStackTrace));
        }

        [Test]
        public void ThenTheTopErrorInEachStackTraceIsAddedToTheResultsInFriendlyFormat()
        {
            var localLocations = _result.ToList();

            Assert.That(localLocations.Count, Is.EqualTo(2));

            Assert.That(localLocations[0], Is.EqualTo("Err0Location1.cs line 123"));
            Assert.That(localLocations[1], Is.EqualTo("Err1Location1.cs line 789"));
        }
    }

    [TestFixture]
    public class GetErrorLocationsForApplicationWithNoLocalLocationsTests
    {
        private Mock<IErrorRepository> _mockErrorRepository;
        private DateTime _now;
        private string _application;
        private List<ErrorModel> _applicationErrors;
        private Mock<IStackTraceProcessor> _mockStackTraceProcessor;
        private IEnumerable<string> _result;

        [SetUp]
        public void WhenFetchingErrorLocationsForAnApplication()
        {
            _now = new DateTime(2014, 3, 30, 13, 58, 0);

            _application = "app1";

            _applicationErrors = new List<ErrorModel>()
            {
                new ErrorModel() {ExceptionStackTrace = "skdfnsdf"},
            };

            _mockErrorRepository = new Mock<IErrorRepository>();
            _mockErrorRepository
                .Setup(repo => repo.Get(It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<string>(), null, null))
                .Returns(_applicationErrors);

            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            mockDateTimeProvider
                .Setup(provider => provider.UtcNow())
                .Returns(_now);

            _mockStackTraceProcessor = new Mock<IStackTraceProcessor>();
            _mockStackTraceProcessor
                .Setup(processor => processor.GetLocalLocations(_applicationErrors[0].ExceptionStackTrace))
                .Returns(new StackTraceLocationResult() { Locations = new List<string>() });

            var summaryProvider = new SummaryProvider(_mockErrorRepository.Object, mockDateTimeProvider.Object, _mockStackTraceProcessor.Object);
            _result = summaryProvider.GetErrorLocationsForApplication(_application);
        }

        [Test]
        public void ThenAnUnknownErrorIsAddedToTheResults()
        {
            var localLocations = _result.ToList();

            Assert.That(localLocations.Count, Is.EqualTo(1));

            Assert.That(localLocations[0], Is.EqualTo("Unknown"));
        }
    }
}
