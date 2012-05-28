using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sioen.Layerless.Web.Pages.User
{
    public class UserModel
    {
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}