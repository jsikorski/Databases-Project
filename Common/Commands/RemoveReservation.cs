using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Common.Infrastucture;
using Common.Messages;
using Common.Utils;
using Connection;

namespace Common.Commands
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
            if (MessageBoxService.ShowConfirmationMessage() != MessageBoxResult.Yes)
            {
                return;
            }

            DBConnection dbConnection = _connectionProvider.GetConnection();
            
            RESERVATION reservationToRemove = dbConnection.RESERVATION
                .Single(reservation => reservation.SYMBOL == _reservationToRemove.SYMBOL);
            reservationToRemove.FLY.FREE_PLACES_NUMBER++;
            dbConnection.RESERVATION.DeleteObject(reservationToRemove);
            dbConnection.SaveChanges();

            _eventAggregator.Publish(new ReservationRemoved());
        }
    }
}
