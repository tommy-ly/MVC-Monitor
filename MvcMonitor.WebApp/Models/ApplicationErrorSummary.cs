using System;

namespace MvcMonitor.Models
{
    [Serializable]
    public class ApplicationErrorSummary
    {
        public ApplicationErrorSummary(string application, int errorCount, DateTime latest, string latestErrorType)
        {
            Application = application;
            ErrorCount = errorCount;
            Latest = latest;
            LatestErrorType = latestErrorType;
        }

        public string Application { get; private set; }
        public int ErrorCount { get; private set; }
        public DateTime Latest { get; private set; }
        public string LatestErrorType { get; private set; }
    }
}