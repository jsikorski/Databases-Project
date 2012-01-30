using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Client.Messages;
using Common.Infrastucture;
using Connection;

namespace Client.Commands.Reservations
{
    public class PayForReservation : ICommand
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly RESERVATION _selectedReservation;
        private readonly IEventAggregator _eventAggregator;

        public PayForReservation(
            IConnectionProvider connectionProvider,
            RESERVATION selectedReservation, 
            IEventAggregator eventAggregator)
        {
            _connectionProvider = connectionProvider;
            _selectedReservation = selectedReservation;
            _eventAggregator = eventAggregator;
        }

        public void Execute()
        {
            DBConnection dbConnection = _connectionProvider.GetConnection();
            RESERVATION reservationToPayFor =
                dbConnection.RESERVATION.Single(reservation => reservation.SYMBOL == _selectedReservation.SYMBOL);
            reservationToPayFor.IS_PAID = 1;
            dbConnection.SaveChanges();

            _eventAggregator.Publish(new ReservationPaid(_selectedReservation));
        }
    }
}
