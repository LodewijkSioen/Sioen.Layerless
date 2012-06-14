using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;

namespace Sioen.Layerless.Infrastructure.Data
{
    public static class ConfigurationExtentions
    {
        public static Configuration ForWeb(this Configuration config)
        {
            return config.CurrentSessionContext<WebSessionContext>();
        }

        public static Configuration ForSqlServerCE(this Configuration config, string connectionStringName)
        {
            return config
               .DataBaseIntegration(db =>
               {
                   db.ConnectionStringName = connectionStringName;
                   db.Dialect<MsSqlCe40CustomDialect>();
                   db.BatchSize = 500;
                   db.LogSqlInConsole = true;
               });
        }

        public static Configuration WithMappingsFromAssemblyOf<T>(this Configuration config)
        {
            var mapper = new ModelMapper();

            mapper.BeforeMapClass += (modelInspector, type, classCustomizer) =>
            {
                var idProperty = typeof(Entity).GetProperty("Id");

                classCustomizer.Id(idProperty, c => 
                { 
                    c.Column(type.Name + "Id");                    
                    c.Generator(Generators.GuidComb);
                });
                classCustomizer.Table(ReservedTableNameHandler(type));
            };

            mapper.AddMappings(from t in typeof(T).Assembly.GetExportedTypes()
                               where t.Namespace == typeof(T).Namespace
                               select t);

            config.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
            return  config;
        }

        private static readonly List<string> ReservedSqlKeywords = new List<string> { "Order", "User", "Index" };
        private static string ReservedTableNameHandler(Type type)
        {
            // http://www.nhforge.org/doc/nh/en/index.html#mapping-quotedidentifiers
            var tableName = type.Name;
            return ReservedSqlKeywords.Contains(type.Name)
                ? "`" + tableName + "`"
                : tableName;
        }
    }
}
