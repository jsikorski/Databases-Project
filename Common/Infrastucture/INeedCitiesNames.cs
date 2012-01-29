using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;

namespace Common.Infrastucture
{
    public interface INeedCitiesNames
    {
        BindableCollection<string> CitiesNames { get; set; }
        void SetCitiesNames(IEnumerable<string> citiesNames);
    }
}
