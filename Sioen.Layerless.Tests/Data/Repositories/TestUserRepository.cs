using System;
using System.Linq;
using NUnit.Framework;
using Sioen.Layerless.Logic.Entities;
using Sioen.Layerless.Logic.Repositories;

namespace Sioen.Layerless.Tests.Data.Queries
{
    public class TestUserRepository : TestFixtureNHibernate<User>
    {
        protected override IQueryable<User> CreateSandboxData()
        {
            return new[]{
                new User{ UserName = "John", BirthDate = DateTime.Now.AddYears(-11) },
                new User{ UserName = "Eddy", BirthDate = DateTime.Now.AddYears(-9) }
            }.AsQueryable();
        }

        [Test]
        public void TestAllUsersOlderThan()
        {
            //Act
            var result = Data.AllUsersOlderThan(10).ToList();

            //Assert
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].UserName, Is.EqualTo("John"));
        }

        [Test]
        public void TestUserNamesStartsWith()
        {
            //Act
            var result = Data.UserNamesStartingWith("J").ToList();

            //Assert
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].UserName, Is.EqualTo("John"));
        }

        [Test]
        public void TestCompositeQuery() 
        { 
            //act
            var result = Data
                .AllUsersOlderThan(8)
                .UserNamesStartingWith("E")
                .ToList();

            //Assert
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].UserName, Is.EqualTo("Eddy"));
        }

        [Test]
        public void TestProjection()
        {
            //Act
            var result = Data.Select(u => u.UserName).ToList();

            //Assert
            Assert.That(LogSqlInterceptor.LastExecutedQuery, Is.EqualTo("select user0_.UserName as col_0_0_ from [User] user0_"));            
        }

    }
}
