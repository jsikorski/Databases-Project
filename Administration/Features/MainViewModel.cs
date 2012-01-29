using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Administration.Features.Airports;
using Administration.Features.Connections;
using Administration.Features.Flights;
using Administration.Infrastucture;
using Caliburn.Micro;

namespace Administration.Features
{
    public class MainViewModel : Screen, IBusyScope
    {
        public IBusyScopeSubscreen ReservationsViewModel { get; private set; }
        public IBusyScopeSubscreen ConnectionsViewModel { get; private set; }
        public IBusyScopeSubscreen AirportsViewModel { get; private set; }
        public IBusyScopeSubscreen FlightsViewModel { get; private set; }

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

        public MainViewModel(
            AirportsViewModel airportsViewModel,
            ConnectionsViewModel connectionsViewModel, 
            FlightsViewModel flightsViewModel)
        {
            base.DisplayName = "Administration panel";

            ConnectionsViewModel = connectionsViewModel;
            ConnectionsViewModel.SetBusyScope(this);

            AirportsViewModel = airportsViewModel;
            AirportsViewModel.SetBusyScope(this);

            FlightsViewModel = flightsViewModel;
            FlightsViewModel.SetBusyScope(this);
        }
    }
}
