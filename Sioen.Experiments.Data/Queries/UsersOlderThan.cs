using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Sioen.Experiments.Data.Entities;
using Sioen.Experiments.Infrastructure.Data;

namespace Sioen.Experiments.Data.Queries
{
    public class UsersOlderThan : Query<User>
    {
        private int _age;
        public UsersOlderThan(int age)
        {
            _age = age;
        }

        public override IList<User> Execute(ISession session)
        {
            return session.CreateCriteria<User>().Add(Expression.Gt("BirthDate", _age)).List<User>();
        }
    }
}
