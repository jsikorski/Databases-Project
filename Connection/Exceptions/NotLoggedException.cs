using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connection.Exceptions
{
    public class NotLoggedException : Exception
    {
        public NotLoggedException()
            : base("You are not logged in.")
        {

        }
    }
}
