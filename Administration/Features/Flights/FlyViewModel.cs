using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Connection;

namespace Administration.Features.Flights
{
    public class FlyViewModel
    {
        private readonly FLY _fly;

        public string Symbol
        {
            get { return _fly.SYMBOL; }
        }

        public string Date
        {
            get { return _fly.FLY_DATE.ToShortDateString(); }
        }

        public string NumberOfPlaces
        {
            get { return string.Format("{0} / {1}", _fly.FREE_PLACES_NUMBER, _fly.CONNECTION.TICKETS); }
        }

        public string ConnectionSymbol
        {
            get { return _fly.CONNECTION_SYMBOL; }
        }

        public FlyViewModel(FLY fly)
        {
            _fly = fly;
        }
    }
}
