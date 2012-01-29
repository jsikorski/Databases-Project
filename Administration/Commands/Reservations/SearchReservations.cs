using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Administration.Features.Reservations;
using Administration.Messages;
using Caliburn.Micro;
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
            DBConnection dbConnection = _connectionProvider.GetConnection();
            IQueryable<RESERVATION> reservations = dbConnection.RESERVATION.Where(
                reservation => reservation.SYMBOL.Contains(_reservationsSearchData.Symbol) &&
                               reservation.PLACE_SYMBOL.Contains(_reservationsSearchData.PlaceSymbol) &&
                               reservation.CLIENT_ID == _reservationsSearchData.ClientId && 
                               //reservation.IS_PAID == _reservationsSearchData.IsPaid &&
                               reservation.FLY_SYMBOL.Contains(_reservationsSearchData.FlySymbol));

            _eventAggregator.Publish(new ReservationsFounded(reservations.ToList()));
        }
    }
}
