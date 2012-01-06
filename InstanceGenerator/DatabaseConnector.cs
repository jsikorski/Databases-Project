using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;

namespace InstanceGenerator
{
    public class DatabaseConnector
    {
        public IDbConnection Connection { get; private set; }
        private readonly string _connectionString;

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
