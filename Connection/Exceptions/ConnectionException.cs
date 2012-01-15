using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connection.Exceptions
{
    public class ConnectionException : Exception
    {
        public ConnectionException()
            : base("Cannot connect to the databse.")
        {

        }
    }
}
