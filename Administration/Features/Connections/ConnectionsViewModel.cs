using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Administration.Commands;
using Administration.Commands.Connections;
using Administration.Messages;
using Autofac;
using Caliburn.Micro;
using Common.Infrastucture;
using Connection;

namespace Administration.Features.Connections
{
    public class ConnectionsViewModel : Screen, IBusyScopeSubscreen, IHandle<ConnectionsFounded>, IHandle<ConnectionRemoved>
    {
        private readonly IContainer _container;
        private readonly Func<ConnectionsSearchData, SearchConnections> _searchConnectionsFactory;
        private readonly Func<CONNECTION, RemoveConnection> _removeConnectionFactory;
        private IBusyScope _busyScope;

        public BindableCollection<CONNECTION> Connections { get; set; }
        private CONNECTION _selectedConnection;
        public CONNECTION SelectedConnection
        {
            get { return _selectedConnection; }
            set
            {
                _selectedConnection = value;
                NotifyOfPropertyChange(() => CanRemoveConnection);
            }
        }

        public string ConnectionSymbol { get; set; }
        public string DepartureTime { get; set; }
        public string ArivalTime { get; set; }
        public IEnumerable<string> Days { get; private set; }
        public string SelectedDay { get; set; }
        public string FromAirportName { get; set; }
        public string ToAirportName { get; set; }

        public bool CanRemoveConnection
        {
            get { return SelectedConnection != null; }
        }

        public ConnectionsViewModel(
            IEventAggregator eventAggregator,
            IContainer container,
            Func<ConnectionsSearchData, SearchConnections> searchConnectionsFactory, 
            Func<CONNECTION, RemoveConnection> removeConnectionFactory)
        {
            _container = container;
            _searchConnectionsFactory = searchConnectionsFactory;
            _removeConnectionFactory = removeConnectionFactory;
            eventAggregator.Subscribe(this);
            Connections = new BindableCollection<CONNECTION>();

            Days = new[] { "Monday", "Tuesday", "Thursday", "Wednesday", "Saturday", "Sunday", "Friday" };
        }

        public void NewConnection()
        {
            ICommand command = _container.Resolve<NewConnection>();
            CommandInvoker.Execute(command);
        }

        public void SearchConnections()
        {
            var connectionsSearchData = new ConnectionsSearchData(ConnectionSymbol, 
                Convert.ToDateTime(DepartureTime), Convert.ToDateTime(ArivalTime), 
                SelectedDay, FromAirportName, ToAirportName);
            ICommand command = _searchConnectionsFactory(connectionsSearchData);
            CommandInvoker.InvokeBusy(command, _busyScope);
        }

        public void RemoveConnection()
        {
            ICommand command = _removeConnectionFactory(SelectedConnection);
            CommandInvoker.Execute(command);
        }

        public void SetBusyScope(IBusyScope busyScope)
        {
            _busyScope = busyScope;
        }

        public void Handle(ConnectionsFounded message)
        {
            Connections.Clear();
            Connections.AddRange(message.Connections);
        }

        public void Handle(ConnectionRemoved message)
        {
            Connections.Remove(SelectedConnection);
        }
    }
}
