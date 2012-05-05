using NHibernate.Mapping.ByCode.Conformist;
using Sioen.Layerless.Data.Entities;

namespace Sioen.Layerless.Data.Mappings
{
    public class UserMapping : ClassMapping<User>
    {
        public UserMapping()
        {
            Property(p => p.UserName);
            Property(p => p.FirstName);
            Property(p => p.LastName);
            Property(p => p.BirthDate);
            Property(p => p.BirthDate);
        }
    }
}
