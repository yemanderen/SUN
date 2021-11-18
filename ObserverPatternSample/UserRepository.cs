using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPatternSample
{
    public class UserRepository
    {
        public LoginMessageModel Get(string userName, string password)
        {
            if (userName == "admin" && password == "123456")
            {
                return new LoginMessageModel
                {
                    UserID = 2021998,
                    Username = "admin",
                    LoginIP = "127.0.0.1"
                };
            }
            return null;
        }

    }
}
