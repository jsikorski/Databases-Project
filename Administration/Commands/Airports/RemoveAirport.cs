using System.Linq;
using System.Windows;
using Administration.Messages;
using Administration.Utils;
using Caliburn.Micro;
using Connection;

namespace Administration.Commands.Airports
{
    public class RemoveAirport : ICommand
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly AIRPORT _airport;
        private readonly IEventAggregator _eventAggregator;

        public RemoveAirport(
            IConnectionProvider connectionProvider,
            AIRPORT airport, 
            IEventAggregator eventAggregator)
        {
            _connectionProvider = connectionProvider;
            _airport = airport;
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
            var airport = dbConnection.AIRPORT.Single(a => a.SYMBOL == _airport.SYMBOL);
            dbConnection.AIRPORT.DeleteObject(airport);
            dbConnection.SaveChanges();

            _eventAggregator.Publish(new AirportRemoved());
        }
    }
}
