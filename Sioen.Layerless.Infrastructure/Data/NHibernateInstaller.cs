using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using NHibernate;
using NHibernate.Cfg;

namespace Sioen.Layerless.Infrastructure.Data
{
    public class NHibernateInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
                Component.For<Configuration>()
                    .UsingFactoryMethod(CreateConfiguration)
                    .LifestyleSingleton(),

                Component.For<ISessionFactory>()
                    .UsingFactoryMethod(k => k.Resolve<Configuration>().BuildSessionFactory())
                    .LifestyleSingleton(),

                Component.For<ISession>()
                    .UsingFactoryMethod(k => k.Resolve<ISessionFactory>().OpenSession())
                    .LifestyleTransient()
            );
        }

        private Configuration CreateConfiguration(IKernel kernel)
        {
            var config = new Configuration()
                .Proxy(p => p.ProxyFactoryFactory<NHibernate.Bytecode.DefaultProxyFactoryFactory>());
#if DEBUG
            config.SessionFactory().GenerateStatistics();
#endif

            foreach(var extender in kernel.ResolveAll<INHibernateConfigurator>())
            {
                extender.Extend(config);
            }
            return config;
        }
    }
}
