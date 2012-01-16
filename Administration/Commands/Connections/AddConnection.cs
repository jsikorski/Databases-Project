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
            var connection = new CONNECTION()
                                 {
                                     SYMBOL =
                                         _symbolsProvider.GetConnectionSymbol(_connectionCreationData.From,
                                                                              _connectionCreationData.To),
                                     DEPARTURE_TIME = DateTime.Parse(_connectionCreationData.DepartureTime),
                                     ARIVAL_TIME = DateTime.Parse(_connectionCreationData.ArivalTime),
                                     WEEKDAY = _connectionCreationData.WeekDay,
                                     PRICE = Convert.ToInt32(_connectionCreationData.Price.Trim('_')),
                                     TICKETS = Convert.ToInt32(_connectionCreationData.NumberOfTickets.Trim('_')),
                                     FROM_AIRPORT_SYMBOL = _connectionCreationData.From.SYMBOL,
                                     TO_AIRPORT_SYMBOL = _connectionCreationData.To.SYMBOL
                                 };
            dbConnection.CONNECTION.AddObject(connection);
            dbConnection.SaveChanges();
        }
    }
}
