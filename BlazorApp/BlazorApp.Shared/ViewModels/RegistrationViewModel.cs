using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared.ViewModels
{
    public class RegistrationViewModel : ApplicationUserViewModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
    }           
}
