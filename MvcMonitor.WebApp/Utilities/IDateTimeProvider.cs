using System;

namespace MvcMonitor.Utilities
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow();
    }
}