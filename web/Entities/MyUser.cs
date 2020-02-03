using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web.Entities
{
    public class MyUser : IdentityUser
    {
        [Required]
        public string DisplayName { get; set; }
    }
}
