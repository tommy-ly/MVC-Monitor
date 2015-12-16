using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace MvcMonitor.Data.Repositories
{
    public class ErrorRepositoryFactory : IErrorRepositoryFactory
    {
        private readonly string _repositoryType;
        private readonly IEnumerable<Type> _availableRepos;

        private IErrorRepository _currentRepository;

        public ErrorRepositoryFactory()
            : this(MonitorConfiguration.ErrorRepository)
        {
        }

        public ErrorRepositoryFactory(string repositoryType)
        {
            _repositoryType = repositoryType;

            _availableRepos = ErrorRepositoryLocator.GetErrorRepositories();
        }

        public IErrorRepository GetRepository()
        {
            if (_currentRepository == null)
            {
                var matchingRepos =
                    _availableRepos.Where(t => t.IsClass && t.FullName.EndsWith("." + _repositoryType)).ToList();

                if (matchingRepos.Count() != 1)
                    throw new ConfigurationErrorsException(
                        string.Format("Could not find type '{0}' that implements IErrorRepository", _repositoryType));

                var configuredRepository = Activator.CreateInstance(Type.GetType(matchingRepos[0].FullName));

                _currentRepository = (IErrorRepository) configuredRepository;
            }

            return _currentRepository;
        }
    }
}