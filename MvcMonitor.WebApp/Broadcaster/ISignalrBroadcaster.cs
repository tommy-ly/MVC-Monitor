using MvcMonitor.Models;

namespace MvcMonitor.Broadcaster
{
    public interface ISignalrBroadcaster
    {
        void ErrorReceived(ErrorModel error);
    }
}