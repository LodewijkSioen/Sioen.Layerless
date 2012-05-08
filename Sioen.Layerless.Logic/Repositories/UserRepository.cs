using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sioen.Layerless.Logic.Entities;

namespace Sioen.Layerless.Logic.Repositories
{
    public static class UserRepository
    {
        public static IQueryable<User> AllUsersOlderThan(this IQueryable<User> users, int age)
        {
            return from u in users
                   where u.BirthDate.HasValue && u.BirthDate.Value.Date < DateTime.Now.Date.AddYears(-age)
                   select u;
        }

        public static IQueryable<User> UserNamesStartingWith(this IQueryable<User> users, string name)
        {
            return from u in users
                   where u.UserName.StartsWith(name)
                   select u;
        }
    }
}
