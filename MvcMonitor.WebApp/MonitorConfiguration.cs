using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace MvcMonitor
{
    public class MonitorConfiguration
    {
        static MonitorConfiguration()
        {
            ApplicationStartTime = DateTime.Now;

            Applications = GetConfiguredApplicationIds();

            ErrorRepository = ConfigurationManager.AppSettings["ErrorRepository"];
        }

        private static List<string> GetConfiguredApplicationIds()
        {
            var appsToMonitor = ConfigurationManager.AppSettings["Applications"];
            var applications = appsToMonitor.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).ToList();
            return applications;
        }

        public static DateTime ApplicationStartTime { get; private set; }

        public static List<string> Applications { get; private set; }

        public static string ErrorRepository { get; private set; }
    }
}