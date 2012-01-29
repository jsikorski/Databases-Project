using System;
using Caliburn.Micro;
using Client.Commands;
using Client.Features;
using Client.Features.Flights;
using Common.Infrastucture;
using Connection;

namespace Client
{
    public class ShellViewModel : Screen, IShell, IHandle<LoggedIn>, IBusyScope
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;
        private readonly MainViewModel _mainViewModel;
        private readonly Func<LoginData, bool, Login> _loginFactory;

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

        private bool _loginAsGuest;
        public bool LoginAsGuest
        {
            get { return _loginAsGuest; }
            set
            {
                _loginAsGuest = value;
                NotifyOfPropertyChange(() => LoginNotAsGuest);
            }
        }

        public bool LoginNotAsGuest
        {
            get { return !LoginAsGuest; }
        }

        public ShellViewModel(
            IEventAggregator eventAggregator,
            IWindowManager windowManager,
            MainViewModel mainViewModel,
            Func<LoginData, bool, Login> loginFactory)
        {
            base.DisplayName = "Login";

            _eventAggregator = eventAggregator;
            _windowManager = windowManager;
            _mainViewModel = mainViewModel;
            _loginFactory = loginFactory;

            _eventAggregator.Subscribe(this);

            #if DEBUG
                Username = "SampleClient";
                Password = "asd";
            #endif
        }

        public void Login()
        {
            ICommand command = _loginFactory(new LoginData(Username, Password), LoginAsGuest);
            CommandInvoker.InvokeBusy(command, this);
        }

        public void Handle(LoggedIn message)
        {
            _windowManager.ShowWindow(_mainViewModel);
            TryClose();
        }
    }
}
