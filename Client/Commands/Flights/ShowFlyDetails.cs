using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Client.Features.Flights;
using Common.Infrastucture;
using Connection;

namespace Client.Commands.Flights
{
    public class ShowFlyDetails : ICommand
    {
        private readonly IWindowManager _windowManager;
        private readonly FLY _selectedFly;
        private readonly Func<FLY, FlyDetailsViewModel> _flyDetailsViewModelFactory;
        private readonly IConnectionProvider _connectionProvider;

        public ShowFlyDetails(
            IWindowManager windowManager,
            FLY selectedFly,
            Func<FLY, FlyDetailsViewModel> flyDetailsViewModelFactory,
            IConnectionProvider connectionProvider)
        {
            _windowManager = windowManager;
            _selectedFly = selectedFly;
            _flyDetailsViewModelFactory = flyDetailsViewModelFactory;
            _connectionProvider = connectionProvider;
        }

        public void Execute()
        {
            DBConnection dbConnection = _connectionProvider.GetConnection();
            FLY fly = dbConnection.FLY.Single(f => f.SYMBOL == _selectedFly.SYMBOL);
            FlyDetailsViewModel flyDetailsViewModel = _flyDetailsViewModelFactory(fly);
            _windowManager.ShowDialog(flyDetailsViewModel);
        }
    }
}
