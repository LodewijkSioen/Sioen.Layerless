using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Sioen.Layerless.Data.Entities;
using Sioen.Layerless.Infrastructure.Data;

namespace Sioen.Layerless.Data.Queries
{
    public class UsersOlderThan : Query<User>
    {
        private DateTime _birthDate;
        public UsersOlderThan(int age)
        {
            _birthDate = DateTime.Now.Date.AddYears(-age);
        }

        public override IList<User> Execute(ISession session)
        {
            return session.CreateCriteria<User>().Add(Expression.Lt("BirthDate", _birthDate)).List<User>();
        }
    }
}
