using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared.ModelsDTO
{
    public class ApplicationUserDTO
    {        
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }                
        public long? FacebookId { get; set; }
        public string PictureUrl { get; set; }
        public string Email { get; set; }
    }
}
