using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Administration.Commands;
using Administration.Commands.Airports;
using Administration.Infrastucture;
using Caliburn.Micro;
using Common;

namespace Administration.Features.Airports
{
    public class NewAirportViewModel : Screen
    {
        private readonly MainViewModel _mainViewModel;
        private readonly Func<AirportCreationData, AddAirport> _addAirportFactory;

        private string _airportName;
        public string AirportName
        {
            get { return _airportName; }
            set
            {
                _airportName = value;
                NotifyOfPropertyChange(() => CanAddAirport);
            }
        }

        private string _cityName;
        public string CityName
        {
            get { return _cityName; }
            set
            {
                _cityName = value;
                NotifyOfPropertyChange(() => CanAddAirport);
            }
        }

        private string _countryName;
        public string CountryName
        {
            get { return _countryName; }
            set
            {
                _countryName = value;
                NotifyOfPropertyChange(() => CanAddAirport);
            }
        }

        public bool CanAddAirport
        {
            get
            {
                return !string.IsNullOrEmpty(AirportName) &&
                       !string.IsNullOrEmpty(CityName) &&
                       !string.IsNullOrEmpty(CountryName);
            }
        }

        public ISymbolsProvider SymbolsProvider { get; set; }

        public NewAirportViewModel(
            MainViewModel mainViewModel,
            Func<AirportCreationData, AddAirport> addAirportFactory)
        {
            base.DisplayName = "Add airport";

            _mainViewModel = mainViewModel;
            _addAirportFactory = addAirportFactory;
        }

        public void AddAirport()
        {
            TryClose();
            var airportCreationData = new AirportCreationData(AirportName, CityName, CountryName);
            ICommand addAirport = _addAirportFactory(airportCreationData);
            CommandInvoker.InvokeBusy(addAirport, _mainViewModel);
        }
    }
}
