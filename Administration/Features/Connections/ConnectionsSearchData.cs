using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Administration.Features.Connections
{
    public class ConnectionsSearchData
    {
        public string Symbol { get; private set; }
        public DateTime DepartureTime { get; private set; }
        public DateTime ArivalTime { get; private set; }
        public string WeekDay { get; private set; }
        public string FromAirportName { get; private set; }
        public string ToAirportName { get; private set; }

        public ConnectionsSearchData(string symbol, DateTime departureTime,
            DateTime arivalTime, string weekDay, string fromAirportName, string toAirportName)
        {
            ToAirportName = toAirportName;
            FromAirportName = fromAirportName;
            WeekDay = weekDay;
            ArivalTime = arivalTime;
            DepartureTime = departureTime;
            Symbol = symbol;
        }
    }
}
