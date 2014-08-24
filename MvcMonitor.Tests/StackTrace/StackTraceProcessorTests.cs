using MvcMonitor.Models;
using MvcMonitor.StackTrace;
using NUnit.Framework;

namespace MvcMonitor.Tests.StackTrace
{
    [TestFixture]
    public class StackTraceProcessorGetErrorLocationTests
    {
        private const string ERROR_STACK_TRACE =
            "My.Web.App.SomeException (0x80004005): Account reference [EX001asd230]\r\n   at My.Web.App.Controller.Method(string param) in C:\\Website\\App\\Code\\File.cs:line 40\r\n   at My.Web.Site.Controller.OnActionExecuting(ActionExecutingContext filterContext) in C:\\Website\\App\\Code\\File2.cs:line 33\r\n   at System.Web.Mvc.ControllerActionInvoker.InvokeActionMethodFilter(IActionFilter filter, ActionExecutingContext preContext, Func`1 continuation)\r\n   at System.Web.Mvc.ControllerActionInvoker.<>c__DisplayClass15.<>c__DisplayClass17.<InvokeActionMethodWithFilters>b__14()\r\n   at System.Web.Mvc.ControllerActionInvoker.InvokeActionMethodFilter(IActionFilter filter, ActionExecutingContext preContext, Func`1 continuation)\r\n   at System.Web.Mvc.ControllerActionInvoker.<>c__DisplayClass15.<>c__DisplayClass17.<InvokeActionMethodWithFilters>b__14()\r\n   at System.Web.Mvc.ControllerActionInvoker.InvokeActionMethodWithFilters(ControllerContext controllerContext, IList`1 filters, ActionDescriptor actionDescriptor, IDictionary`2 parameters)\r\n   at System.Web.Mvc.ControllerActionInvoker.InvokeAction(ControllerContext controllerContext, String actionName)\r\n   at System.Web.Mvc.Controller.ExecuteCore()\r\n   at System.Web.Mvc.ControllerBase.Execute(RequestContext requestContext)\r\n   at System.Web.Mvc.ControllerBase.System.Web.Mvc.IController.Execute(RequestContext requestContext)\r\n   at System.Web.Mvc.MvcHandler.<>c__DisplayClass6.<>c__DisplayClassb.<BeginProcessRequest>b__5()\r\n   at System.Web.Mvc.Async.AsyncResultWrapper.<>c__DisplayClass1.<MakeVoidDelegate>b__0()\r\n   at System.Web.Mvc.Async.AsyncResultWrapper.<>c__DisplayClass8`1.<BeginSynchronous>b__7(IAsyncResult _)\r\n   at System.Web.Mvc.Async.AsyncResultWrapper.WrappedAsyncResult`1.End()\r\n   at System.Web.Mvc.MvcHandler.<>c__DisplayClasse.<EndProcessRequest>b__d()\r\n   at System.Web.Mvc.SecurityUtil.<GetCallInAppTrustThunk>b__0(Action f)\r\n   at System.Web.Mvc.SecurityUtil.ProcessInApplicationTrust(Action action)\r\n   at System.Web.Mvc.MvcHandler.EndProcessRequest(IAsyncResult asyncResult)\r\n   at System.Web.Mvc.MvcHandler.System.Web.IHttpAsyncHandler.EndProcessRequest(IAsyncResult result)\r\n   at System.Web.HttpApplication.CallHandlerExecutionStep.System.Web.HttpApplication.IExecutionStep.Execute()\r\n   at System.Web.HttpApplication.ExecuteStep(IExecutionStep step, Boolean& completedSynchronously)";

        private StackTraceLocationResult _result;

        [SetUp]
        public void WhenGettingTheErrorLocations()
        {
            _result = new StackTraceProcessor().GetLocalLocations(ERROR_STACK_TRACE);
        }

        [Test]
        public void TheLocalErrorLocationsAreReturned()
        {
            Assert.That(_result.Locations.Count, Is.EqualTo(2));

            Assert.That(_result.Locations[0], Is.EqualTo("at My.Web.App.Controller.Method(string param) in C:\\Website\\App\\Code\\File.cs:line 40"));
            Assert.That(_result.Locations[1], Is.EqualTo("at My.Web.Site.Controller.OnActionExecuting(ActionExecutingContext filterContext) in C:\\Website\\App\\Code\\File2.cs:line 33"));
        }
    }

    [TestFixture]
    public class StackTraceProcessorGetErrorLocationsForMvcErrorTests
    {
        private const string ERROR_STACK_TRACE =
            "System.Web.HttpException (0x80004005): The controller for path '/Not/Found' was not found or does not implement IController.\r\n   at System.Web.Mvc.DefaultControllerFactory.GetControllerInstance(RequestContext requestContext, Type controllerType)\r\n   at System.Web.Mvc.DefaultControllerFactory.CreateController(RequestContext requestContext, String controllerName)\r\n   at System.Web.Mvc.MvcHandler.ProcessRequestInit(HttpContextBase httpContext, IController& controller, IControllerFactory& factory)\r\n   at System.Web.Mvc.MvcHandler.<>c__DisplayClass6.<BeginProcessRequest>b__2()\r\n   at System.Web.Mvc.SecurityUtil.<>c__DisplayClassb`1.<ProcessInApplicationTrust>b__a()\r\n   at System.Web.Mvc.SecurityUtil.<GetCallInAppTrustThunk>b__0(Action f)\r\n   at System.Web.Mvc.SecurityUtil.ProcessInApplicationTrust(Action action)\r\n   at System.Web.Mvc.SecurityUtil.ProcessInApplicationTrust[TResult](Func`1 func)\r\n   at System.Web.Mvc.MvcHandler.BeginProcessRequest(HttpContextBase httpContext, AsyncCallback callback, Object state)\r\n   at System.Web.Mvc.MvcHandler.BeginProcessRequest(HttpContext httpContext, AsyncCallback callback, Object state)\r\n   at System.Web.Mvc.MvcHandler.System.Web.IHttpAsyncHandler.BeginProcessRequest(HttpContext context, AsyncCallback cb, Object extraData)\r\n   at System.Web.HttpApplication.CallHandlerExecutionStep.System.Web.HttpApplication.IExecutionStep.Execute()\r\n   at System.Web.HttpApplication.ExecuteStep(IExecutionStep step, Boolean& completedSynchronously)";

        private StackTraceLocationResult _result;

        [SetUp]
        public void WhenGettingTheErrorLocations()
        {
            _result = new StackTraceProcessor().GetLocalLocations(ERROR_STACK_TRACE);
        }

        [Test]
        public void ThenNoLocationsAreReturned()
        {
            Assert.That(_result.Locations.Count, Is.EqualTo(0));
        }

    }
}
