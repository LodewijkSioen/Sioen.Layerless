using System;
using Sioen.Layerless.Infrastructure.Data;

namespace Sioen.Layerless.Logic.Entities
{
    public class User : Entity
    {
        public virtual string UserName { get; set; }
        public virtual DateTime? BirthDate { get; set; }
    }
}
