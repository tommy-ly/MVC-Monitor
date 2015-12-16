using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcMonitor.Data.Repositories
{
    public static class ErrorRepositoryLocator
    {
        private static List<Type> _repoAssemblies;

        public static IEnumerable<Type> GetErrorRepositories()
        {
            if (_repoAssemblies == null)
            {
                _repoAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                                           .SelectMany(s => s.GetTypes())
                                           .Where(typeof (IErrorRepository).IsAssignableFrom).ToList();
            }

            return _repoAssemblies;
        }
    }
}