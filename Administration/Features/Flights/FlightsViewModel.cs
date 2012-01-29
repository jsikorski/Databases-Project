using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Administration.Commands;
using Administration.Commands.Flights;
using Administration.Messages;
using Autofac;
using Caliburn.Micro;
using Common.Infrastucture;
using Connection;

namespace Administration.Features.Flights
{
    public class FlightsViewModel : Screen, IBusyScopeSubscreen, IHandle<FlightsFounded>, IHandle<FlyRemoved>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly Func<FlightsSearchData, SearchFlights> _searchFlightsFactory;
        private readonly Func<FLY, RemoveFly> _removeFlyFactory;
        private readonly IContainer _container;
        private IBusyScope _busyScope;

        public BindableCollection<FlyViewModel> Flights { get; private set; }
        private FlyViewModel _selectedFly;
        public FlyViewModel SelectedFly
        {
            get { return _selectedFly; }
            set
            {
                _selectedFly = value;
                NotifyOfPropertyChange(() => CanRemoveFly);
            }
        }

        public string FlightSymbol { get; set; }
        public DateTime DisplayDate { get; private set; }
        public DateTime FlightDate { get; set; }
        public string ConnectionSymbol { get; set; }

        public bool CanRemoveFly
        {
            get { return SelectedFly != null; }
        }

        public FlightsViewModel(
            IEventAggregator eventAggregator, 
            Func<FlightsSearchData, SearchFlights> searchFlightsFactory,
            Func<FLY, RemoveFly> removeFlyFactory,
            IContainer container)
        {
            _eventAggregator = eventAggregator;
            _searchFlightsFactory = searchFlightsFactory;
            _removeFlyFactory = removeFlyFactory;
            _container = container;
            DisplayDate = DateTime.Now;
     
            Flights = new BindableCollection<FlyViewModel>();
        }

        public void SetBusyScope(IBusyScope busyScope)
        {
            _busyScope = busyScope;
        }

        public void SearchFlights()
        {
            _eventAggregator.Subscribe(this);
            var flightsSearchData = new FlightsSearchData(FlightSymbol, FlightDate, ConnectionSymbol);
            ICommand command = _searchFlightsFactory(flightsSearchData);
            CommandInvoker.InvokeBusy(command, _busyScope);
        }

        public void NewFly()
        {
            ICommand newFly = _container.Resolve<NewFly>();
            CommandInvoker.Execute(newFly);
        }

        public void RemoveFly()
        {
            _eventAggregator.Subscribe(this);
            ICommand removeFly = _removeFlyFactory(SelectedFly.Fly);
            CommandInvoker.Execute(removeFly);
        }

        public void Handle(FlightsFounded message)
        {
            _eventAggregator.Unsubscribe(this);
            Flights.Clear();
            Flights.AddRange(message.Flights.Select(fly => new FlyViewModel(fly)));
        }

        public void Handle(FlyRemoved message)
        {
            _eventAggregator.Unsubscribe(this);
            Flights.Remove(SelectedFly);
        }
    }
}
