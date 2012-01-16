﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Administration.Infrastucture;
using Administration.Messages;
using Caliburn.Micro;
using Connection;

namespace Administration.Commands
{
    public class Login : ICommand
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly IEventAggregator _eventAggregator;
        private readonly string _username;
        private readonly string _password;

        public Login(
            IConnectionProvider connectionProvider,
            IEventAggregator eventAggregator,
            string username, 
            string password)
        {
            _connectionProvider = connectionProvider;
            _eventAggregator = eventAggregator;
            _username = username;
            _password = password;
        }

        public void Execute()
        {
            _connectionProvider.Login(_username, _password, "Administrator");
            _eventAggregator.Publish(new LoggedIn());
        }
    }
}
