using System.Collections.Generic;
using Connection;

namespace Common.Messages
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
