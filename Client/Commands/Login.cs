using Caliburn.Micro;
using Common.Infrastucture;
using Common.Messages;
using Connection;

namespace Client.Commands
{
    public class Login : ICommand
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly IEventAggregator _eventAggregator;
        private readonly LoginData _connectionData;
        private readonly bool _loginAsGuest;

        public Login(
            IConnectionProvider connectionProvider,
            IEventAggregator eventAggregator,
            LoginData connectionData,
            bool loginAsGuest)
        {
            _connectionProvider = connectionProvider;
            _eventAggregator = eventAggregator;
            _connectionData = connectionData;
            _loginAsGuest = loginAsGuest;
        }

        public void Execute()
        {
            string userName = _connectionData.UserName;
            string password = _connectionData.Password;
            string role = "Client";
             
            if (_loginAsGuest)
            {
                userName = "GlobalUser";
                password = "asd";
                role = "NotLoggedClient";
            }

            _connectionProvider.Login(userName, password, role);
            _eventAggregator.Publish(new LoggedIn());
        }
    }
}
