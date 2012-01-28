using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Administration.Features.Connections;
using Administration.Messages;
using Caliburn.Micro;
using Connection;

namespace Administration.Commands.Connections
{
    public class SearchConnections : ICommand
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly IEventAggregator _eventAggregator;
        private readonly ConnectionsSearchData _connectionsSearchData;

        public SearchConnections(
            IConnectionProvider connectionProvider,
            IEventAggregator eventAggregator,
            ConnectionsSearchData connectionsSearchData)
        {
            _connectionProvider = connectionProvider;
            _eventAggregator = eventAggregator;
            _connectionsSearchData = connectionsSearchData;
        }

        public void Execute()
        {
            DBConnection dbConnection = _connectionProvider.GetConnection();

            IQueryable<CONNECTION> connections = dbConnection.CONNECTION.Where(
                connection => connection.SYMBOL.Contains(_connectionsSearchData.Symbol));

            if (!string.IsNullOrEmpty(_connectionsSearchData.WeekDay))
            {
                connections = connections.Where(
                    connection => connection.WEEKDAY == _connectionsSearchData.WeekDay);
            }

            var defaultDateTime = new DateTime();
            if (_connectionsSearchData.DepartureTime != defaultDateTime)
            {
                connections = connections.Where(
                    connection => connection.DEPARTURE_TIME == _connectionsSearchData.DepartureTime);
            }
            if (_connectionsSearchData.ArivalTime != defaultDateTime)
            {
                connections = connections.Where(
                    connection => connection.ARIVAL_TIME == _connectionsSearchData.ArivalTime);
            }

            AIRPORT fromAirport = dbConnection.AIRPORT.FirstOrDefault(
                airport => airport.NAME == _connectionsSearchData.FromAirportName);
            if (fromAirport != null)
            {
                connections = connections.Where(connection => connection.FROM_AIRPORT_SYMBOL == fromAirport.SYMBOL);
            }

            AIRPORT toAirport = dbConnection.AIRPORT.FirstOrDefault(
                airport => airport.NAME == _connectionsSearchData.ToAirportName);
            if (toAirport != null)
            {
                connections = connections.Where(connection => connection.TO_AIRPORT_SYMBOL == toAirport.SYMBOL);
            }
            
            _eventAggregator.Publish(new ConnectionsFounded(connections.ToList()));
        }
    }
}
