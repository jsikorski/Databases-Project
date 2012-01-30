using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using Common;
using Common.Infrastucture;
using Common.Utils;
using Connection;

namespace Client.Commands.Flights
{
    public class BookTicket : ICommand
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly ISymbolsProvider _symbolsProvider;
        private readonly FLY _fly;

        public BookTicket(
            IConnectionProvider connectionProvider, 
            ISymbolsProvider symbolsProvider,
            FLY fly)
        {
            _connectionProvider = connectionProvider;
            _symbolsProvider = symbolsProvider;
            _fly = fly;
        }

        public void Execute()
        {
            if (MessageBoxService.ShowConfirmationMessage() != MessageBoxResult.Yes)
            {
                return;
            }

            DBConnection dbConnection = _connectionProvider.GetConnection();
            FLY fly = dbConnection.FLY.Single(f => f.SYMBOL == _fly.SYMBOL);
            fly.FREE_PLACES_NUMBER--;

            const string query = "SELECT USER_ID FROM USER_USERS";
            ObjectResult<decimal> userId = dbConnection.ExecuteStoreQuery<decimal>(query);

            var reservation = new RESERVATION();
            reservation.SYMBOL = _symbolsProvider.GetReservationSymbol();
            reservation.PLACE_SYMBOL = (fly.CONNECTION.TICKETS - fly.FREE_PLACES_NUMBER).ToString(CultureInfo.InvariantCulture);
            reservation.IS_PAID = 0;
            reservation.FLY_SYMBOL = fly.SYMBOL;
            reservation.CLIENT_ID = userId.First();
            dbConnection.RESERVATION.AddObject(reservation);

            dbConnection.SaveChanges();
        }
    }
}
