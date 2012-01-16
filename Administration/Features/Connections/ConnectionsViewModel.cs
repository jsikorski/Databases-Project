﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Administration.Commands;
using Administration.Commands.Connections;
using Administration.Infrastucture;
using Administration.Messages;
using Autofac;
using Caliburn.Micro;
using Connection;

namespace Administration.Features.Connections
{
    public class ConnectionsViewModel : IBusyScopeSubscreen, IHandle<ConnectionsFounded>
    {
        private readonly IContainer _container;
        private IBusyScope _busyScope;

        public BindableCollection<CONNECTION> Connections { get; set; }

        public ConnectionsViewModel(
            IEventAggregator eventAggregator,
            IContainer container)
        {
            _container = container;
            eventAggregator.Subscribe(this);
            Connections = new BindableCollection<CONNECTION>();
        }

        public void NewConnection()
        {
            ICommand command = _container.Resolve<NewConnection>();
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
    }
}