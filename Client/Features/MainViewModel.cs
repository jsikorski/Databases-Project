using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Caliburn.Micro;
using Client.Commands.Cities;
using Client.Features.Flights;
using Client.Messages;
using Common.Infrastucture;

namespace Client.Features
{
    public class MainViewModel : Screen, IBusyScope, IHandle<CitiesNamesFounded>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IContainer _container;
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
            IEventAggregator eventAggregator,
            FlightsViewModel flightsViewModel, 
            IContainer container)
        {
            _eventAggregator = eventAggregator;
            _container = container;
            base.DisplayName = "Client";

            FlightsViewModel = flightsViewModel;
            flightsViewModel.SetBusyScope(this);
        }

        protected override void OnActivate()
        {
            _eventAggregator.Subscribe(this);
            ICommand command = _container.Resolve<GetCitiesNames>();
            CommandInvoker.InvokeBusy(command, this);
            base.OnActivate();
        }

        public void Handle(CitiesNamesFounded message)
        {
            _eventAggregator.Unsubscribe(this);
            (FlightsViewModel as INeedCitiesNames).SetCitiesNames(message.CitiesNames);
        }
    }
}
