﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public interface ISymbolsProvider
    {
        string GetAirportSymbol();
        string GetConnectionSymbol();
        string GetFlySymbol();
        string GetReservationSymbol();
    }
}
