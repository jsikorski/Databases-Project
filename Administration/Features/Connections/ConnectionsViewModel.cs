using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Administration.Commands;
using Administration.Commands.Connections;
using Administration.Infrastucture;
using Administration.Messages;
using Administration.Utils;
using Autofac;
using Caliburn.Micro;
using Connection;

namespace Administration.Features.Connections
{
    public class ConnectionsViewModel : IBusyScopeSubscreen, IHandle<ConnectionsFounded>
    {
        private readonly IContainer _container;
        private readonly Func<CONNECTION, RemoveConnection> _removeConnectionFactory;
        private IBusyScope _busyScope;

        public BindableCollection<CONNECTION> Connections { get; set; }
        public CONNECTION SelectedConnection { get; set; }

        public ConnectionsViewModel(
            IEventAggregator eventAggregator,
            IContainer container,
            Func<CONNECTION, RemoveConnection> removeConnectionFactory)
        {
            _container = container;
            _removeConnectionFactory = removeConnectionFactory;
            eventAggregator.Subscribe(this);
            Connections = new BindableCollection<CONNECTION>();
        }

        public void NewConnection()
        {
            ICommand command = _container.Resolve<NewConnection>();
            CommandInvoker.Execute(command);
        }

        public IResult SearchConnections()
        {
            ICommand command = _container.Resolve<SearchConnections>();
            return new BusyCommandResult(command, _busyScope);
        }

        public void RemoveConnection()
        {
            ICommand command = _removeConnectionFactory(SelectedConnection);
            CommandInvoker.Invoke(command);
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
    }
}
