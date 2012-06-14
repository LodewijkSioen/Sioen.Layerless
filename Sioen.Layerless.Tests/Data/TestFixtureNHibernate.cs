using System;
using System.Linq;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using NUnit.Framework;
using Sioen.Layerless.Infrastructure.Data;
using Sioen.Layerless.Logic.Mappings;

namespace Sioen.Layerless.Tests.Data
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
            log4net.Config.XmlConfigurator.Configure();

            _useRealDatabase = ConnectToDatabase();
            if (_useRealDatabase)
            {
                Database.Create();
                var config = new Configuration()
                    .Proxy(p => p.ProxyFactoryFactory<NHibernate.Bytecode.DefaultProxyFactoryFactory>());
                config.SessionFactory().GenerateStatistics();
                config.SetInterceptor(new LogSqlInterceptor());
                config.ForSqlServerCE("CE").WithMappingsFromAssemblyOf<UserMapping>();

                SchemaHelper.BuildDatabase(config);
                _sessionFactory = config.BuildSessionFactory();                
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
            Boolean.TryParse(System.Configuration.ConfigurationManager.AppSettings["UseRealDatabase"], out result);

            return result;
        }
    }
}
