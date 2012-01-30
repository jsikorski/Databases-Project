using System.Collections.Generic;
using Connection;

namespace Common.Messages
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
