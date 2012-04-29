using System;
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
        private bool _useRealDatabase = Boolean.Parse(System.Configuration.ConfigurationSettings.AppSettings["UseRealDatabase"]);

        public IQueryable<T> Data { get; set; }

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            if (_useRealDatabase)
            {
                NHibernateConfigurator.BuildDatabase();
                _sessionFactory = NHibernateConfigurator.BuildConfiguration().BuildSessionFactory();
                using (var session = _sessionFactory.OpenSession())
                {
                    using (var tx = session.BeginTransaction())
                    {
                        foreach (var entity in InitializeOfflineData())
                        {
                            session.Save(entity);
                        }
                        tx.Commit();
                    }
                }
            }
        }

        [SetUp]
        public void Setup()
        {
            Data = _useRealDatabase ? InitializeOnlineData() : InitializeOfflineData();
        }

        [TearDown]
        public void TearDown()
        {
            if (_useRealDatabase)
            {
                _transaction.Rollback();
                _session.Dispose();
            }
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            if (_useRealDatabase)
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
