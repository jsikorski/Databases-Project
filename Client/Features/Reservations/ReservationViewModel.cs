using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Connection;

namespace Client.Features.Reservations
{
    public class ReservationViewModel : Screen
    {
        public ReservationViewModel(RESERVATION reservation)
        {
            Reservation = reservation;
        }

        public RESERVATION Reservation { get; private set; }

        public string Symbol
        {
            get { return Reservation.SYMBOL; }
        }

        public string PlaceSymbol
        {
            get { return Reservation.PLACE_SYMBOL; }
        }

        public string Date
        {
            get { return Reservation.FLY == null ? null : Reservation.FLY.FLY_DATE.ToShortDateString(); }
        }

        public bool IsPaid
        {
            get
            {
                return Convert.ToBoolean(Reservation.IS_PAID);
            }
            set
            {
                Reservation.IS_PAID = value ? (short)1 : (short)0;
                NotifyOfPropertyChange(() => IsPaid);
            }
        }

        public string FlySymbol
        {
            get { return Reservation.FLY_SYMBOL; }
        }
    }
}
