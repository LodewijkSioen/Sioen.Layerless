using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Context;
using NHibernate.Linq;

namespace Sioen.Experiments.Infrastructure.Data
{
    public class QueryExecutor : IQueryExecutor, IDisposable
    {
        private ISession _session;
        ITransaction _transaction;

        public QueryExecutor(ISession session)
        {
            _session = session;
            _transaction = session.BeginTransaction();
        }

        public void Save(Entity entity)
        {
            _session.SaveOrUpdate(entity);
        }

        public void Delete(Entity entity)
        {
            _session.Delete(entity);
        }

        public T Get<T>(Guid id)
        {
            return _session.Get<T>(id);
        }

        public T Get<T>(Query<T> query)
        {
            return Query(query).SingleOrDefault(null);
        }

        public IList<T> Query<T>(Query<T> query)
        {
            return query.Execute(_session);
        }

        public IQueryable<T> Query<T>()
        {
            return _session.Query<T>();
        }

        public void Dispose()
        {
            if (_transaction.IsActive)
            {
                _transaction.Commit();
                _transaction.Dispose();
            }

            _session.Dispose();
            CurrentSessionContext.Unbind(_session.SessionFactory);
        }
    }
}
