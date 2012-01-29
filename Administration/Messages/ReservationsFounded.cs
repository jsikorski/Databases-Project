using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Connection;

namespace Administration.Messages
{
    public class ReservationsFounded
    {
        public IEnumerable<RESERVATION> Reservations { get; private set; }

        public ReservationsFounded(IEnumerable<RESERVATION> reservations)
        {
            Reservations = reservations;
        }
    }
}
