using System;
using System.Collections.Generic;
using System.Linq;

namespace Sioen.Layerless.Infrastructure.Data
{
    public interface IQueryExecutor : IDisposable
    {
        void Save(Entity entity);
        void Delete(Entity entity);
        T Get<T>(Guid id);
        T Get<T>(Query<T> query);

        IList<T> Query<T>(Query<T> query);
        IQueryable<T> Query<T>();
    }
}
