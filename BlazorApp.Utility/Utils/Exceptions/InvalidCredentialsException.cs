using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Utility.Utils.Exceptions
{
    public class InvalidCredentialsException : Exception
    {        

        public InvalidCredentialsException()
        : base("Invalid username or password.")
        {

        }
    }
}
