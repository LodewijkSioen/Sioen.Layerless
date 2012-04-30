using System;
using System.Configuration;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using NUnit.Framework;
using Sioen.Experiments.Infrastructure.Data;

namespace Sioen.Experiments.Tests.Data
{    
    [TestFixture]
    public abstract class TestFixtureNHibernate<T>
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly bool _useRealDatabase;
        private ITransaction _transaction;

        public IQueryable<T> Data { get; private set; }
        public ISession Session { get; private set; }

        public TestFixtureNHibernate()
        {
            _useRealDatabase = ConnectToDatabase();
            if (_useRealDatabase)
            {
                Database.Create();
                NHibernateConfigurator.BuildDatabase();
                _sessionFactory = NHibernateConfigurator.BuildConfiguration().BuildSessionFactory();                
            }
        }
        
        [SetUp]
        public void Setup()
        {
            Data = _useRealDatabase ? InitializeDatabase() : CreateSandboxData();
        }

        [TearDown]
        public void TearDown()
        {
            if (_useRealDatabase)
            {
                _transaction.Rollback();
                Session.Dispose();
            }
        }

        protected abstract IQueryable<T> CreateSandboxData();

        private IQueryable<T> InitializeDatabase()
        {
            Session = _sessionFactory.OpenSession();
            _transaction = Session.BeginTransaction();

            foreach (var entity in CreateSandboxData())
            {
                Session.Save(entity);
            }

            return Session.Query<T>();
        }

        private Boolean ConnectToDatabase()
        {
            bool result;
            Boolean.TryParse(ConfigurationManager.AppSettings["UseRealDatabase"], out result);

            return result;
        }
    }
}
