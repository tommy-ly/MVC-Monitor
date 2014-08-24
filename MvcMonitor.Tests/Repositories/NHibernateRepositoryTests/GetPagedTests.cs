using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using MvcMonitor.Data.Repositories;
using MvcMonitor.Models;
using MvcMonitor.Utilities;
using NUnit.Framework;

namespace MvcMonitor.Tests.Repositories.NHibernateRepositoryTests
{
    [TestFixture]
    [Ignore("Only required if using NHibernate Repository")]
    public class GetPagedTests
    {
        private readonly List<ErrorModel> _testDataErrors = new List<ErrorModel>();
        private IErrorRepository _repository;

        [TestFixtureSetUp]
        public void PopulateRepositoryWithErrors()
        {
            SetUpTestData();

            _repository = new NHibernateRepository();

            foreach (var errorModel in _testDataErrors)
            {
                _repository.Add(errorModel);
            }
        }

        [TestFixtureTearDown]
        public void ClearTestData()
        {
            foreach (var error in _testDataErrors)
            {
                using (var session = NHibernateHelper.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        var repoError = session.Get<ErrorModel>(error.Id);

                        session.Delete(repoError);
                        transaction.Commit();
                    }
                }
            }
        }


        [Test]
        public void ThenGetWithDateFilterReturnsCorrectErrors()
        {
            var errors = _repository
                .GetPaged(1, 1, _testDataErrors[2].Time, _testDataErrors[2].Time, null, null, null);

            Assert.That(errors.TotalCount, Is.EqualTo(1));
        }

        [Test]
        public void ThenGetWithApplicationFilterReturnsCorrectErrors()
        {
            var errors = _repository
                .GetPaged(0, 10, _testDataErrors[5].Time, _testDataErrors[0].Time, "Test2", null, null);

            Assert.That(errors.TotalCount, Is.EqualTo(1));
            Assert.That(errors.Items.Count(), Is.EqualTo(1));
            Assert.That(errors.Items.ToList()[0].Id, Is.EqualTo(_testDataErrors[2].Id));
        }

        [Test]
        public void ThenGetWithUserFilterReturnsCorrectErrors()
        {
            var errors = _repository
                .GetPaged(1, 2, _testDataErrors[5].Time, _testDataErrors[0].Time, null, "Test User1", null);

            Assert.That(errors.TotalCount, Is.EqualTo(5));
            Assert.That(errors.Items.Count(), Is.EqualTo(2));
            Assert.That(errors.Items.ToList()[0].Id, Is.EqualTo(_testDataErrors[1].Id));
            Assert.That(errors.Items.ToList()[1].Id, Is.EqualTo(_testDataErrors[3].Id));
        }

        [Test]
        public void ThenGetWithLocationFilterReturnsCorrectErrors()
        {
            var errors = _repository.GetPaged(1, 1, _testDataErrors[5].Time, _testDataErrors[0].Time, null, null,
                                              "file1.cs");

            Assert.That(errors.TotalCount, Is.EqualTo(4));
            Assert.That(errors.Items.Count(), Is.EqualTo(1));
            Assert.That(errors.Items.ToList()[0].Id, Is.EqualTo(_testDataErrors[1].Id));
        }


