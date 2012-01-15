using System;
using System.Data.EntityClient;
using Connection.Exceptions;

namespace Connection
{
    public class ConnectionProvider : IConnectionProvider
    {
        private readonly string _connectionStringFormat;

        private string _login;
        private string _password;
        private string _role;

        public ConnectionProvider()
        {
            var entityConnectionStringBuilder =
                new EntityConnectionStringBuilder
                    {
                        Provider = "Oracle.DataAccess.Client",
                        ProviderConnectionString =
                            "DATA SOURCE=localhost:1521/xe;PASSWORD={1};PERSIST SECURITY INFO=True;USER ID={0}",
                        Metadata =
                            "res://*/Model.csdl|res://*/Model.ssdl|res://*/Model.msl"
                    };

            _connectionStringFormat = entityConnectionStringBuilder.ToString();
        }

        public void Login(string login, string password, string role)
        {
            try
            {
                var dbConnection = GetDbConnection(login, password);
                dbConnection.ExecuteStoreCommand(string.Format("SET ROLE {0}", role));
            }
            catch (Exception)
            {
                throw new AuthenticationException();
            }

            _login = login;
            _password = password;
            _role = role;
        }

        public void Logout()
        {
            _login = null;
            _password = null;
            _role = null;
        }

        public DBConnection GetConnection()
        {
            if (_login == null)
            {
                throw new NotLoggedException();
            }

            try
            {
                return GetDbConnection(_login, _password);
            }
            catch (Exception)
            {
                throw new ConnectionException();
            }
        }

        private DBConnection GetDbConnection(string login, string password)
        {
            return new DBConnection(string.Format(_connectionStringFormat, login, password));
        }
    }
}
