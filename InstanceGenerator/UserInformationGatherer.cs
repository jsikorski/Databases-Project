using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstanceGenerator
{
    public class UserInformationGatherer
    {
        public string GetUsername()
        {
            Console.WriteLine("Username:");
            return Console.ReadLine();
        }

        public string GetPassword()
        {
            Console.WriteLine("Password:");
            return Console.ReadLine();
        }
    }
}
