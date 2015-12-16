namespace MvcMonitor.Models.Factories
{
    public interface IErrorModelFactory
    {
        ErrorModel Create(ElmahErrorRequest elmahErrorRequest);
    }
}