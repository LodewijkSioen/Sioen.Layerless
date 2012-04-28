using System.Collections.Generic;
using NHibernate;

namespace Sioen.Experiments.Infrastructure.Data
{
    public abstract class Query<T>
    {
        public abstract IList<T> Execute(ISession session);
    }
}
