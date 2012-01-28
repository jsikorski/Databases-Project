using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.EntityClient;
using System.Data.Odbc;
using System.Net;
using System.Threading;
using Administration.Commands;
using Administration.Features;
using Administration.Infrastucture;
using Administration.Messages;
using Autofac;
using Caliburn.Micro;
using Connection;

namespace Administration
{
    public class ShellViewModel : Screen, IShell, IHandle<LoggedIn>, IBusyScope
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;
        private readonly MainViewModel _mainViewModel;
        private readonly Func<LoginData, Login> _loginFactory;

        public string Username { get; set; }
        public string Password { get; set; }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                NotifyOfPropertyChange(() => IsBusy);
            }
        }

        public ShellViewModel(
            IEventAggregator eventAggregator,
            IWindowManager windowManager,
            MainViewModel mainViewModel, 
            Func<LoginData, Login> loginFactory)
        {
            base.DisplayName = "Login";

            _eventAggregator = eventAggregator;
            _windowManager = windowManager;
            _mainViewModel = mainViewModel;
            _loginFactory = loginFactory;

            _eventAggregator.Subscribe(this);

            #if DEBUG
                Username = "SampleAdmin";
                Password = "asd";
            #endif
        }

        public void Login()
        {
            ICommand command = _loginFactory(new LoginData(Username, Password));
            CommandInvoker.InvokeBusy(command, this);
        }

        public void Handle(LoggedIn message)
        {
            _windowManager.ShowWindow(_mainViewModel);
            TryClose();
        }
    }
}
