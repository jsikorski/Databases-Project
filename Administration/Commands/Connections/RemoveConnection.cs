using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Administration.Utils;
using Connection;

namespace Administration.Commands.Connections
{
    public class RemoveConnection : ICommand
    {
        private readonly CONNECTION _connection;
        private readonly IConnectionProvider _connectionProvider;

        public RemoveConnection(
            CONNECTION connection, 
            IConnectionProvider connectionProvider)
        {
            _connection = connection;
            _connectionProvider = connectionProvider;
        }

        public void Execute()
        {
            MessageBoxResult messageBoxResult = MessageBoxService.ShowConfirmationMessage();
            if (messageBoxResult != MessageBoxResult.Yes)
            {
                return;
            }

            DBConnection dbConnection = _connectionProvider.GetConnection();
            var connection = dbConnection.CONNECTION.Single(c => c.SYMBOL == _connection.SYMBOL);
            dbConnection.CONNECTION.DeleteObject(connection);
            dbConnection.SaveChanges();
        }
    }
}
