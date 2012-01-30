using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Client.Commands.Flights;
using Common.Infrastucture;
using Connection;

namespace Client.Features.Flights
{
    public class FlyDetailsViewModel : Screen
    {
        private readonly IBusyScope _mainViewModel;
        private readonly FLY _connectedFly;
        private readonly Func<FLY, BookTicket> _bookTicketFactory;

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

        public bool CanBookTicket
        {
            get { return _connectedFly.FREE_PLACES_NUMBER > 0; }
        }

        public FlyDetailsViewModel(
            MainViewModel mainViewModel,
            FLY connectedFly,
            Func<FLY, BookTicket> bookTicketFactory)
        {
            base.DisplayName = "Fly details";

            _mainViewModel = mainViewModel;
            _connectedFly = connectedFly;
            _bookTicketFactory = bookTicketFactory;
        }

        public void BookTicket()
        {
            TryClose();
            ICommand command = _bookTicketFactory(_connectedFly);
            CommandInvoker.InvokeBusy(command, _mainViewModel);
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
