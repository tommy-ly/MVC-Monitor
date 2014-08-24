using System;
using System.Linq;
using MvcMonitor.Models;

namespace MvcMonitor.StackTrace
{
    public class StackTraceProcessor : IStackTraceProcessor
    {
        public StackTraceLocationResult GetLocalLocations(string stackTrace)
        {
            var result = new StackTraceLocationResult();

            var lines = stackTrace.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines.Where(line => line.Contains(".cs:line ")))
            {
                result.Locations.Add(line.Trim());
            }

            return result;
        }
    }
}