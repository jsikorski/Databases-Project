using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Administration.Features.Flights;
using Administration.Messages;
using Caliburn.Micro;
using Connection;

namespace Administration.Commands.Flights
{
    public class SearchFlights : ICommand
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly IEventAggregator _eventAggregator;
        private readonly FlightsSearchData _flightsSearchData;

        public SearchFlights(
            IConnectionProvider connectionProvider, 
            IEventAggregator eventAggregator, 
            FlightsSearchData flightsSearchData)
        {
            _connectionProvider = connectionProvider;
            _eventAggregator = eventAggregator;
            _flightsSearchData = flightsSearchData;
        }

        public void Execute()
        {
            DBConnection dbConnection = _connectionProvider.GetConnection();
            IQueryable<FLY> flights = dbConnection.FLY.Where(
                fly => fly.SYMBOL.Contains(_flightsSearchData.Symbol) &&
                       fly.FLY_DATE == _flightsSearchData.Date &&
                       fly.CONNECTION_SYMBOL.Contains(_flightsSearchData.ConnectionSymbol));
            
            _eventAggregator.Publish(new FlightsFounded(flights));
        }
    }
}
