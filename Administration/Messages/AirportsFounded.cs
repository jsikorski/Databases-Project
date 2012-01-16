using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Connection;

namespace Administration.Messages
{
    public class AirportsFounded
    {
        public IEnumerable<AIRPORT> Airports { get; private set; }

        public AirportsFounded(IEnumerable<AIRPORT> airports)
        {
            Airports = airports;
        }
    }
}
