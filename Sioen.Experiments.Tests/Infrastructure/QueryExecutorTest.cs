using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using Sioen.Experiments.Infrastructure.Data;

namespace Sioen.Experiments.Tests.Infrastructure
{
    [TestFixture]
    public class QueryExecutorTest
    {
        public IQueryExecutor Executor;
        public ISession Session;

        [SetUp]
        public void Setup()
        {
            Session = MockRepository.GenerateMock<ISession>();
            Executor = new QueryExecutor(Session);
        }

        [Test]
        public void WhenSaveIsCalledThisIsRoutedToTheISession()
        {
            //Arrange
            var entity = new TestEntity();

            //Act
            Executor.Save(entity);

            //Assert
            Session.AssertWasCalled(s => s.SaveOrUpdate(entity));
        }

        [Test]
        public void WhenDeleteIsCalledThisIsRoutedToTheISession()
        {
            //Arrange
            var entity = new TestEntity();

            //Act
            Executor.Delete(entity);

            //Assert
            Session.AssertWasCalled(s => s.Delete(entity));
        }

        [Test]
        public void WhenGetByIdIsCalledThisIsRoutedToTheISession()
        {
            //Arrange
            var id = Guid.NewGuid();

            //Act
            Executor.Get<TestEntity>(id);

            //Assert
            Session.AssertWasCalled(s => s.Get<TestEntity>(id));
        }

        [Test]
        public void WhenGetByQueryIsCalledTheDefaultValueIsReturnedForEmptyList()
        {
            //Arrange
            var query = MockRepository.GenerateMock<Query<TestEntity>>();            
            var list = new List<TestEntity>();
            query.Expect(q => q.Execute(Session)).Return(list);

            //Act
            var result = Executor.Get(query);

            //Assert
            query.VerifyAllExpectations();
            Assert.That(result, Is.Null);
        }

        [Test]
        public void WhenGetByQueryIsCalledTheOnlyResultIsReturned()
        {
            //Arrange
            var query = MockRepository.GenerateMock<Query<TestEntity>>();
            var entity = new TestEntity();
            var list = new List<TestEntity> { entity };
            query.Expect(q => q.Execute(Session)).Return(list);

            //Act
            var result = Executor.Get(query);

            //Assert
            query.VerifyAllExpectations();
            Assert.That(result, Is.EqualTo(entity));
        }

        [Test]
        public void WhenGetByQueryIsCalledAnExceptionIsThrownIfMoreThanOneResult()
        {
            //Arrange
            var query = MockRepository.GenerateMock<Query<TestEntity>>();
            var list = new List<TestEntity> { new TestEntity(), new TestEntity() };
            query.Expect(q => q.Execute(Session)).Return(list);

            //Act, Assert            
            Assert.Throws<InvalidOperationException>(() => Executor.Get(query));
        }

        [Test]
        public void WhenExecutingQueryTheISessionIsPassedIntoTheQuery()
        {
            //Arrange
            var query = MockRepository.GenerateMock<Query<TestEntity>>();
            var list = new List<TestEntity>();
            query.Expect(q => q.Execute(Session)).Return(list);

            //Act
            var result = Executor.Query(query);

            //Assert
            query.VerifyAllExpectations();
            Assert.That(result, Is.EqualTo(list));
        }

        [Test]
        public void WhenQueryingLinqToNHibernateIsUsed()
        {
            //act
            Executor.Query<TestEntity>();

            //Assert
            Session.AssertWasCalled(s => s.Query<TestEntity>());
        }

        [Test]
        public void WhenUsingTheExecutorTheSessionAndTransactionAreProperlyHandled()
        {
            //Arrange
            var transaction = MockRepository.GenerateMock<ITransaction>();
            transaction.Expect(t => t.IsActive).Return(true);
            Session.Expect(s => s.BeginTransaction()).Return(transaction);            

            //act
            using (new QueryExecutor(Session)) { }

            //Assert
            transaction.AssertWasCalled(t => t.Commit());
            transaction.AssertWasCalled(t => t.Dispose());
            Session.AssertWasCalled(s => s.Dispose());
        }

        public class TestEntity : Entity
        { }        
    }
}
