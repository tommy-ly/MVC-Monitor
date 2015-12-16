using System;
using MvcMonitor.Data.Repositories;
using MvcMonitor.Models;

namespace MvcMonitor.Data.Providers
{
    public class IndexProvider : IIndexProvider
    {
        private readonly IErrorRepositoryFactory _errorRepositoryFactory;

        public IndexProvider(IErrorRepositoryFactory errorRepositoryFactory)
        {
            _errorRepositoryFactory = errorRepositoryFactory;
        }

        public PagedList<ErrorModel> GetErrors(int pageNumber, int pageSize, DateTime? filterFrom, DateTime? filterTo, string filterUser,
                                               string filterApplication, string filterLocation)
        {
            var errorRepository = _errorRepositoryFactory.GetRepository();

            var errors = errorRepository
                .GetPaged(CalculateStartIndex(pageNumber, pageSize), pageSize, filterFrom, filterTo, filterApplication, filterUser, filterLocation);

            foreach (var err in errors.Items)
            {
                err.PopulateCalculatedFields();
            }

            return errors;
        }

        private static int CalculateStartIndex(int pageNumber, int pageSize)
        {
            return (pageNumber - 1)*pageSize;
        }
    }
}