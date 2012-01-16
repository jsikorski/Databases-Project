using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class SymbolsProvider : ISymbolsProvider
    {
        public string GetAirportSymbol()
        {
            return string.Format("{0}_{1}", "AIRPORT", DateTime.Now);
        }

        public string GetConnectionSymbol()
        {
            return string.Format("{0}_{1}", "CONNECTION", DateTime.Now);
        }
    }
}
