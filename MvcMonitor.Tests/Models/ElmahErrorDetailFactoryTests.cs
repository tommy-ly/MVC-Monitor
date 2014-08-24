using MvcMonitor.Models;
using MvcMonitor.Models.Factories;
using NUnit.Framework;

namespace MvcMonitor.Tests.Models
{
    [TestFixture]
    public class ElmahErrorDetailFactorySampleErrorTests
    {
        private const string SAMPLE_ERROR = @"{ ""application"": ""MyApp1"", ""host"": ""MyServer1"", ""type"": ""System.Web.HttpException"", ""message"": ""The controller for path '/Not/Found' was not found or does not implement IController."", ""source"": ""System.Web.Mvc"", ""detail"": ""System.Web.HttpException (0x80004005): The controller for path '/Not/Found' was not found or does not implement IController.\r\n   at System.Web.Mvc.DefaultControllerFactory.GetControllerInstance(RequestContext requestContext, Type controllerType)\r\n   at System.Web.Mvc.DefaultControllerFactory.CreateController(RequestContext requestContext, String controllerName)\r\n   at System.Web.Mvc.MvcHandler.ProcessRequestInit(HttpContextBase httpContext, IController& controller, IControllerFactory& factory)\r\n   at System.Web.Mvc.MvcHandler.<>c__DisplayClass6.<BeginProcessRequest>b__2()\r\n   at System.Web.Mvc.SecurityUtil.<>c__DisplayClassb`1.<ProcessInApplicationTrust>b__a()\r\n   at System.Web.Mvc.SecurityUtil.<GetCallInAppTrustThunk>b__0(Action f)\r\n   at System.Web.Mvc.SecurityUtil.ProcessInApplicationTrust(Action action)\r\n   at System.Web.Mvc.SecurityUtil.ProcessInApplicationTrust[TResult](Func`1 func)\r\n   at System.Web.Mvc.MvcHandler.BeginProcessRequest(HttpContextBase httpContext, AsyncCallback callback, Object state)\r\n   at System.Web.Mvc.MvcHandler.BeginProcessRequest(HttpContext httpContext, AsyncCallback callback, Object state)\r\n   at System.Web.Mvc.MvcHandler.System.Web.IHttpAsyncHandler.BeginProcessRequest(HttpContext context, AsyncCallback cb, Object extraData)\r\n   at System.Web.HttpApplication.CallHandlerExecutionStep.System.Web.HttpApplication.IExecutionStep.Execute()\r\n   at System.Web.HttpApplication.ExecuteStep(IExecutionStep step, Boolean& completedSynchronously)"", ""user"": ""default@user.com"", ""time"": ""2013-08-08T12:59:48.1051371Z"", ""statusCode"": 404, ""serverVariables"": { ""ALL_HTTP"": ""HTTP_CACHE_CONTROL:max-age=0\r\nHTTP_CONNECTION:keep-alive\r\nHTTP_ACCEPT:text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8\r\nHTTP_ACCEPT_ENCODING:gzip,deflate,sdch\r\nHTTP_ACCEPT_LANGUAGE:en-GB,en;q=0.8,en-US;q=0.6,fr;q=0.4,es;q=0.2\r\nHTTP_COOKIE:ASP.NET_SessionId=aaaaaaa; culture=en-GB\r\nHTTP_HOST:localhost:1685\r\nHTTP_USER_AGENT:Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/28.0.1500.95 Safari/537.36\r\n"", ""ALL_RAW"": ""Cache-Control: max-age=0\r\nConnection: keep-alive\r\nAccept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8\r\nAccept-Encoding: gzip,deflate,sdch\r\nAccept-Language: en-GB,en;q=0.8,en-US;q=0.6,fr;q=0.4,es;q=0.2\r\nCookie: ASP.NET_SessionId=aaaaaaa; culture=en-GB\r\nHost: localhost:1685\r\nUser-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/28.0.1500.95 Safari/537.36\r\n"", ""APPL_PHYSICAL_PATH"": ""C:\\my\\site\\dir\\"", ""AUTH_TYPE"": ""Forms"", ""AUTH_USER"": ""default@user.com"", ""AUTH_PASSWORD"": ""*****"", ""LOGON_USER"": ""SERVER\\ADMIN"", ""REMOTE_USER"": ""default@user.com"", ""CONTENT_LENGTH"": ""0"", ""LOCAL_ADDR"": ""127.0.0.1"", ""PATH_INFO"": ""/Not/Found"", ""PATH_TRANSLATED"": ""C:\\apps\\directory\\Not\\Found"", ""REMOTE_ADDR"": ""127.0.0.1"", ""REMOTE_HOST"": ""127.0.0.1"", ""REQUEST_METHOD"": ""GET"", ""SCRIPT_NAME"": ""/Not/Found"", ""SERVER_NAME"": ""localhost"", ""SERVER_PORT"": ""1685"", ""SERVER_PORT_SECURE"": ""0"", ""SERVER_PROTOCOL"": ""HTTP/1.1"", ""URL"": ""/Not/Found"", ""HTTP_CACHE_CONTROL"": ""max-age=0"", ""HTTP_CONNECTION"": ""keep-alive"", ""HTTP_ACCEPT"": ""text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"", ""HTTP_ACCEPT_ENCODING"": ""gzip,deflate,sdch"", ""HTTP_ACCEPT_LANGUAGE"": ""en-GB,en;q=0.8,en-US;q=0.6,fr;q=0.4,es;q=0.2"", ""HTTP_COOKIE"": ""ASP.NET_SessionId=aaaaaaa; culture=en-GB"", ""HTTP_HOST"": ""localhost:1685"", ""HTTP_USER_AGENT"": ""Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/28.0.1500.95 Safari/537.36"" }, ""cookies"": { ""ASP.NET_SessionId"": ""aaaaaaa"", ""session"": ""aaa"", ""culture"": [ ""en-GB"", ""en-GB"" ] } }";

