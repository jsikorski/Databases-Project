using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Features.Flights
{
    public class FlightsSearchData
    {
        public string From { get; private set; }
        public string To { get; private set; }
        public int? MaximumPrice { get; private set; }
        public DateTime Date { get; private set; }

        public FlightsSearchData(string @from, string to, int? maximumPrice, DateTime date)
        {
            From = @from;
            To = to;
            MaximumPrice = maximumPrice;
            Date = date;
        }
    }
}
