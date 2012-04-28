using System.Linq;
using NHibernate;
using NHibernate.Linq;
using NUnit.Framework;

namespace Sioen.Experiments.Tests.Data.Queries
{
    [TestFixture]
    public abstract class TestFixtureRepository<T>
    {
        private ISessionFactory _sessionFactory;
        private ISession _session;
        private bool _online;

        public IQueryable<T> Data { get; set; }

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _sessionFactory = null;
        }

        [SetUp]
        public void Setup()
        {
            Data = _online ? InitializeOnlineData() : InitializeOfflineData();
        }

        protected abstract IQueryable<T> InitializeOfflineData();

        private IQueryable<T> InitializeOnlineData()
        {
            _session = _sessionFactory.OpenSession();
            return _session.Query<T>();
        }
    }
}
