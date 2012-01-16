using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Administration.Commands;
using Administration.Commands.Airports;
using Administration.Infrastucture;
using Autofac;
using Caliburn.Micro;
using Common;

namespace Administration.Features.Airports
{
    public class NewAirportViewModel : Screen
    {
        private readonly IContainer _container;
        private readonly MainViewModel _mainViewModel;

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
            IContainer container,
            MainViewModel mainViewModel)
        {
            base.DisplayName = "Add airport";

            _container = container;
            _mainViewModel = mainViewModel;
        }

        public IResult AddAirport()
        {
            TryClose();
            var airportCreationData = new AirportCreationData(AirportName, CityName, CountryName);
            ICommand addAirport = _container.Resolve<AddAirport>(new NamedParameter("airportCreationData", airportCreationData));
            return new BusyCommandResult(addAirport, _mainViewModel);
        }
    }
}
