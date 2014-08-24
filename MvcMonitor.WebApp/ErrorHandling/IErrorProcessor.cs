using MvcMonitor.Models;

namespace MvcMonitor.ErrorHandling
{
    public interface IErrorProcessor
    {
        void ProcessElmahError(ElmahErrorRequest elmahErrorDetailRequest);
    }
}