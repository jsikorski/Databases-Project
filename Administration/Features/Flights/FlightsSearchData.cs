using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Administration.Features.Flights
{
    public class FlightsSearchData
    {
        public string Symbol { get; private set; }
        public DateTime Date { get; private set; }
        public string ConnectionSymbol { get; private set; }

        public FlightsSearchData(string symbol, DateTime date, string connectionSymbol)
        {
            ConnectionSymbol = connectionSymbol;
            Date = date;
            Symbol = symbol;
        }
    }
}
