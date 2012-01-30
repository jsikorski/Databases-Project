using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstanceGenerator
{
    public interface IDataGenerator
    {
        void GenerateData(string citiesFileName);
    }
}
