using System.Net;
using MvcMonitor.Models.Factories;
using NUnit.Framework;

namespace MvcMonitor.Tests.Models
{
    public class StatusCodeFactoryTests
    {
        [TestCase("200", HttpStatusCode.OK)]
        [TestCase("404", HttpStatusCode.NotFound)]
        [TestCase("500", HttpStatusCode.InternalServerError)]
        [TestCase("", HttpStatusCode.Unused)]
        [TestCase(null, HttpStatusCode.Unused)]
        [TestCase("53452324", (HttpStatusCode)53452324)]
        public void WhenCreatingStatusCode(string input, HttpStatusCode expectedOutput)
        {
            var result = new StatusCodeFactory().Create(input);

            Assert.That(result, Is.EqualTo(expectedOutput));
        }
    }
}
