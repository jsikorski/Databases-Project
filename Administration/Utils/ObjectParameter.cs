using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;

namespace Administration.Utils
{
    public class ObjectParameter : TypedParameter
    {
        public ObjectParameter(object parameter)
            : base(parameter.GetType(), parameter)
        {

        }
    }
}