        private void SetUpTestData()
        {
            _testDataErrors.Add(new ErrorModel()
                {
                    Application = "Test0",
                    ErrorId = Guid.NewGuid(),
                    ExceptionMessage = "NotReturn",
                    ExceptionSource = "NotReturn",
                    ExceptionStackTrace = "Test Stacktrace file1.cs",
                    ExceptionType = "Test Type",
                    Host = "NotReturn",
                    RequestMethod = "NotReturn",
                    ServerApplicationPath = "NotReturn",
                    ServerApplicationPathTranslated = "NotReturn",
                    ServerName = "NotReturn",
                    ServerPort = 0,
                    ServerPortSecure = "NotReturn",
                    StatusCode = HttpStatusCode.NotFound,
                    Time = DateTime.Now.AddMinutes(-30),
                    Url = "Test Url",
                    Username = "Test User1",
                    UserAgent = "Test User Agent"
                });

            _testDataErrors.Add(new ErrorModel()
                {
                    Application = "Test1",
                    ErrorId = Guid.NewGuid(),
                    ExceptionMessage = "TestMessage1",
                    ExceptionSource = "Test Source",
                    ExceptionStackTrace = "Test Stacktrace file1.cs",
                    ExceptionType = "Test Type",
                    Host = "Test Host",
                    RequestMethod = "Test Method",
                    ServerApplicationPath = "Test Path",
                    ServerApplicationPathTranslated = "Test Path Translated",
                    ServerName = "Test Server",
                    ServerPort = 0,
                    ServerPortSecure = "Test Port Secure",
                    StatusCode = HttpStatusCode.NotFound,
                    Time = DateTime.Now.AddHours(-1),
                    Url = "Test Url",
                    Username = "Test User1",
                    UserAgent = "Test User Agent"
                });

            _testDataErrors.Add(new ErrorModel()
                {
                    Application = "Test2",
                    ErrorId = Guid.NewGuid(),
                    ExceptionMessage = "TestMessage2",
                    ExceptionSource = "Test Source",
                    ExceptionStackTrace = "Test Stacktrace file1.cs",
                    ExceptionType = "Test Type",
                    Host = "Test Host",
                    RequestMethod = "Test Method",
                    ServerApplicationPath = "Test Path",
                    ServerApplicationPathTranslated = "Test Path Translated",
                    ServerName = "Test Server",
                    ServerPort = 0,
                    ServerPortSecure = "Test Port Secure",
                    StatusCode = HttpStatusCode.NotFound,
                    Time = DateTime.Now.AddHours(-2),
                    Url = "Test Url",
                    Username = "Test User2",
                    UserAgent = "Test User Agent"
                });

            _testDataErrors.Add(new ErrorModel()
                {
                    Application = "Test3",
                    ErrorId = Guid.NewGuid(),
                    ExceptionMessage = "TestMessage3",
                    ExceptionSource = "Test Source",
                    ExceptionStackTrace = "Test Stacktrace file2.cs",
                    ExceptionType = "Test Type",
                    Host = "Test Host",
                    RequestMethod = "Test Method",
                    ServerApplicationPath = "Test Path",
                    ServerApplicationPathTranslated = "Test Path Translated",
                    ServerName = "Test Server",
                    ServerPort = 0,
                    ServerPortSecure = "Test Port Secure",
                    StatusCode = HttpStatusCode.NotFound,
                    Time = DateTime.Now.AddHours(-3),
                    Url = "Test Url",
                    Username = "Test User1",
                    UserAgent = "Test User Agent"
                });

            _testDataErrors.Add(new ErrorModel()
                {
                    Application = "Test4",
                    ErrorId = Guid.NewGuid(),
                    ExceptionMessage = "TestMessage4",
                    ExceptionSource = "Test Source",
                    ExceptionStackTrace = "Test Stacktrace file3.cs",
                    ExceptionType = "Test Type",
                    Host = "Test Host",
                    RequestMethod = "Test Method",
                    ServerApplicationPath = "Test Path",
                    ServerApplicationPathTranslated = "Test Path Translated",
                    ServerName = "Test Server",
                    ServerPort = 0,
                    ServerPortSecure = "Test Port Secure",
                    StatusCode = HttpStatusCode.NotFound,
                    Time = DateTime.Now.AddHours(-4),
                    Url = "Test Url",
                    Username = "Test User1",
                    UserAgent = "Test User Agent"
                });

            _testDataErrors.Add(new ErrorModel()
                {
                    Application = "Test5",
                    ErrorId = Guid.NewGuid(),
                    ExceptionMessage = "NotReturn",
                    ExceptionSource = "NotReturn",
                    ExceptionStackTrace = "Test Stacktrace file1.cs",
                    ExceptionType = "Test Type",
                    Host = "NotReturn",
                    RequestMethod = "NotReturn",
                    ServerApplicationPath = "NotReturn",
                    ServerApplicationPathTranslated = "NotReturn",
                    ServerName = "NotReturn",
                    ServerPort = 0,
                    ServerPortSecure = "NotReturn",
                    StatusCode = HttpStatusCode.NotFound,
                    Time = DateTime.Now.AddHours(-5),
                    Url = "Test Url",
                    Username = "Test User1",
                    UserAgent = "Test User Agent"
                });
        }
    }
}
