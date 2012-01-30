using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Administration.Features.Reservations;
using Administration.Messages;
using Caliburn.Micro;
using Common.Infrastucture;
using Common.Messages;
using Connection;

namespace Administration.Commands.Reservations
{
    public class SearchReservations : ICommand
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly ReservationsSearchData _reservationsSearchData;
        private readonly IEventAggregator _eventAggregator;

        public SearchReservations(
            IConnectionProvider connectionProvider,
            ReservationsSearchData reservationsSearchData, 
            IEventAggregator eventAggregator)
        {
            _connectionProvider = connectionProvider;
            _reservationsSearchData = reservationsSearchData;
            _eventAggregator = eventAggregator;
        }

        public void Execute()
        {
            short isPaidOracle = Convert.ToInt16(_reservationsSearchData.IsPaid);

            DBConnection dbConnection = _connectionProvider.GetConnection();
            IQueryable<RESERVATION> reservations = dbConnection.RESERVATION.Where(
                reservation => reservation.SYMBOL.Contains(_reservationsSearchData.Symbol) &&
                               reservation.PLACE_SYMBOL.Contains(_reservationsSearchData.PlaceSymbol) &&
                               reservation.IS_PAID == isPaidOracle &&
                               (reservation.FLY_SYMBOL == null || reservation.FLY_SYMBOL.Contains(_reservationsSearchData.FlySymbol)));

            if (_reservationsSearchData.ClientId != null)
            {
                reservations =
                    reservations.Where(reservation => reservation.CLIENT_ID == _reservationsSearchData.ClientId);
            }

            _eventAggregator.Publish(new ReservationsFounded(reservations.ToList()));
        }
    }
}
