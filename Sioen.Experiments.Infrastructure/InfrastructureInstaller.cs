using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Sioen.Experiments.Infrastructure.Data;

namespace Sioen.Experiments.Infrastructure
{
    public class InfrastructureInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IQueryExecutor>().ImplementedBy<QueryExecutor>()
                    .LifestylePerWebRequest()
            );
        }
    }
}
