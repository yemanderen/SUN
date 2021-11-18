using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPatternSample
{
    public class UserService
    {
        private readonly UserRepository _userRepository =new UserRepository();
        public event LoginSucessDelegate LoginSucessEvent;
        public bool Login(string userName, string password)
        {
            var user = _userRepository.Get(userName, password);
            if (user != null)
            {
                var message = new LoginMessageModel
                {
                    UserID = user.UserID,
                    Username = user.Username,
                    LoginIP = user.LoginIP
                };

                Task.Run(() =>
                {
                    LoginSucessEvent?.Invoke(message);
                });

                return true;
            }

            return false;
        }
    }

    public delegate void LoginSucessDelegate(LoginMessageModel message);
}
