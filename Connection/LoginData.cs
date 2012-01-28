using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connection
{
    public class LoginData
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }

        public LoginData(string userName, string password)
        {
            Password = password;
            UserName = userName;
        }
    }
}
