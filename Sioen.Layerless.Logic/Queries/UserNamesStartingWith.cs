using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Criterion;
using Sioen.Layerless.Logic.Entities;
using Sioen.Layerless.Infrastructure.Data;

namespace Sioen.Layerless.Logic.Queries
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
