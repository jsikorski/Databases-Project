using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Administration.Utils;
using Connection;

namespace Administration.Commands.Flights
{
    public class RemoveFly : ICommand
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly FLY _flyToRemove;

        public RemoveFly(
            IConnectionProvider connectionProvider, 
            FLY flyToRemove)
        {
            _connectionProvider = connectionProvider;
            _flyToRemove = flyToRemove;
        }

        public void Execute()
        {
            if (MessageBoxService.ShowConfirmationMessage() != MessageBoxResult.Yes)
            {
                return;
            }

            DBConnection dbConnection = _connectionProvider.GetConnection();
            FLY flyToRemove = dbConnection.FLY.Single(fly => fly.SYMBOL == _flyToRemove.SYMBOL);
            dbConnection.FLY.DeleteObject(flyToRemove);
            dbConnection.SaveChanges();
        }
    }
}
