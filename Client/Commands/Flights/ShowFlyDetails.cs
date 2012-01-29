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

        public ShowFlyDetails(
            IWindowManager windowManager,
            FLY selectedFly,
            Func<FLY, FlyDetailsViewModel> flyDetailsViewModelFactory)
        {
            _windowManager = windowManager;
            _selectedFly = selectedFly;
            _flyDetailsViewModelFactory = flyDetailsViewModelFactory;
        }

        public void Execute()
        {
            FlyDetailsViewModel flyDetailsViewModel = _flyDetailsViewModelFactory(_selectedFly);
            _windowManager.ShowDialog(flyDetailsViewModel);
        }
    }
}
