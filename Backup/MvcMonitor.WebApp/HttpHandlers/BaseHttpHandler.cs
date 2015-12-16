using System.Web;

namespace MvcMonitor.HttpHandlers
{
    public abstract class BaseHttpHandler : IHttpHandler
    {
        public virtual bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            ProcessRequest(new HttpContextWrapper(context));
        }

        public abstract void ProcessRequest(HttpContextBase context);
    }
}