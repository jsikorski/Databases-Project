using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Administration.Messages;
using Caliburn.Micro;
using Connection;

namespace Administration.Commands.Connections
{
    public class SearchConnections : ICommand
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly IEventAggregator _eventAggregator;

        public SearchConnections(
            IConnectionProvider connectionProvider,
            IEventAggregator eventAggregator)
        {
            _connectionProvider = connectionProvider;
            _eventAggregator = eventAggregator;
        }

        public void Execute()
        {
            DBConnection dbConnection = _connectionProvider.GetConnection();
            IEnumerable<CONNECTION> connections = dbConnection.CONNECTION;
            _eventAggregator.Publish(new ConnectionsFounded(connections));   
        }
    }
}
