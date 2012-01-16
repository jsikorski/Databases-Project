using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connection.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException()
            : base("Some problem occured while performing operation.")
        {

        }
    }
}
