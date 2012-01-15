using System.Data;
using System.Data.Odbc;

namespace InstanceGenerator
{
    internal class DatabaseConnector : IDatabaseConnector
    {
        private readonly string _connectionString;

        public IDbConnection Connection { get; private set; }

        public DatabaseConnector(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Connect()
        {
            Connection = new OdbcConnection(_connectionString);
            Connection.Open();
        }

        public void CloseConnection()
        {
            Connection.Close();
        }
    }
}
