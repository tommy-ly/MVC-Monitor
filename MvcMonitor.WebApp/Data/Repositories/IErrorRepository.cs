using System;
using System.Collections.Generic;
using MvcMonitor.Models;

namespace MvcMonitor.Data.Repositories
{
    public interface IErrorRepository
    {
        bool IsAvailable();

        void Add(ErrorModel error);

        IEnumerable<ErrorModel> Get(DateTime? from, DateTime? to, string applicationName, string username, string location);

        PagedList<ErrorModel> GetPaged(int skip, int take, DateTime? from, DateTime? to, string applicationName, string username, string location);
    }
}