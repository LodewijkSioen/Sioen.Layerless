using System.Linq;
using NHibernate;
using NHibernate.Linq;
using NUnit.Framework;
using Sioen.Experiments.Infrastructure.Data;

namespace Sioen.Experiments.Tests.Data.Queries
{
    
    public abstract class TestFixtureRepository<T>
    {
        private ISessionFactory _sessionFactory;
        private ISession _session;
        private ITransaction _transaction;
        private bool _online;

        public IQueryable<T> Data { get; set; }

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            if (_online)
            {
                _sessionFactory = NHibernateConfigurator.BuildConfiguration().BuildSessionFactory();
            }
        }

        [SetUp]
        public void Setup()
        {
            Data = _online ? InitializeOnlineData() : InitializeOfflineData();
        }

        [TearDown]
        public void TearDown()
        {
            if (_online)
            {
                _transaction.Rollback();
                _session.Dispose();
            }
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            if (_online)
            {
                _sessionFactory.Dispose();
            }
        }

        protected abstract IQueryable<T> InitializeOfflineData();

        private IQueryable<T> InitializeOnlineData()
        {
            _session = _sessionFactory.OpenSession();
            _transaction = _session.BeginTransaction();
            return _session.Query<T>();
        }
    }
}
