using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Connection;

namespace Administration.Features.Connections
{
    public class NewConnectionViewModel : Screen
    {
        public DateTime DepartureTime { get; set; }
        public DateTime ArivalTime { get; set; }
        
        public IEnumerable<string> Days { get; set; }
        public string SelectedDay { get; set; }

        public int Price { get; set; }
        public int NumberOfTickets { get; set; }

        public IEnumerable<AIRPORT> Airports { get; set; }
        public AIRPORT SelectedFromAirport { get; set; }
        public AIRPORT SelectedToAirport { get; set; }

        public NewConnectionViewModel()
        {
            base.DisplayName = "Add connection";
        }

        public IResult AddConnection()
        {
            return null;
        }
    }
}
