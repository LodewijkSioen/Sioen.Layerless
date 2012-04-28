using Castle.MicroKernel.Registration;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using Sioen.Experiments.Infrastructure.Data;

namespace Sioen.Experiments.Infrastructure.Installers
{
    public class NHibernateInstaller : IWindsorInstaller
    {
        private static ISessionFactory _factory;
        private static Configuration _config;

        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            _config = NHibernateConfigurator.BuildConfiguration();
            _factory = _config.BuildSessionFactory();

            container.Register(
                Component.For<ISessionFactory>().Instance(_factory),
                Component.For<Configuration>().Instance(_config),
                Component.For<ISession>()
                    .UsingFactoryMethod(k =>
                    {
                        var session = k.Resolve<ISessionFactory>().OpenSession();
                        CurrentSessionContext.Bind(session);
                        return session;
                    })
                    .LifestyleTransient()                
            );
        }
    }
}
