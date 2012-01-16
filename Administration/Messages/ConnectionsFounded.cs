using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Connection;

namespace Administration.Messages
{
    public class ConnectionsFounded
    {
        public IEnumerable<CONNECTION> Connections { get; private set; }

        public ConnectionsFounded(IEnumerable<CONNECTION> connections)
        {
            Connections = connections;
        }
    }
}
