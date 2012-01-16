using System.Collections.Generic;
using System.Linq;
using Administration.Features;
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
        private readonly MainViewModel _mainViewModel;

        public SearchAirports(
            IConnectionProvider connectionProvider,
            AirportsSearchData airportsSearchData,
            IEventAggregator eventAggregator,
            MainViewModel mainViewModel)
        {
            _connectionProvider = connectionProvider;
            _airportsSearchData = airportsSearchData;
            _eventAggregator = eventAggregator;
            _mainViewModel = mainViewModel;
        }

        public void Execute()
        {
            _mainViewModel.IsBusy = true;
            DBConnection dbConnection = _connectionProvider.GetConnection();
            IEnumerable<AIRPORT> airports = dbConnection.AIRPORT.Where(
                airport => airport.NAME.Contains(_airportsSearchData.AirportName) &&
                           airport.CITY.NAME.Contains(_airportsSearchData.CityName) &&
                           airport.CITY.COUNTRY.NAME.Contains(_airportsSearchData.CountryName));
            _eventAggregator.Publish(new AirportsFounded(airports));
        }
    }
}
