using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Common.Infrastucture;
using Common.Messages;
using Connection;

namespace Client.Commands.Reservations
{
    public class GetReservationsForUser : ICommand
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly IEventAggregator _eventAggregator;

        public GetReservationsForUser(
            IConnectionProvider connectionProvider, 
            IEventAggregator eventAggregator)
        {
            _connectionProvider = connectionProvider;
            _eventAggregator = eventAggregator;
        }

        public void Execute()
        {
            DBConnection dbConnection = _connectionProvider.GetConnection();

            const string query = "SELECT USER_ID FROM USER_USERS";
            decimal userId = dbConnection.ExecuteStoreQuery<decimal>(query).First();

            IQueryable<RESERVATION> reservations =
                dbConnection.RESERVATION.Where(reservation => reservation.CLIENT_ID == userId);
            _eventAggregator.Publish(new ReservationsFounded(reservations.ToList()));
        }
    }
}
