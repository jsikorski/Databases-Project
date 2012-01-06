using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Text;

namespace InstanceGenerator
{
    public class ScriptLoader
    {
        private readonly string _scriptText;

        public ScriptLoader(string fileName)
        {
            var script = new FileInfo("some.sql");
            _scriptText = script.OpenText().ReadToEnd()
                .Replace("\n", "").Replace("\r", "");
        }

        public IEnumerable<string> GetCommands()
        {
            return _scriptText.Split(';').Where(command => !string.IsNullOrEmpty(command));
        }
    }
}
