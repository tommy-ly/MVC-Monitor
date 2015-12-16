using Microsoft.AspNet.SignalR.Hubs;
using MvcMonitor.Models;

namespace MvcMonitor.Broadcaster
{
    public class SignalrBroadcaster : ISignalrBroadcaster
    {
        private readonly IHubConnectionContext _context;

        public SignalrBroadcaster(IHubConnectionContext context)
        {
            _context = context;
        }

        public void ErrorReceived(ErrorModel error)
        {
            _context.All.errorReceived(error);
        }
    }
}