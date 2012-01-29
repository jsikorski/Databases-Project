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
            return GetSymbol("AIRPORT");
        }

        public string GetConnectionSymbol()
        {
            return GetSymbol("CONNECTION");
        }

        public string GetFlySymbol()
        {
            return GetSymbol("FLY");
        }

        private string GetSymbol(string objectType)
        {
            return objectType + "_" + Guid.NewGuid().ToString();
        }
    }
}
