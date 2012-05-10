using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Cfg;
using NHibernate.Dialect;

namespace Sioen.Layerless.Infrastructure.Data.Configurators
{
    public class SqlCeConfigurator : INHibernateConfigurator
    {
        private string _connectionStringName;        

        public SqlCeConfigurator(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
        }

        public void Extend(Configuration config)
        {
            config
               .DataBaseIntegration(db =>
               {
                   db.ConnectionStringName = _connectionStringName;
                   db.Dialect<MsSqlCe40CustomDialect>();
                   db.BatchSize = 500;
               });
        }
    }
}
