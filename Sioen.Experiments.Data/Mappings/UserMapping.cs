using NHibernate.Mapping.ByCode.Conformist;
using Sioen.Experiments.Data.Entities;

namespace Sioen.Experiments.Data.Mappings
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
