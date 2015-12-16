namespace MvcMonitor.Models.Factories
{
    public interface IElmahErrorDtoFactory
    {
        ElmahErrorRequest Create(string errorId, string applicationSourceId, string infoUrl, string errorDetails);
    }
}