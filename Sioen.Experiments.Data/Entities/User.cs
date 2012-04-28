using System;
using Sioen.Experiments.Infrastructure.Data;

namespace Sioen.Experiments.Data.Entities
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
