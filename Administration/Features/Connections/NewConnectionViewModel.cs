using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Administration.Commands;
using Administration.Commands.Airports;
using Administration.Commands.Connections;
using Administration.Features.Airports;
using Administration.Infrastucture;
using Administration.Messages;
using Caliburn.Micro;
using Connection;

namespace Administration.Features.Connections
{
    public class NewConnectionViewModel : Screen, IHandle<AirportsFounded>, IBusyScope
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly MainViewModel _mainViewModel;
        private readonly Func<ConnectionCreationData, AddConnection> _addConnectionFactory;
        private readonly Func<AirportsSearchData, SearchAirports> _searchAirportsFactory;

        private string _departureTime;
        public string DepartureTime
        {
            get { return _departureTime; }
            set
            {
                _departureTime = value;
                NotifyOfPropertyChange(() => CanAddConnection);
            }
        }

        private string _arivalTime;
        public string ArivalTime
        {
            get { return _arivalTime; }
            set
            {
                _arivalTime = value;
                NotifyOfPropertyChange(() => CanAddConnection);
            }
        }

        public IEnumerable<string> Days { get; private set; }
        private string _selectedDay;
        public string SelectedDay
        {
            get { return _selectedDay; }
            set
            {
                _selectedDay = value;
                NotifyOfPropertyChange(() => CanAddConnection);
            }
        }

        private string _price;
        public string Price
        {
            get { return _price; }
            set
            {
                _price = value;
                NotifyOfPropertyChange(() => CanAddConnection);
            }
        }

        private string _numberOfTickets;
        public string NumberOfTickets
        {
            get { return _numberOfTickets; }
            set
            {
                _numberOfTickets = value;
                NotifyOfPropertyChange(() => CanAddConnection);
            }
        }

        public BindableCollection<AIRPORT> Airports { get; set; }
        private AIRPORT _selectedFromAirport;
        public AIRPORT SelectedFromAirport
        {
            get { return _selectedFromAirport; }
            set
            {
                _selectedFromAirport = value;
                NotifyOfPropertyChange(() => CanAddConnection);
            }
        }

        private AIRPORT _selectedToAirport;
        public AIRPORT SelectedToAirport
        {
            get { return _selectedToAirport; }
            set
            {
                _selectedToAirport = value;
                NotifyOfPropertyChange(() => CanAddConnection);
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                NotifyOfPropertyChange(() => IsBusy);
            }
        }

        public bool CanAddConnection
        {
            get
            {
                return !string.IsNullOrEmpty(DepartureTime) &&
                       !string.IsNullOrEmpty(ArivalTime) &&
                       !string.IsNullOrEmpty(SelectedDay) &&
                       !string.IsNullOrEmpty(Price.Trim('_')) &&
                       !string.IsNullOrEmpty(NumberOfTickets.Trim('_')) &&
                       SelectedFromAirport != null &&
                       SelectedToAirport != null && 
                       SelectedFromAirport != SelectedToAirport;
            }
        }

        public NewConnectionViewModel(
            IEventAggregator eventAggregator,
            MainViewModel mainViewModel,
            Func<ConnectionCreationData, AddConnection> addConnectionFactory, 
            Func<AirportsSearchData, SearchAirports> searchAirportsFactory)
        {
            _eventAggregator = eventAggregator;
            _mainViewModel = mainViewModel;
            _addConnectionFactory = addConnectionFactory;
            _searchAirportsFactory = searchAirportsFactory;
            _eventAggregator.Subscribe(this);

            base.DisplayName = "Add connection";
                
            Days = new[] {"Monday", "Tuesday", "Thursday", "Wednesday", "Saturday", "Sunday", "Friday"};
        }

        protected override void OnActivate()
        {
            GetAirports();
        }

        public void AddConnection()
        {
            TryClose();
            var connectionCreationData = new ConnectionCreationData(DepartureTime, ArivalTime, 
                SelectedDay, Price, NumberOfTickets, SelectedFromAirport, SelectedToAirport);
            ICommand command = _addConnectionFactory(connectionCreationData);
            CommandInvoker.InvokeBusy(command, _mainViewModel);
        }

        public void Handle(AirportsFounded message)
        {
            Airports.AddRange(message.Airports);
        }

        private void GetAirports()
        {
            Airports = new BindableCollection<AIRPORT>();            
            var airportsSearchData = new AirportsSearchData(
                string.Empty, string.Empty, string.Empty, string.Empty);
            ICommand command = _searchAirportsFactory(airportsSearchData);
            CommandInvoker.InvokeBusy(command, this);
        }
    }
}
