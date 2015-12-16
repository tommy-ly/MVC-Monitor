using System;
using System.Collections.Generic;

namespace MvcMonitor.Models
{
    [Serializable]
    public class ErrorSummaryCollection
    {
        public ErrorSummaryCollection(int totalCount, ErrorModel latestError, IList<ApplicationErrorSummary> errorSummaries)
        {
            TotalCount = totalCount;
            LatestError = latestError;
            ErrorSummaries = errorSummaries;
        }

        public int TotalCount { get; private set; }
        public ErrorModel LatestError { get; private set; }
        public IList<ApplicationErrorSummary> ErrorSummaries { get; private set; }
    }
}