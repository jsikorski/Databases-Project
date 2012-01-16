using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Administration.Features.Airports
{
    public class AirportsSearchData
    {
        public string AirportName { get; private set; }
        public string CityName { get; private set; }
        public string CountryName { get; private set; }

        public AirportsSearchData(string airportName, string cityName, string countryName)
        {
            CountryName = countryName;
            CityName = cityName;
            AirportName = airportName;
        }
    }
}
