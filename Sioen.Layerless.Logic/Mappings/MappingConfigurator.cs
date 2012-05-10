using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Sioen.Layerless.Infrastructure.Data;

namespace Sioen.Layerless.Logic.Mappings
{
    public class MappingConfigurator : INHibernateConfigurator
    {
        public void Extend(NHibernate.Cfg.Configuration config)
        {
            var mapper = new ModelMapper();

            mapper.BeforeMapClass += (modelInspector, type, classCustomizer) =>
            {
                classCustomizer.Id(c => c.Column(type.Name + "Id"));
                classCustomizer.Id(c => c.Generator(Generators.GuidComb));
                classCustomizer.Table(ReservedTableNameHandler(type));
            };

            mapper.AddMappings(from t in this.GetType().Assembly.GetExportedTypes()
                               where t.IsSubclassOf(typeof(ClassMapping<>))
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
