using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Administration.Features.Flights;
using Caliburn.Micro;

namespace Administration.Commands.Flights
{
    public class NewFly : ICommand
    {
        private readonly IWindowManager _windowManager;
        private readonly NewFlyViewModel _newFlyViewModel;

        public NewFly(
            IWindowManager windowManager, 
            NewFlyViewModel newFlyViewModel)
        {
            _windowManager = windowManager;
            _newFlyViewModel = newFlyViewModel;
        }

        public void Execute()
        {
            _windowManager.ShowDialog(_newFlyViewModel);
        }
    }
}
