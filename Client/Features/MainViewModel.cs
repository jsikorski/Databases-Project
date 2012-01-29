using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Client.Features.Flights;
using Common.Infrastucture;

namespace Client.Features
{
    public class MainViewModel : Screen, IBusyScope
    {
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

        public IBusyScopeSubscreen FlightsViewModel { get; set; }

        public MainViewModel(
            FlightsViewModel flightsViewModel)
        {
            base.DisplayName = "Client";

            FlightsViewModel = flightsViewModel;
            flightsViewModel.SetBusyScope(this);
        }
    }
}
