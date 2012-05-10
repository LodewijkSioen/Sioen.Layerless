using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Cfg;

namespace Sioen.Layerless.Infrastructure.Data
{
    public interface INHibernateConfigurator
    {
        void Extend(Configuration config);
    }
}
