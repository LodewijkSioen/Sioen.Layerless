using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Sioen.Layerless.Infrastructure.Command;
using Sioen.Layerless.Infrastructure.Data;

namespace Sioen.Layerless.Infrastructure
{
    public class InfrastructureInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IQueryExecutor>().ImplementedBy<QueryExecutor>()
                    .LifestylePerWebRequest(),
                Component.For<ICommandExecutor>().ImplementedBy<CommandExecutor>()
                    .LifestylePerWebRequest()
            );
        }
    }
}
