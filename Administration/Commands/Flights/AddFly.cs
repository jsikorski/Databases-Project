using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Administration.Features.Flights;
using Common;
using Connection;

namespace Administration.Commands.Flights
{
    public class AddFly : ICommand
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly FlyCreationData _flyCreationData;
        private readonly ISymbolsProvider _symbolsProvider;

        public AddFly(
            IConnectionProvider connectionProvider, 
            FlyCreationData flyCreationData, 
            ISymbolsProvider symbolsProvider)
        {
            _connectionProvider = connectionProvider;
            _flyCreationData = flyCreationData;
            _symbolsProvider = symbolsProvider;
        }

        public void Execute()
        {
            DBConnection dbConnection = _connectionProvider.GetConnection();
            
            var fly = new FLY();
            fly.SYMBOL = _symbolsProvider.GetFlySymbol();
            fly.FLY_DATE = _flyCreationData.Date;
            fly.FREE_PLACES_NUMBER = _flyCreationData.RelatedConnection.TICKETS;
            fly.CONNECTION_SYMBOL = _flyCreationData.RelatedConnection.SYMBOL;
            
            dbConnection.FLY.AddObject(fly);
            dbConnection.SaveChanges();
        }
    }
}
