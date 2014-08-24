using System;
using System.Collections.Generic;
using System.Linq;
using MvcMonitor.Models;

namespace MvcMonitor.Data.Repositories
{
    public class InMemoryErrorRepository : IErrorRepository
    {
        private static readonly List<ErrorModel> Errors = new List<ErrorModel>();

        public bool IsAvailable()
        {
            return true;
        }

        public void Add(ErrorModel error)
        {
            Errors.Add(error);
        }

        public IEnumerable<ErrorModel> Get(DateTime? @from, DateTime? to, string applicationName, string username, string location)
        {
            return GetErrorsWithFilter(@from, to, applicationName, username, location);
        }

        public PagedList<ErrorModel> GetPaged(int skip, int take, DateTime? @from, DateTime? to, string applicationName, string username,
            string location)
        {
            var page = (skip/take) + 1;
            var filteredErrors = GetErrorsWithFilter(@from, to, applicationName, username, location).ToList();

            return new PagedList<ErrorModel>(page, take, filteredErrors.Count, filteredErrors.Skip(skip).Take(take));
        }

        private static IEnumerable<ErrorModel> GetErrorsWithFilter(DateTime? @from, DateTime? to, string applicationName, string username, string location)
        {
            @from = @from ?? DateTime.UtcNow.AddDays(-7).Date;
            @to = @to ?? DateTime.UtcNow.AddDays(1).Date;

            var errorsWithFilter = Errors.OrderByDescending(error => error.Time)
                .Where(error => ContainsInsensitive(error.Application, applicationName)
                                && ContainsInsensitive(error.Username, username)
                                && error.ExceptionLocations.Any(exceptionLocation => ContainsInsensitive(exceptionLocation, location))
                                && error.Time >= @from
                                && error.Time <= @to);

            return errorsWithFilter;
        }

        private static bool ContainsInsensitive(string source, string phrase)
        {
            if (string.IsNullOrEmpty(source) && string.IsNullOrEmpty(phrase))
                return true;

            if (string.IsNullOrEmpty(source))
                return false;

            if (string.IsNullOrEmpty(phrase))
                return true;

            return source.IndexOf(phrase, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}