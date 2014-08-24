using Microsoft.AspNet.SignalR;
using MvcMonitor.ErrorHandling;

namespace MvcMonitor.Broadcaster
{
    public class SignalrBroadcasterFactory : ISignalrBroadcasterFactory
    {
        public ISignalrBroadcaster Create()
        {
            return new SignalrBroadcaster(GlobalHost.ConnectionManager.GetHubContext<ErrorHub>().Clients);
        }
    }
}