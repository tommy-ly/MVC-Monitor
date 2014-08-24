namespace MvcMonitor.Broadcaster
{
    public interface ISignalrBroadcasterFactory
    {
        ISignalrBroadcaster Create();
    }
}