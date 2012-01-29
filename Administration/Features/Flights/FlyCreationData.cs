using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Connection;

namespace Administration.Features.Flights
{
    public class FlyCreationData
    {
        public DateTime Date { get; private set; }
        public CONNECTION RelatedConnection { get; private set; }

        public FlyCreationData(DateTime date, CONNECTION relatedConnection)
        {
            Date = date;
            RelatedConnection = relatedConnection;
        }
    }
}
