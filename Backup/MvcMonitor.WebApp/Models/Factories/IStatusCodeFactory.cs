using System.Net;

namespace MvcMonitor.Models.Factories
{
    public interface IStatusCodeFactory
    {
        HttpStatusCode Create(string httpStatusCode);
    }
}