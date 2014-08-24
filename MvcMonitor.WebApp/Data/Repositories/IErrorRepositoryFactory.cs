namespace MvcMonitor.Data.Repositories
{
    public interface IErrorRepositoryFactory
    {
        IErrorRepository GetRepository();
    }
}