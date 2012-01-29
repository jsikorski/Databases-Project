using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Administration.Commands;
using Administration.Commands.Flights;
using Administration.Infrastucture;
using Administration.Messages;
using Autofac;
using Caliburn.Micro;

namespace Administration.Features.Flights
{
    public class FlightsViewModel : IBusyScopeSubscreen, IHandle<FlightsFounded>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly Func<FlightsSearchData, SearchFlights> _searchFlightsFactory;
        private readonly IContainer _container;
        private IBusyScope _busyScope;

        public BindableCollection<FlyViewModel> Flights { get; private set; }
        public FlyViewModel SelectedFly { get; set; }

        public string FlightSymbol { get; set; }
        public DateTime DisplayDate { get; private set; }
        public DateTime FlightDate { get; set; }
        public string ConnectionSymbol { get; set; }

        public FlightsViewModel(
            IEventAggregator eventAggregator, 
            Func<FlightsSearchData, SearchFlights> searchFlightsFactory,
            IContainer container)
        {
            _eventAggregator = eventAggregator;
            _searchFlightsFactory = searchFlightsFactory;
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

        }

        public void Handle(FlightsFounded message)
        {
            _eventAggregator.Unsubscribe(this);
            Flights.Clear();
            Flights.AddRange(message.Flights.Select(fly => new FlyViewModel(fly)));
        }
    }
}
