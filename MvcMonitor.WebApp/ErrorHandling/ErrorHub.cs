using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using MvcMonitor.Data.Providers.Factories;
using MvcMonitor.Models;

namespace MvcMonitor.ErrorHandling
{
    [HubName("showcaseErrorHub")]
    public class ErrorHub : Hub
    {
        private readonly List<string> _applications;
        private readonly ISummaryProviderFactory _summaryProviderFactory;
        private readonly IIndexProviderFactory _indexProviderFactory;

        public ErrorHub() : this(new SummaryProviderFactory(), MonitorConfiguration.Applications, new IndexProviderFactory()) { }

        public ErrorHub(ISummaryProviderFactory summaryProviderFactory, List<string> applications, IIndexProviderFactory indexProviderFactory)
        {
            _summaryProviderFactory = summaryProviderFactory;
            _applications = applications;
            _indexProviderFactory = indexProviderFactory;
        }

        public ErrorSummaryCollection GetApplicationErrorSummary()
        {
            var summaryProvider = _summaryProviderFactory.Create();

            var applicationErrorSummaries = new List<ApplicationErrorSummary>();

            foreach (var applicationName in _applications)
            {
                var latestApplicationError = summaryProvider.GetLatestErrorForApplication(applicationName);
                var totalApplicationErrorCount = summaryProvider.GetTotalErrorCountForApplication(applicationName);

                applicationErrorSummaries.Add(new ApplicationErrorSummary(applicationName,
                                                                          totalApplicationErrorCount,
                                                                          latestApplicationError != null ? latestApplicationError.Time : DateTime.MinValue,
                                                                          latestApplicationError != null ? latestApplicationError.ExceptionType : "Unknown"));
            }

            var errorSummaryCollection = new ErrorSummaryCollection(summaryProvider.GetTotalErrorCount(),
                                                                    summaryProvider.GetLatestError(),
                                                                    applicationErrorSummaries);

            return errorSummaryCollection;
        }

        public PagedList<ErrorModel> GetPagedErrors(int page, int pageSize, string filterFrom, string filterTo,
                                                    string filterUser, string filterApplication, string filterLocation)
        {
            DateTime dateFrom;
            DateTime dateTo;

            var queryDateFrom = DateTime.TryParse(filterFrom, out dateFrom) ? (DateTime?) dateFrom : null;
            var queryDateTo = DateTime.TryParse(filterTo, out dateTo) ? (DateTime?)dateTo : null;

            if (queryDateTo != null)
            {
                queryDateTo = queryDateTo.Value.AddHours(23).AddMinutes(59).AddSeconds(59);
            }

            var indexProvider = _indexProviderFactory.Create();
            
            var pagedErrors = indexProvider.GetErrors(page, pageSize, queryDateFrom, queryDateTo, filterUser, filterApplication, filterLocation);
            return pagedErrors;
        }

        public List<ErrorLocationModel> GetErrorLocations()
        {
            var summaryLocations = new List<ErrorLocationModel>();
            
            var summaryProvider = _summaryProviderFactory.Create();

            foreach (var applicationToCheck in _applications)
            {
                foreach (var location in summaryProvider.GetErrorLocationsForApplication(applicationToCheck))
                {
                    if (summaryLocations.Any(summaryLocation => summaryLocation.Application == applicationToCheck && summaryLocation.Location == location))
                    {
                        summaryLocations
                            .Single(current => current.Application == applicationToCheck && current.Location == location)
                            .Occurences += 1;
                    }
                    else
                    {
                        summaryLocations.Add(new ErrorLocationModel()
                        {
                            Application = applicationToCheck,
                            Location = location,
                            Occurences = 1
                        });
                    }
                }
            }

            return summaryLocations;
        }
    }
}