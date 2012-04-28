using System;
using System.Collections.Generic;
using System.Linq;

namespace Sioen.Experiments.Infrastructure.Data
{
    public interface IQueryExecutor
    {
        void Save(Entity entity);
        void Delete(Entity entity);
        T Get<T>(Guid id);
        T Get<T>(Query<T> query);

        IList<T> Query<T>(Query<T> query);
        IQueryable<T> Query<T>();
    }
}
