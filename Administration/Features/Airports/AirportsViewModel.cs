using System;
using System.Windows;
using Administration.Commands;
using Administration.Commands.Airports;
using Administration.Messages;
using Autofac;
using Caliburn.Micro;
using Common.Infrastucture;
using Connection;
using Microsoft.Windows.Controls;

namespace Administration.Features.Airports
{
    public class AirportsViewModel : Screen, IBusyScopeSubscreen, IHandle<AirportsFounded>, IHandle<AirportRemoved>
    {
        private readonly IContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private readonly Func<AirportsSearchData, SearchAirports> _searchAirportsFactory;
        private readonly Func<AIRPORT, RemoveAirport> _removeAirportFactory;
        private IBusyScope _busyScope;

        public BindableCollection<AIRPORT> Airports { get; set; }
        
        private AIRPORT _selectedAirport;
        public AIRPORT SelectedAirport
        {
            get { return _selectedAirport; }
            set
            {
                _selectedAirport = value;
                NotifyOfPropertyChange(() => CanRemoveAirport);
            }
        }

        public bool CanRemoveAirport
        {
            get
            {
                return SelectedAirport != null;
            }
        }

        public string AirportSymbol { get; set; }
        public string AirportName { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }

        public AirportsViewModel(
            IContainer container,
            IEventAggregator eventAggregator,
            Func<AirportsSearchData, SearchAirports> searchAirportsFactory,
            Func<AIRPORT, RemoveAirport> removeAirportFactory)
        {
            _container = container;
            _eventAggregator = eventAggregator;
            _searchAirportsFactory = searchAirportsFactory;
            _removeAirportFactory = removeAirportFactory;

            Airports = new BindableCollection<AIRPORT>();
        }

        public void SetBusyScope(IBusyScope busyScope)
        {
            _busyScope = busyScope;
        }

        public void SearchAirports()
        {
            _eventAggregator.Subscribe(this);

            var airportsSearchData = new AirportsSearchData(
                AirportSymbol, AirportName, CityName, CountryName);
            ICommand command = _searchAirportsFactory(airportsSearchData);
            CommandInvoker.InvokeBusy(command, _busyScope);
        }

        public void NewAirport()
        {
            ICommand command = _container.Resolve<NewAirport>();
            CommandInvoker.Execute(command);
        }

        public void RemoveAirport()
        {
            _eventAggregator.Subscribe(this);

            ICommand command = _removeAirportFactory(SelectedAirport);
            CommandInvoker.Execute(command);
        }

        public void Handle(AirportsFounded message)
        {
            Airports.Clear();
            Airports.AddRange(message.Airports);

            _eventAggregator.Unsubscribe(this);
        }

        public void Handle(AirportRemoved message)
        {
            Airports.Remove(SelectedAirport);
            
            _eventAggregator.Unsubscribe(this);
        }
    }
}
