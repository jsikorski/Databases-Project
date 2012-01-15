using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connection
{
    public interface IConnectionProvider
    {
        void Login(string login, string password, string role);
        void Logout();
        DBConnection GetConnection();
    }
}
