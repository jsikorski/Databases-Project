using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Administration.Features.Airports
{
    public class AirportCreationData
    {
        public string AirportName { get; private set; }
        public string CityName { get; private set; }
        public string CountryName { get; private set; }

        public AirportCreationData(string airportName, string cityName, string countryName)
        {
            AirportName = airportName;
            CityName = cityName;
            CountryName = countryName;
        }
    }
}
