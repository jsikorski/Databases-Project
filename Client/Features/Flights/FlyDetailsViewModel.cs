using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Connection;

namespace Client.Features.Flights
{
    public class FlyDetailsViewModel
    {
        private readonly FLY _connectedFly;

        public string From
        {
            get { return GetAirportInfo(_connectedFly.CONNECTION.AIRPORT); }
        }

        public string To
        {
            get { return GetAirportInfo(_connectedFly.CONNECTION.AIRPORT1); }
        }

        public string Date
        {
            get { return _connectedFly.FLY_DATE.ToShortDateString(); }
        }

        public string DepartureTime
        {
            get { return _connectedFly.CONNECTION.DEPARTURE_TIME.ToShortTimeString(); }
        }

        public string ArivalTime
        {
            get { return _connectedFly.CONNECTION.ARIVAL_TIME.ToShortTimeString(); }
        }

        public decimal Price
        {
            get { return _connectedFly.CONNECTION.PRICE; }
        }

        public string NumberOfTickets
        {
            get { return string.Format("{0} / {1}", _connectedFly.FREE_PLACES_NUMBER, _connectedFly.CONNECTION.TICKETS); }
        }

        public FlyDetailsViewModel(FLY connectedFly)
        {
            _connectedFly = connectedFly;
        }

        private string GetAirportInfo(AIRPORT airport)
        {
            return string.Format(
                    "{0} / {1} / {2}",
                    airport.NAME,
                    airport.CITY.NAME,
                    airport.CITY.COUNTRY.NAME);
        }
    }
}
