using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Client.Messages;
using Common.Infrastucture;
using Connection;

namespace Client.Commands.Cities
{
    public class GetCitiesNames : ICommand
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly IEventAggregator _eventAggregator;

        public GetCitiesNames(
            IConnectionProvider connectionProvider, 
            IEventAggregator eventAggregator)
        {
            _connectionProvider = connectionProvider;
            _eventAggregator = eventAggregator;
        }

        public void Execute()
        {
            DBConnection dbConnection = _connectionProvider.GetConnection();
            IQueryable<string> citiesNames = dbConnection.CITY.Select(city => city.NAME);
            _eventAggregator.Publish(new CitiesNamesFounded(citiesNames.ToList()));
        }
    }
}
