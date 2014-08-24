using System;
using System.Collections.Generic;
using MvcMonitor.Models;
using MvcMonitor.Utilities;
using NHibernate;

namespace MvcMonitor.Data.Repositories
{
    public class NHibernateRepository : IErrorRepository
    {
        public bool IsAvailable()
        {
            return true;
        }

        public void Add(ErrorModel error)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(error);
                    transaction.Commit();
                }
            }
        }

        public IEnumerable<ErrorModel> Get(DateTime? @from, DateTime? to, string applicationName, string username, string location)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (session.BeginTransaction())
                {
                    var queryOver = session.QueryOver<ErrorModel>()
                        .OrderBy(model => model.Time).Desc;

                    if (@from.HasValue && to.HasValue)
                    {
                        queryOver.AndRestrictionOn(error => error.Time)
                            .IsBetween(@from)
                                .And(to);
                    }

                    if (!string.IsNullOrWhiteSpace(applicationName))
                    {
                        queryOver.Where(model => model.Application == applicationName);
                    }

                    if (!string.IsNullOrWhiteSpace(username))
                    {
                        queryOver.Where(model => model.Username == username);
                    }

                    if (!string.IsNullOrWhiteSpace(location))
                    {
                        queryOver.WhereRestrictionOn(model => model.ExceptionStackTrace)
                            .IsLike(string.Format("%{0}%", location));
                    }

                    return queryOver.List();
                }
            }
        }

        public PagedList<ErrorModel> GetPaged(int skip, int take, DateTime? @from, DateTime? to, string applicationName, string username, string location)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (session.BeginTransaction())
                {
                    var rowCount = BuildCriteria(@from, to, applicationName, username, location, session)
                        .RowCount();

                    var itemCriteria = BuildCriteria(@from, to, applicationName, username, location, session)
                        .OrderBy(model => model.Time).Desc
                        .Skip(skip)
                        .Take(take);

                    return new PagedList<ErrorModel>(0, 0, rowCount, itemCriteria.List());
                }
            }
        }

        private static IQueryOver<ErrorModel, ErrorModel> BuildCriteria(DateTime? @from, DateTime? to, string applicationName, string username,
                                                string location, ISession session)
        {
            var listCriteria1 = session.QueryOver<ErrorModel>();

            if (@from.HasValue && to.HasValue)
            {
                listCriteria1.AndRestrictionOn(error => error.Time)
                             .IsBetween(@from)
                             .And(to);
            }

            if (!string.IsNullOrWhiteSpace(applicationName))
            {
                listCriteria1.WhereRestrictionOn(model => model.Application)
                             .IsLike(string.Format("%{0}%", applicationName));
            }

            if (!string.IsNullOrWhiteSpace(username))
            {
                listCriteria1.WhereRestrictionOn(model => model.Username)
                             .IsLike(string.Format("%{0}%", username));
            }

            if (!string.IsNullOrWhiteSpace(location))
            {
                listCriteria1.WhereRestrictionOn(model => model.ExceptionStackTrace)
                             .IsLike(string.Format("%{0}%", location));
            }

            return listCriteria1;
        }
    }
}