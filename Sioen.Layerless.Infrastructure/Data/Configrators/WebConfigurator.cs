using NHibernate.Cfg;
using NHibernate.Context;

namespace Sioen.Layerless.Infrastructure.Data.Configrators
{
    public class WebConfigurator : INHibernateConfigurator
    {
        public void Extend(Configuration config)
        {
            config.CurrentSessionContext<WebSessionContext>();
        }
    }
}
