using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Connection;

namespace Client.Messages
{
    public class ReservationPaid
    {
        public ReservationPaid(RESERVATION reservation)
        {
            Reservation = reservation;
        }

        public RESERVATION Reservation { get; private set; }
    }
}
