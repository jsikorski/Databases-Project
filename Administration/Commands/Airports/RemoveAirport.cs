﻿using System.Linq;
using System.Windows;
using Administration.Messages;
using Caliburn.Micro;
using Common.Infrastucture;
using Common.Utils;
using Connection;

namespace Administration.Commands.Airports
{
    public class RemoveAirport : ICommand
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly AIRPORT _airportToRemove;
        private readonly IEventAggregator _eventAggregator;

        public RemoveAirport(
            IConnectionProvider connectionProvider,
            AIRPORT airportToRemove, 
            IEventAggregator eventAggregator)
        {
            _connectionProvider = connectionProvider;
            _airportToRemove = airportToRemove;
            _eventAggregator = eventAggregator;
        }

        public void Execute()
        {
            MessageBoxResult result = MessageBoxService.ShowConfirmationMessage();
            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            DBConnection dbConnection = _connectionProvider.GetConnection();

            var airport = dbConnection.AIRPORT.Single(a => a.SYMBOL == _airportToRemove.SYMBOL);
            dbConnection.AIRPORT.DeleteObject(airport);
            dbConnection.SaveChanges();

            _eventAggregator.Publish(new AirportRemoved());
        }
    }
}
