using System;
using System.Collections.Generic;
using System.Linq;
using MvcMonitor.Data.Repositories;
using MvcMonitor.Models;
using MvcMonitor.StackTrace;
using MvcMonitor.Utilities;

namespace MvcMonitor.Data.Providers
{
    public class SummaryProvider : ISummaryProvider
    {
        private readonly IErrorRepository _errorRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IStackTraceProcessor _stackTraceProcessor;


        public SummaryProvider()
            : this(new InMemoryErrorRepository(), new DateTimeProvider(), new StackTraceProcessor())
        {
        }

        public SummaryProvider(IErrorRepository errorRepository, IDateTimeProvider dateTimeProvider, IStackTraceProcessor stackTraceProcessor)
        {
            _errorRepository = errorRepository;
            _dateTimeProvider = dateTimeProvider;
            _stackTraceProcessor = stackTraceProcessor;
        }

        public IEnumerable<string> GetErrorLocationsForApplication(string application)
        {
            var dateFrom = _dateTimeProvider.UtcNow().AddHours(-24);

            var locations = new List<string>();

            var applicationErrors = _errorRepository.Get(dateFrom, _dateTimeProvider.UtcNow(), application, null, null);

            foreach (var stackTraceLocationResult 
                in applicationErrors.Select(applicationError => _stackTraceProcessor.GetLocalLocations(applicationError.ExceptionStackTrace)))
            {
                locations.Add(stackTraceLocationResult.Locations.Any() 
                        ? stackTraceLocationResult.Locations.First().Split(new[] {"\\"}, StringSplitOptions.RemoveEmptyEntries).Last()
                        : "Unknown");
            }

            return locations;
        }

        public ErrorModel GetLatestError()
        {
            var now = _dateTimeProvider.UtcNow();

            var latestError = _errorRepository.GetPaged(0, 1, now.AddHours(-24), now, null, null, null);

            return latestError.Items.Any()
                ? latestError.Items.First() 
                : null;
        }

        public ErrorModel GetLatestErrorForApplication(string application)
        {
            var now = _dateTimeProvider.UtcNow();

            var latestError = _errorRepository.GetPaged(0, 1, now.AddHours(-24), now, application, null, null);

            return latestError.Items.Any()
                ? latestError.Items.First()
                : null;
        }

        public int GetTotalErrorCount()
        {
            var now = _dateTimeProvider.UtcNow();

            var repositoryErrors = _errorRepository.GetPaged(0, 1, now.AddHours(-24), now, null, null, null);

            return repositoryErrors.TotalCount;
        }

        public int GetTotalErrorCountForApplication(string application)
        {
            var now = _dateTimeProvider.UtcNow();

            var repositoryErrors = _errorRepository.GetPaged(0, 1, now.AddHours(-24), now, application, null, null);

            return repositoryErrors.TotalCount;
        }
    }
}