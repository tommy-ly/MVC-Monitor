using MvcMonitor.Data.Repositories;
using MvcMonitor.StackTrace;
using MvcMonitor.Utilities;

namespace MvcMonitor.Data.Providers.Factories
{
    public class SummaryProviderFactory : ISummaryProviderFactory
    {
        public ISummaryProvider Create()
        {
            return new SummaryProvider(new ErrorRepositoryFactory().GetRepository(), new DateTimeProvider(), new StackTraceProcessor());
        }
    }
}