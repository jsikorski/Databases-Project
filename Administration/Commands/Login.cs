using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Administration.Messages;
using Caliburn.Micro;
using Common.Infrastucture;
using Connection;

namespace Administration.Commands
{
    public class Login : ICommand
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly IEventAggregator _eventAggregator;
        private readonly LoginData _connectionData;

        public Login(
            IConnectionProvider connectionProvider,
            IEventAggregator eventAggregator,
            LoginData connectionData)
        {
            _connectionProvider = connectionProvider;
            _eventAggregator = eventAggregator;
            _connectionData = connectionData;
        }

        public void Execute()
        {
            _connectionProvider.Login(_connectionData.UserName,
                                      _connectionData.Password,
                                      "Administrator");
            _eventAggregator.Publish(new LoggedIn());
        }
    }
}
