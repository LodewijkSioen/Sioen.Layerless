using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace Sioen.Layerless.Infrastructure.Data
{
    public class AdHocQuery : Query<bool>
    {
        private Action<ISession> _query;

        public AdHocQuery(Action<ISession> query)
        {
            _query = query;
        }

        public override IList<bool> Execute(NHibernate.ISession session)
        {
            _query(session);
            return null;
        }
    }

    public static class QueryExtenstions
    {
        public static void Query(this IQueryExecutor db, Action<ISession> query)
        {
            db.Query(new AdHocQuery(query));
        }
    }
}
