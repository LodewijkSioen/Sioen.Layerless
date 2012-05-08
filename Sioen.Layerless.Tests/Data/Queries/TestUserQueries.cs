using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Sioen.Layerless.Logic.Entities;
using Sioen.Layerless.Logic.Queries;

namespace Sioen.Layerless.Tests.Data.Queries
{
    public class TestUserQueries : TestFixtureNHibernate<User>
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
            //Arrange
            var query = new UsersOlderThan(10);

            //Act
            var result = query.Execute(Session);

            //Assert
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].UserName, Is.EqualTo("John"));
        }

        [Test]
        public void TestUserNamesStartingWith()
        {
            //Arrange
            var query = new UserNamesStartingWith() { Name = "J" };

            //Act
            var result = query.Execute(Session);

            //Assert
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].UserName, Is.EqualTo("John"));
        }
    }
}
