using System.Collections.Generic;
using NHibernate;
using Sioen.Experiments.Infrastructure.Data;

namespace Sioen.Experiments.Tests.Data.Queries
{
    public class TestFixtureQuery<U>
    {
        private ISession _session;

        public IList<U> Execute(Query<U> query)
        {
            return query.Execute(_session);
        }
    }
}
