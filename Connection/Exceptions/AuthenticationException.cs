using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connection.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException()
            : base("Username or password is not correct.")
        {

        }
    }
}
