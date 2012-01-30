using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Common;
using Connection;

namespace InstanceGenerator
{
    public class DataGenerator : IDataGenerator
    {
        private readonly DBConnection _dbConnection;
        private readonly ISymbolsProvider _symbolsProvider;

        public DataGenerator()
        {
            var connectionProvider = new ConnectionProvider();
            connectionProvider.Login("SampleAdmin", "asd", "Administrator");
            _dbConnection = connectionProvider.GetConnection();

            _symbolsProvider = new SymbolsProvider();
        }

        public void GenerateData(string citiesFileName)
        {
            Console.WriteLine("Generating data...");

            using (var fileStream = new StreamReader(citiesFileName, Encoding.ASCII))
            {
                string file = fileStream.ReadToEnd();
                IEnumerable<string> lines = file.Split('\n').Where(line => !line.Contains("?") && !string.IsNullOrEmpty(line)).ToList();

                foreach (var line in lines)
                {
                    var line1 = line.Replace("\r", "");
                    string[] parts = line1.Split(';');
                    string cityName = parts[2];
                    string countryName = parts[4];

                    Guid countryId = Guid.NewGuid();
                    Guid cityId = Guid.NewGuid();
                    string airportSymbol = _symbolsProvider.GetAirportSymbol();
                    string connectionSymbol = _symbolsProvider.GetConnectionSymbol();

                    _dbConnection.COUNTRY.AddObject(new COUNTRY
                                                        {
                                                            ID = countryId, 
                                                            NAME = countryName
                                                        });
                    _dbConnection.CITY.AddObject(new CITY
                                                     {
                                                         ID = cityId, 
                                                         NAME = cityName,
                                                         COUNTRY_ID = countryId 
                                                     });
                    _dbConnection.AIRPORT.AddObject(new AIRPORT
                                                        {
                                                            SYMBOL = airportSymbol, 
                                                            NAME = "Airport in " + cityName,
                                                            CITY_ID = cityId
                                                        } );
                    }
                _dbConnection.SaveChanges();
            }

            Console.WriteLine("Data generated.");
        }
    }
}
