using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Administration.Features.Connections;
using Caliburn.Micro;

namespace Administration.Commands.Connections
{
    public class NewConnection : ICommand
    {
        private readonly IWindowManager _windowManager;
        private readonly NewConnectionViewModel _newConnectionViewModel;

        public NewConnection(
            IWindowManager windowManager, 
            NewConnectionViewModel newConnectionViewModel)
        {
            _windowManager = windowManager;
            _newConnectionViewModel = newConnectionViewModel;
        }

        public void Execute()
        {
            _windowManager.ShowDialog(_newConnectionViewModel);
        }
    }
}
