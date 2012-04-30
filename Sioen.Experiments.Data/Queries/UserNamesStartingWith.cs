using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Criterion;
using Sioen.Experiments.Data.Entities;
using Sioen.Experiments.Infrastructure.Data;

namespace Sioen.Experiments.Data.Queries
{
    public class UserNamesStartingWith : Query<User>
    {
        public string Name { get; set; }

        public override IList<User> Execute(NHibernate.ISession session)
        {
            return session.CreateCriteria<User>().Add(Expression.InsensitiveLike("UserName", Name, MatchMode.Start)).List<User>();
        }
    }
}
