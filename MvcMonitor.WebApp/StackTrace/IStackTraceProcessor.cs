using MvcMonitor.Models;

namespace MvcMonitor.StackTrace
{
    public interface IStackTraceProcessor
    {
        StackTraceLocationResult GetLocalLocations(string stackTrace);
    }
}