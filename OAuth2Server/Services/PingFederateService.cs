using CDBWebSecurity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDBWebSecurity.Services
{
    public class PingFederateService
    {
        public static List<LoginModel> GetUserList()
        {
            List<LoginModel> listUser = new List<LoginModel>();
            listUser.Add(new LoginModel() { Account = "JackV123456", Password = "jack" });
            listUser.Add(new LoginModel() { Account = "WyhA265658", Password = "wyh" });
            return listUser;
        }

        public static bool ValidateUser(LoginModel user)
        {
            List<LoginModel> listUser = GetUserList();
            if (listUser.Exists(o=>o.Account == user.Account && o.Password == user.Password))
                return true;
            else
                return false;
        }
    }
}
