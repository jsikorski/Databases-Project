using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Administration.Features.Connections
{
    public class ConnectionsSearchData
    {
        public string Symbol { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArivalTime { get; set; }
        public string WeekDay { get; set; }
        public string FromAirportName { get; set; }
        public string ToAirportName { get; set; }
    }
}
