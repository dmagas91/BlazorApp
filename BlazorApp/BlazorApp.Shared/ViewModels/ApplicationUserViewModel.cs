using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public long? FacebookId { get; set; }
        public string PictureUrl { get; set; }
    }
}
