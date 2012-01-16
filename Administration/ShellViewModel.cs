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

namespace Administration
{
    public class ShellViewModel : Screen, IShell, IHandle<LoggedIn>, IBusyScope
    {
        private readonly IContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;
        private readonly MainViewModel _mainViewModel;
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
            IContainer container,
            IEventAggregator eventAggregator,
            IWindowManager windowManager,
            MainViewModel mainViewModel)
        {
            base.DisplayName = "Login";

            _container = container;
            _eventAggregator = eventAggregator;
            _windowManager = windowManager;
            _mainViewModel = mainViewModel;

            _eventAggregator.Subscribe(this);

#if DEBUG
            Username = "SampleAdmin";
            Password = "asd";
#endif
        }

        public IResult Login()
        {
            ICommand command = _container.Resolve<Login>(new NamedParameter("username", Username),
                                                         new NamedParameter("password", Password));
            return new BusyCommandResult(command, this);
        }

        public void Handle(LoggedIn message)
        {
            _windowManager.ShowWindow(_mainViewModel);
            TryClose();
        }
    }
}
