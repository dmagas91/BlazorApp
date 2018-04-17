using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.DAL.Models
{
    public class User : IdentityUser
    {        
        public string IdentityId { get; set; }        
        public string Location { get; set; }
        public string Locale { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
    }


}
