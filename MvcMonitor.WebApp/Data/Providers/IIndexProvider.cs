using System;
using MvcMonitor.Models;

namespace MvcMonitor.Data.Providers
{
    public interface IIndexProvider
    {
        PagedList<ErrorModel> GetErrors(int pageNumber, int pageSize, DateTime? filterFrom, DateTime? filterTo,
                                        string filterUser, string filterApplication, string filterLocation);
    }
}