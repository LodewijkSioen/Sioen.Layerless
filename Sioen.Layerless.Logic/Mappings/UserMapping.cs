using NHibernate.Mapping.ByCode.Conformist;
using Sioen.Layerless.Logic.Entities;

namespace Sioen.Layerless.Logic.Mappings
{
    public class UserMapping : ClassMapping<User>
    {
        public UserMapping()
        {
            Property(p => p.UserName);
            Property(p => p.BirthDate);
        }
    }
}
