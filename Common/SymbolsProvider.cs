using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Connection;

namespace Common
{
    public class SymbolsProvider : ISymbolsProvider
    {
        public string GetAirportSymbol(CITY city, COUNTRY country)
        {
            return string.Format("{0}_{1}_{2}", 
                city.NAME.Substring(0, 2), country.NAME.Substring(0, 2), DateTime.Now);
        }

        public string GetConnectionSymbol(AIRPORT @from, AIRPORT to)
        {
            return string.Format("{0}_{1}_{2}",
                @from.NAME.Substring(0, 2), to.NAME.Substring(0, 2), DateTime.Now);
        }
    }
}
