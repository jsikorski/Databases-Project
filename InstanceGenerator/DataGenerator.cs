using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly Random _random;

        public DataGenerator()
        {
            var connectionProvider = new ConnectionProvider();
            connectionProvider.Login("SampleAdmin", "asd", "Administrator");
            _dbConnection = connectionProvider.GetConnection();

            _symbolsProvider = new SymbolsProvider();

            _random = new Random();
        }

        public void GenerateData(string citiesFileName)
        {
            Console.WriteLine("Generating data...");

            //GenerateCitiesCountriesAndAirports(citiesFileName);
            //GenerateConnections();
            //GenerateFlights();

            Console.WriteLine("Saving changes...");
            _dbConnection.SaveChanges();

            Console.WriteLine("Data generated.");
        }

        private void GenerateCitiesCountriesAndAirports(string citiesFileName)
        {
            using (var fileStream = new StreamReader(citiesFileName, Encoding.ASCII))
            {
                string file = fileStream.ReadToEnd();
                IEnumerable<string> lines =
                    file.Split('\n').Where(line => !line.Contains("?") && !string.IsNullOrEmpty(line)).ToList();

                Console.WriteLine("Generating cities, countries and airports...");
                foreach (var line in lines)
                {
                    var line1 = line.Replace("\r", "");
                    string[] parts = line1.Split(';');
                    string cityName = parts[2];
                    string countryName = parts[4];

                    Guid countryId = Guid.NewGuid();
                    Guid cityId = Guid.NewGuid();
                    string airportSymbol = _symbolsProvider.GetAirportSymbol();

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
                                                        });
                }
            }
        }

        private void GenerateConnections()
        {
            Console.WriteLine("Generating connections...");

            for (int i = 0; i < 1000; i++)
            {
                string connectionSymbol = _symbolsProvider.GetConnectionSymbol();
                var departureTime = new DateTime(1, 1, 1, _random.Next(0, 24), _random.Next(0, 60),
                                                 _random.Next(0, 60), 0);
                var arivalTime = new DateTime(1, 1, 1, _random.Next(0, 24), _random.Next(0, 60),
                                              _random.Next(0, 60), 0);
                while (departureTime >= arivalTime)
                {
                    departureTime = new DateTime(1, 1, 1, _random.Next(0, 24), _random.Next(0, 60),
                                                 _random.Next(0, 60), 0);
                    arivalTime = new DateTime(1, 1, 1, _random.Next(0, 24), _random.Next(0, 60),
                                                  _random.Next(0, 60), 0);
                }

                string weekDay = GetWeekDay();
                int price = _random.Next(100, 351);
                int numberOfTickets = _random.Next(50, 251);

                AIRPORT toAirport = null;
                AIRPORT fromAirport = toAirport;
                while (toAirport == fromAirport)
                {
                    toAirport = _dbConnection.AIRPORT.ToList()[_random.Next(0, _dbConnection.AIRPORT.Count())];
                    fromAirport = _dbConnection.AIRPORT.ToList()[_random.Next(0, _dbConnection.AIRPORT.Count())];
                }

                _dbConnection.CONNECTION.AddObject(new CONNECTION
                {
                    SYMBOL = connectionSymbol,
                    DEPARTURE_TIME = departureTime,
                    ARIVAL_TIME = arivalTime,
                    WEEKDAY = weekDay,
                    PRICE = price,
                    TICKETS = numberOfTickets,
                    FROM_AIRPORT_SYMBOL = fromAirport.SYMBOL,
                    TO_AIRPORT_SYMBOL = toAirport.SYMBOL
                });
            }
        }

        private void GenerateFlights()
        {
            Console.WriteLine("Generating flights");
            for (int i = 0; i < 10000; i++)
            {
                CONNECTION connection =
                    _dbConnection.CONNECTION.ToList()[_random.Next(0, _dbConnection.CONNECTION.Count())];
                int freePlacesNumber = _random.Next(0, (int)connection.TICKETS);
                DateTime date = GetNearestDayDate(connection.WEEKDAY);

                for (int j = 0; j < 10; j++)
                {
                    string flySymbol = _symbolsProvider.GetFlySymbol();
                    _dbConnection.FLY.AddObject(new FLY
                    {
                        SYMBOL = flySymbol,
                        FLY_DATE = date,
                        FREE_PLACES_NUMBER = freePlacesNumber,
                        CONNECTION_SYMBOL = connection.SYMBOL
                    });
                    date = date.Add(new TimeSpan(7, 0, 0, 0));
                }
            }
        }

        private string GetWeekDay()
        {
            int random = _random.Next(0, 7);
            var days = new[] { "Monday", "Tuesday", "Thursday", "Wednesday", "Saturday", "Sunday", "Friday" };
            return days[random];
        }

        private DateTime GetNearestDayDate(string day)
        {
            DateTime now = DateTime.Now;
            DateTime currentDate = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            while (currentDate.DayOfWeek.ToString() != day)
            {
                currentDate = currentDate.Add(new TimeSpan(1, 0, 0, 0));
            }
            return currentDate;
        }
    }
}
