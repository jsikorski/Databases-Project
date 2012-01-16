using System.Collections.Generic;
using System.Linq;
using Administration.Features.Airports;
using Administration.Messages;
using Caliburn.Micro;
using Connection;

namespace Administration.Commands.Airports
{
    public class SearchAirports : ICommand
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly AirportsSearchData _airportsSearchData;
        private readonly IEventAggregator _eventAggregator;

        public SearchAirports(
            IConnectionProvider connectionProvider,
            AirportsSearchData airportsSearchData,
            IEventAggregator eventAggregator)
        {
            _connectionProvider = connectionProvider;
            _airportsSearchData = airportsSearchData;
            _eventAggregator = eventAggregator;
        }

        public void Execute()
        {
            DBConnection dbConnection = _connectionProvider.GetConnection();
            IEnumerable<AIRPORT> airports = dbConnection.AIRPORT.Where(
                airport => airport.NAME.Contains(_airportsSearchData.AirportName) &&
                           airport.CITY.NAME.Contains(_airportsSearchData.CityName) &&
                           airport.CITY.COUNTRY.NAME.Contains(_airportsSearchData.CountryName));
            _eventAggregator.Publish(new AirportsFounded(airports));
        }
    }
}
