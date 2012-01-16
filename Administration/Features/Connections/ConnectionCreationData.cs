using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Connection;

namespace Administration.Features.Connections
{
    public class ConnectionCreationData
    {
        public ConnectionCreationData(string departureTime, string arivalTime, 
            string weekDay, string price, string numberOfTickets, AIRPORT @from, AIRPORT to)
        {
            To = to;
            From = @from;
            NumberOfTickets = numberOfTickets;
            Price = price;
            WeekDay = weekDay;
            ArivalTime = arivalTime;
            DepartureTime = departureTime;
        }

        public string DepartureTime { get; private set; }
        public string ArivalTime { get; private set; }
        public string WeekDay { get; private set; }
        public string Price { get; private set; }
        public string NumberOfTickets { get; private set; }
        public AIRPORT From { get; private set; }
        public AIRPORT To { get; private set; }
    }
}
