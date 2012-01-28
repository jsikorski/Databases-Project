using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Administration.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsToLower(this string str, string value)
        {
            return str.ToLowerInvariant().Contains(value.ToLowerInvariant());
        }
    }
}
