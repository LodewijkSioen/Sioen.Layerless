using NHibernate.Cfg;

namespace Sioen.Layerless.Infrastructure.Data
{
    public class SchemaHelper
    {
        public static void BuildDatabase(Configuration config)
        {
            new NHibernate.Tool.hbm2ddl.SchemaExport(config).Create(false, true);
        }

        public static void DropDatabase(Configuration config)
        {
            new NHibernate.Tool.hbm2ddl.SchemaExport(config).Drop(false, true);
        }
    }
}
