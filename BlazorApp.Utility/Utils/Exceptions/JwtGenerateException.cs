using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Utility.Utils.Exceptions
{
    public class JwtGenerateException : Exception
    {
        public JwtGenerateException() { }

        public JwtGenerateException(string username)
        : base(String.Format("Error while generating Jwt for user : {0}", username))
        {

        }
    }
    
}
