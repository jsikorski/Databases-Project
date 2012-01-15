using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;

namespace InstanceGenerator
{
    internal class DatabaseGenerator
    {
        private readonly IDatabaseConnector _dbConnector;
        private readonly ScriptLoader _scriptLoader;

        public DatabaseGenerator(IDatabaseConnector dbConnector, ScriptLoader scriptLoader)
        {
            _dbConnector = dbConnector;
            _scriptLoader = scriptLoader;
        }

        public void Generate()
        {
            try
            {
                _dbConnector.Connect();
            }
            catch (Exception exception)
            {
                Console.WriteLine("\nCannot connect to database server.");
                Console.WriteLine("Exception: " + exception.Message);
                return;
            }

            Console.WriteLine("\nBegining generating database.\n");

            IEnumerable<string> commands = _scriptLoader.GetCommands();
            foreach (var command in commands)
            {
                Console.WriteLine("Executing command: {0}", command);

                try
                {
                    var dbCommand = new OdbcCommand(command, _dbConnector.Connection as OdbcConnection);
                    dbCommand.ExecuteNonQuery();
                    Console.WriteLine("Success");
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Cannot execute command: {0}", command);
                    Console.WriteLine("\nDatabase generating failure.");
                    Console.WriteLine("Exception: " + exception.Message);
                    return;
                }
            }

            _dbConnector.CloseConnection();
            Console.WriteLine("\nDatabase was successfully generated.");
        }
    }
}
