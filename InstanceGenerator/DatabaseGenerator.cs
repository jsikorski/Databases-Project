using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;

namespace InstanceGenerator
{
    public class DatabaseGenerator
    {
        private readonly DatabaseConnector _dbConnector;
        private readonly ScriptLoader _scriptLoader;

        public DatabaseGenerator(DatabaseConnector dbConnector, ScriptLoader scriptLoader)
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
            catch
            {
                Console.WriteLine("\nCannot connect to database server.");
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
                catch
                {
                    Console.WriteLine("Cannot execute command: {0}", command);
                    Console.WriteLine("\nDatabase generating failure.");
                    return;
                }
            }

            _dbConnector.CloseConnection();
            Console.WriteLine("\nDatabase was successfully generated.");
        }
    }
}
