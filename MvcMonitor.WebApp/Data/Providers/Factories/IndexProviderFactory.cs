namespace MvcMonitor.Data.Providers.Factories
{
    public class IndexProviderFactory : IIndexProviderFactory
    {
        public IIndexProvider Create()
        {
            return new IndexProvider(new Repositories.ErrorRepositoryFactory());
        }
    }
}