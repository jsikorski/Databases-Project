using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Caliburn.Micro;
using Client.Commands.Flights;
using Common.Infrastucture;
using Common.Messages;
using Connection;

namespace Client.Features.Flights
{
    public class FlightsViewModel : Screen, IBusyScopeSubscreen, IHandle<FlightsFounded>
    {
        private IBusyScope _busyScope;
        private readonly IEventAggregator _eventAggregator;
        private readonly Func<FlightsSearchData, SearchFlights> _searchFlightsFactory;
        private readonly IContainer _container;

        public BindableCollection<FlyViewModel> Flights { get; private set; }
        private FlyViewModel _selectedFly;
        public FlyViewModel SelectedFly
        {
            get { return _selectedFly; }
            set
            {
                _selectedFly = value;
                NotifyOfPropertyChange(() => CanShowFlyDetails);
            }
        }

        public string FromCityName { get; set; }
        public string ToCityName { get; set; }
        public string MaximumPrice { get; set; }
        public DateTime DisplayDate { get; set; }
        public DateTime SelectedDate { get; set; }

        public bool CanShowFlyDetails
        {
            get { return SelectedFly != null; }
        }

        public FlightsViewModel(
            IEventAggregator eventAggregator,
            Func<FlightsSearchData, SearchFlights> searchFlightsFactory,
            IContainer container)
        {
            _eventAggregator = eventAggregator;
            _searchFlightsFactory = searchFlightsFactory;
            _container = container;
            DisplayDate = DateTime.Now;
            SelectedDate = DisplayDate;

            Flights = new BindableCollection<FlyViewModel>();
        }

        public void ShowFlyDetails()
        {

        }

        public void SearchFlights()
        {
            _eventAggregator.Subscribe(this);
            int? maximumPrice = !string.IsNullOrEmpty(MaximumPrice) ? Convert.ToInt32(MaximumPrice) : (int?) null;
            var flightsSearchData = new FlightsSearchData(FromCityName, 
                ToCityName, maximumPrice, SelectedDate);
            ICommand command = _searchFlightsFactory(flightsSearchData);
            CommandInvoker.InvokeBusy(command, _busyScope);
        }

        public void Handle(FlightsFounded message)
        {
            _eventAggregator.Unsubscribe(this);
            Flights.Clear();
            Flights.AddRange(message.Flights.Select(fly => new FlyViewModel(fly)));
        }

        public void SetBusyScope(IBusyScope busyScope)
        {
            _busyScope = busyScope;
        }
    }
}
