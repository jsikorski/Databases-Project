using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Administration.Features.Connections;
using Common;
using Connection;

namespace Administration.Commands.Connections
{
    public class AddConnection : ICommand
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly ISymbolsProvider _symbolsProvider;
        private readonly ConnectionCreationData _connectionCreationData;

        public AddConnection(
            IConnectionProvider connectionProvider,
            ISymbolsProvider symbolsProvider,
            ConnectionCreationData connectionCreationData)
        {
            _connectionProvider = connectionProvider;
            _symbolsProvider = symbolsProvider;
            _connectionCreationData = connectionCreationData;
        }

        public void Execute()
        {
            DBConnection dbConnection = _connectionProvider.GetConnection();
            var connection = new CONNECTION();
            connection.SYMBOL = _symbolsProvider.GetConnectionSymbol();
            connection.DEPARTURE_TIME = DateTime.Parse(_connectionCreationData.DepartureTime);
            connection.ARIVAL_TIME = DateTime.Parse(_connectionCreationData.ArivalTime);
            connection.WEEKDAY = _connectionCreationData.WeekDay;
            connection.PRICE = Convert.ToInt32(_connectionCreationData.Price.Trim('_'));
            connection.TICKETS = Convert.ToInt32(_connectionCreationData.NumberOfTickets.Trim('_'));
            connection.FROM_AIRPORT_SYMBOL = _connectionCreationData.From.SYMBOL;
            connection.TO_AIRPORT_SYMBOL = _connectionCreationData.To.SYMBOL;
            dbConnection.CONNECTION.AddObject(connection);
            dbConnection.SaveChanges();
        }
    }
}
