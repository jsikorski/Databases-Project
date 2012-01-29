using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Connection;

namespace Administration.Messages
{
    public class FlightsFounded
    {
        public IEnumerable<FLY> Flights { get; private set; }

        public FlightsFounded(IEnumerable<FLY> flights)
        {
            Flights = flights;
        }
    }
}