        private ElmahErrorDetailDto _result;

        [SetUp]
        public void WhenCreatingNewErrorModel()
        {
            _result = new ElmahErrorDetailDtoFactory().Create(SAMPLE_ERROR);
        }

        [Test]
        public void ThenTheTopLevelDetailsAreSet()
        {
            Assert.That(_result.application, Is.EqualTo("MyApp1"));
            Assert.That(_result.host, Is.EqualTo("MyServer1"));
            Assert.That(_result.type, Is.EqualTo("System.Web.HttpException"));
            Assert.That(_result.message, Is.EqualTo("The controller for path '/Not/Found' was not found or does not implement IController."));
            Assert.That(_result.source, Is.EqualTo("System.Web.Mvc"));
            Assert.That(_result.detail, Is.EqualTo("System.Web.HttpException (0x80004005): The controller for path '/Not/Found' was not found or does not implement IController.\r\n   at System.Web.Mvc.DefaultControllerFactory.GetControllerInstance(RequestContext requestContext, Type controllerType)\r\n   at System.Web.Mvc.DefaultControllerFactory.CreateController(RequestContext requestContext, String controllerName)\r\n   at System.Web.Mvc.MvcHandler.ProcessRequestInit(HttpContextBase httpContext, IController& controller, IControllerFactory& factory)\r\n   at System.Web.Mvc.MvcHandler.<>c__DisplayClass6.<BeginProcessRequest>b__2()\r\n   at System.Web.Mvc.SecurityUtil.<>c__DisplayClassb`1.<ProcessInApplicationTrust>b__a()\r\n   at System.Web.Mvc.SecurityUtil.<GetCallInAppTrustThunk>b__0(Action f)\r\n   at System.Web.Mvc.SecurityUtil.ProcessInApplicationTrust(Action action)\r\n   at System.Web.Mvc.SecurityUtil.ProcessInApplicationTrust[TResult](Func`1 func)\r\n   at System.Web.Mvc.MvcHandler.BeginProcessRequest(HttpContextBase httpContext, AsyncCallback callback, Object state)\r\n   at System.Web.Mvc.MvcHandler.BeginProcessRequest(HttpContext httpContext, AsyncCallback callback, Object state)\r\n   at System.Web.Mvc.MvcHandler.System.Web.IHttpAsyncHandler.BeginProcessRequest(HttpContext context, AsyncCallback cb, Object extraData)\r\n   at System.Web.HttpApplication.CallHandlerExecutionStep.System.Web.HttpApplication.IExecutionStep.Execute()\r\n   at System.Web.HttpApplication.ExecuteStep(IExecutionStep step, Boolean& completedSynchronously)"));            
            Assert.That(_result.user, Is.EqualTo("default@user.com"));
            Assert.That(_result.time, Is.EqualTo("2013-08-08T12:59:48.1051371Z"));
            Assert.That(_result.statusCode, Is.EqualTo("404"));
        }

