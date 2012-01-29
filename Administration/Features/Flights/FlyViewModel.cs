using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Connection;

namespace Administration.Features.Flights
{
    public class FlyViewModel
    {
        public FLY Fly { get; private set; }

        public string Symbol
        {
            get { return Fly.SYMBOL; }
        }

        public string Date
        {
            get { return Fly.FLY_DATE.ToShortDateString(); }
        }

        public string NumberOfPlaces
        {
            get { return string.Format("{0} / {1}", Fly.FREE_PLACES_NUMBER, Fly.CONNECTION.TICKETS); }
        }

        public string ConnectionSymbol
        {
            get { return Fly.CONNECTION_SYMBOL; }
        }

        public FlyViewModel(FLY fly)
        {
            Fly = fly;
        }
    }
}
