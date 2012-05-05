using System;
using Sioen.Layerless.Infrastructure.Data;

namespace Sioen.Layerless.Data.Entities
{
    public class User : Entity
    {
        public virtual string UserName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual DateTime BirthDate { get; set; }
    }
}
