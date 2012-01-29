using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Administration.Messages;
using Caliburn.Micro;
using Connection;

namespace Administration.Commands.Reservations
{
    public class RemoveReservation : ICommand
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly RESERVATION _reservationToRemove;
        private readonly IEventAggregator _eventAggregator;

        public RemoveReservation(
            IConnectionProvider connectionProvider,
            RESERVATION reservationToRemove, 
            IEventAggregator eventAggregator)
        {
            _connectionProvider = connectionProvider;
            _reservationToRemove = reservationToRemove;
            _eventAggregator = eventAggregator;
        }

        public void Execute()
        {
            DBConnection dbConnection = _connectionProvider.GetConnection();
            RESERVATION reservationToRemove = dbConnection.RESERVATION
                .Single(reservation => reservation.SYMBOL == _reservationToRemove.SYMBOL);
            dbConnection.RESERVATION.DeleteObject(reservationToRemove);
            dbConnection.SaveChanges();

            _eventAggregator.Publish(new ReservationRemoved());
        }
    }
}
