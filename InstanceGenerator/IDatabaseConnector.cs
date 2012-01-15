using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace InstanceGenerator
{
    internal interface IDatabaseConnector
    {
        IDbConnection Connection { get; }

        void Connect();
        void CloseConnection();
    }
}
