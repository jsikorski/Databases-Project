using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Connection;

namespace Common
{
    public interface ISymbolsProvider
    {
        string GetAirportSymbol(CITY city, COUNTRY country);
    }
}
