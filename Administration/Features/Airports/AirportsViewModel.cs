using System.Windows;
using Administration.Commands;
using Administration.Commands.Airports;
using Administration.Infrastucture;
using Administration.Messages;
using Administration.Utils;
using Autofac;
using Caliburn.Micro;
using Connection;
using Microsoft.Windows.Controls;

namespace Administration.Features.Airports
{
    public class AirportsViewModel : Screen, IBusyScopeSubscreen, IHandle<AirportsFounded>, IHandle<AirportRemoved>
    {
        private readonly IContainer _container;
        private readonly IEventAggregator _eventAggregator;
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

        public string AirportName { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }

        public AirportsViewModel(
            IContainer container,
            IEventAggregator eventAggregator)
        {
            _container = container;
            _eventAggregator = eventAggregator;

            Airports = new BindableCollection<AIRPORT>();
        }

        public IResult SearchAirports()
        {
            _eventAggregator.Subscribe(this);

            var airportsSearchData = new AirportsSearchData(AirportName, CityName, CountryName);
            ICommand command = _container.Resolve<SearchAirports>(
                new NamedParameter("airportsSearchData", airportsSearchData));
            return new BusyCommandResult(command, _busyScope);
        }

        public void NewAirport()
        {
            ICommand command = _container.Resolve<NewAirport>();
            CommandInvoker.Execute(command);
        }

        public IResult RemoveAirport()
        {
            _eventAggregator.Subscribe(this);

            ICommand command = _container.Resolve<RemoveAirport>(new TypedParameter(typeof(AIRPORT), SelectedAirport));
            return new CommandResult(command);
        }

        public void SetBusyScope(IBusyScope busyScope)
        {
            _busyScope = busyScope;
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
