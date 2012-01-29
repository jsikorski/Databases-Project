using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Client.Features.Flights;
using Common.Infrastucture;
using Common.Messages;
using Connection;

namespace Client.Commands.Flights
{
    public class SearchFlights : ICommand
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IConnectionProvider _connectionProvider;
        private readonly FlightsSearchData _flightsSearchData;

        public SearchFlights(
            IEventAggregator eventAggregator,
            IConnectionProvider connectionProvider,
            FlightsSearchData flightsSearchData)
        {
            _eventAggregator = eventAggregator;
            _connectionProvider = connectionProvider;
            _flightsSearchData = flightsSearchData;
        }

        public void Execute()
        {
            DBConnection dbConnection = _connectionProvider.GetConnection();
            IQueryable<FLY> flights =
                dbConnection.FLY.Where(
                    fly =>
                    fly.CONNECTION.AIRPORT.NAME.Contains(_flightsSearchData.From) &&
                    fly.CONNECTION.AIRPORT1.NAME.Contains(_flightsSearchData.To) && 
                    fly.FLY_DATE == _flightsSearchData.Date);

            if (_flightsSearchData.MaximumPrice != null)
            {
                flights = flights.Where(fly => fly.CONNECTION.PRICE < _flightsSearchData.MaximumPrice);
            }

            _eventAggregator.Publish(new FlightsFounded(flights.ToList()));
        }
    }
}
