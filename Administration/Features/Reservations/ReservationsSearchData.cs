using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Administration.Features.Reservations
{
    public class ReservationsSearchData
    {
        public string Symbol { get; set; }
        public string PlaceSymbol { get; set; }
        public int? ClientId { get; set; }
        public bool IsPaid { get; set; }
        public string FlySymbol { get; set; }
    }
}
