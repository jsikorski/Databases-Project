using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Administration.Commands;
using Administration.Commands.Connections;
using Administration.Commands.Flights;
using Administration.Features.Connections;
using Administration.Messages;
using Caliburn.Micro;
using Common.Infrastucture;
using Connection;

namespace Administration.Features.Flights
{
    public class NewFlyViewModel : Screen, IBusyScope, IHandle<ConnectionsFounded>
    {
        private readonly MainViewModel _mainViewModel;
        private readonly IEventAggregator _eventAggregator;
        private readonly Func<ConnectionsSearchData, SearchConnections> _searchConnectionsFactory;
        private readonly Func<FlyCreationData, AddFly> _addFlyFactory;

        public DateTime DisplayDate { get; private set; }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                SelectedConnection = null;
                _selectedDate = value;

                if (_selectedDate >= DateTime.Now)
                {
                    UpdateConnections();
                }

                NotifyOfPropertyChange(() => CanAddFly);
            }
        }

        public BindableCollection<CONNECTION> Connections { get; set; }
        private CONNECTION _selectedConnection;
        public CONNECTION SelectedConnection
        {
            get { return _selectedConnection; }
            set
            {
                _selectedConnection = value;
                NotifyOfPropertyChange(() => SelectedConnection);
                NotifyOfPropertyChange(() => CanAddFly);
            }
        }

        public bool IsBusy { get; set; }

        public bool CanAddFly
        {
            get
            {
                return SelectedDate >= DateTime.Now &&
                    SelectedConnection != null;
            }
        }

        public NewFlyViewModel(
            MainViewModel mainViewModel,
            IEventAggregator eventAggregator,
            Func<ConnectionsSearchData, SearchConnections> searchConnectionsFactory, 
            Func<FlyCreationData, AddFly> addFlyFactory)
        {
            base.DisplayName = "Add new fly";

            _mainViewModel = mainViewModel;
            _eventAggregator = eventAggregator;
            _searchConnectionsFactory = searchConnectionsFactory;
            _addFlyFactory = addFlyFactory;
            DisplayDate = DateTime.Now;
            SelectedDate = DisplayDate;

            Connections = new BindableCollection<CONNECTION>();
        }

        public void AddFly()
        {
            TryClose();
            var flyCreationData = new FlyCreationData(SelectedDate, SelectedConnection);
            ICommand command = _addFlyFactory(flyCreationData);
            CommandInvoker.InvokeBusy(command, _mainViewModel);
        }

        private void UpdateConnections()
        {
            _eventAggregator.Subscribe(this);
            var connectionsSearchData = new ConnectionsSearchData(
                string.Empty, new DateTime(), new DateTime(), SelectedDate.DayOfWeek.ToString(), string.Empty, string.Empty);
            ICommand command = _searchConnectionsFactory(connectionsSearchData);
            CommandInvoker.InvokeBusy(command, this);
        }

        public void Handle(ConnectionsFounded message)
        {
            Connections.Clear();
            Connections.AddRange(message.Connections);
        }
    }
}
