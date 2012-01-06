using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace InstanceGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var userInformationGatherer = new UserInformationGatherer();
            string username = userInformationGatherer.GetUsername();
            string password = userInformationGatherer.GetUsername();

            string connectionString = string.Format(
                "Driver={{Microsoft ODBC for Oracle}};" +
                "Server=localhost: 1521/xe;" +
                "Uid={0};" +
                "Pwd={1};",
                username,
                password);

            var databaseConnector = new DatabaseConnector(connectionString);
            var scriptLoader = new ScriptLoader("some.sql");
            var databaseGenerator = new DatabaseGenerator(databaseConnector, scriptLoader);

            databaseGenerator.Generate();
            Console.ReadKey();
        }
    }
}
