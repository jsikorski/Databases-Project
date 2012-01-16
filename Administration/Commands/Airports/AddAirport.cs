using System.Diagnostics;
using System.Linq;
using Administration.Features.Airports;
using Common;
using Connection;

namespace Administration.Commands.Airports
{
    public class AddAirport : ICommand
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly AirportCreationData _airportCreationData;
        private readonly ISymbolsProvider _symbolsProvider;

        public AddAirport(
            IConnectionProvider connectionProvider,
            AirportCreationData airportCreationData,
            ISymbolsProvider symbolsProvider)
        {
            _connectionProvider = connectionProvider;
            _airportCreationData = airportCreationData;
            _symbolsProvider = symbolsProvider;
        }

        public void Execute()
        {
            DBConnection dbConnection = _connectionProvider.GetConnection();

            COUNTRY country = dbConnection.COUNTRY.FirstOrDefault(c => c.NAME == _airportCreationData.CountryName);
            if (country == null)
            {
                country = new COUNTRY
                {
                    ID = dbConnection.COUNTRY.Max(c => c.ID) + 1,
                    NAME = _airportCreationData.CountryName
                };
                dbConnection.COUNTRY.AddObject(country);
            }

            CITY city = dbConnection.CITY.FirstOrDefault(c => c.NAME == _airportCreationData.CityName);
            if (city == null)
            {
                city = new CITY
                           {
                               ID = dbConnection.CITY.Max(c => c.ID) + 1,
                               NAME = _airportCreationData.AirportName,
                               COUNTRY_ID = country.ID,                               
                           };
                dbConnection.CITY.AddObject(city);
            }

            Debug.Assert(city != null && country != null);

            var airport = new AIRPORT
                              {
                                  SYMBOL = _symbolsProvider.GetAirportSymbol(),
                                  NAME = _airportCreationData.AirportName,
                                  CITY_ID = city.ID
                              };
            
            dbConnection.AIRPORT.AddObject(airport);
            dbConnection.SaveChanges();
        }
    }
}
