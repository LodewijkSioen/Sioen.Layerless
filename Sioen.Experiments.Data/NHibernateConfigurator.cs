using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;

namespace Sioen.Experiments.Infrastructure.Data
{
    public static class NHibernateConfigurator
    {
        public static Configuration BuildConfiguration()
        {
            var config = new Configuration();
            config.Proxy(p => p.ProxyFactoryFactory<NHibernate.Bytecode.DefaultProxyFactoryFactory>())
               .DataBaseIntegration(db =>
               {
                   db.ConnectionStringName = "CE";
                   db.Dialect<MsSqlCe40CustomDialect>();
                   db.BatchSize = 500;                   
               })
               .CurrentSessionContext<WebSessionContext>()
               .SessionFactory().GenerateStatistics();

            AddMappings(config);

            return config;
        }

        public static void BuildDatabase()
        {
            new NHibernate.Tool.hbm2ddl.SchemaExport(BuildConfiguration()).Create(false, true);
        }

        private static void AddMappings(Configuration config)
        {
            var mapper = new ModelMapper();

            mapper.BeforeMapClass += (modelInspector, type, classCustomizer) =>
            {
                classCustomizer.Id(c => c.Column(type.Name + "Id"));
                classCustomizer.Id(c => c.Generator(Generators.GuidComb));
                classCustomizer.Table(ReservedTableNameHandler(type));
            };

            mapper.AddMappings(from t in typeof(NHibernateConfigurator).Assembly.GetExportedTypes()
                               where t.Namespace.EndsWith("Mappings")
                               select t);

            config.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
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
