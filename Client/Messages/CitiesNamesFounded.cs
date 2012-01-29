using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Messages
{
    public class CitiesNamesFounded
    {
        public IEnumerable<string> CitiesNames { get; private set; }

        public CitiesNamesFounded(IEnumerable<string> citiesNames)
        {
            CitiesNames = citiesNames;
        }
    }
}