        [Test]
        public void ThenTheServerVariablesAreSet()
        {
            var serverVariables = _result.serverVariables;

            Assert.That(serverVariables.APPL_PHYSICAL_PATH, Is.EqualTo("C:\\my\\site\\dir\\"));
            Assert.That(serverVariables.AUTH_TYPE, Is.EqualTo("Forms"));
            Assert.That(serverVariables.AUTH_USER, Is.EqualTo("default@user.com"));
            Assert.That(serverVariables.LOGON_USER, Is.EqualTo("SERVER\\ADMIN"));
            Assert.That(serverVariables.REMOTE_USER, Is.EqualTo("default@user.com"));
            Assert.That(serverVariables.CONTENT_LENGTH, Is.EqualTo("0"));
            Assert.That(serverVariables.LOCAL_ADDR, Is.EqualTo("127.0.0.1"));
            Assert.That(serverVariables.PATH_INFO, Is.EqualTo("/Not/Found"));
            Assert.That(serverVariables.PATH_TRANSLATED, Is.EqualTo("C:\\apps\\directory\\Not\\Found"));
            Assert.That(serverVariables.REMOTE_ADDR, Is.EqualTo("127.0.0.1"));
            Assert.That(serverVariables.REMOTE_HOST, Is.EqualTo("127.0.0.1"));
            Assert.That(serverVariables.REQUEST_METHOD, Is.EqualTo("GET"));
            Assert.That(serverVariables.SCRIPT_NAME, Is.EqualTo("/Not/Found"));
            Assert.That(serverVariables.SERVER_NAME, Is.EqualTo("localhost"));
            Assert.That(serverVariables.SERVER_PORT, Is.EqualTo("1685"));
            Assert.That(serverVariables.SERVER_PORT_SECURE, Is.EqualTo("0"));
            Assert.That(serverVariables.SERVER_PROTOCOL, Is.EqualTo("HTTP/1.1"));
            Assert.That(serverVariables.HTTP_CACHE_CONTROL, Is.EqualTo("max-age=0"));
            Assert.That(serverVariables.HTTP_CONNECTION, Is.EqualTo("keep-alive"));
            Assert.That(serverVariables.HTTP_ACCEPT, Is.EqualTo("text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"));
            Assert.That(serverVariables.HTTP_ACCEPT_ENCODING, Is.EqualTo("gzip,deflate,sdch"));
            Assert.That(serverVariables.HTTP_ACCEPT_LANGUAGE, Is.EqualTo("en-GB,en;q=0.8,en-US;q=0.6,fr;q=0.4,es;q=0.2"));
            Assert.That(serverVariables.HTTP_COOKIE, Is.EqualTo("ASP.NET_SessionId=aaaaaaa; culture=en-GB"));
            Assert.That(serverVariables.HTTP_HOST, Is.EqualTo("localhost:1685"));
            Assert.That(serverVariables.HTTP_USER_AGENT, Is.EqualTo("Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/28.0.1500.95 Safari/537.36"));
        }
    }
}
