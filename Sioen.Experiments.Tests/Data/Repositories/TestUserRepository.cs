using System;
using System.Linq;
using NUnit.Framework;
using Sioen.Experiments.Data.Entities;
using Sioen.Experiments.Data.Repositories;

namespace Sioen.Experiments.Tests.Data.Queries
{
    public class TestUserRepository : TestFixtureRepository<User>
    {
        protected override IQueryable<User> InitializeOfflineData()
        {
            return new[]{
                new User{ UserName = "John", BirthDate = DateTime.Now.AddYears(-11) },
                new User{ UserName = "Eddy", BirthDate = DateTime.Now.AddYears(-9) }
            }.AsQueryable();
        }

        public void TestAllUsersOlderThan()
        {
            //Act
            var result = Data.AllUsersOlderThan(10).ToList();

            //Assert
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].UserName, Is.EqualTo("John"));
        }
    }
}
