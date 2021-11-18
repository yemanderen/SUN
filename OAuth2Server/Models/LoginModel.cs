using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CDBWebSecurity.Models
{
    public class LoginModel
    {
        /// <summary>
        /// account
        /// </summary>
        [Required(ErrorMessage = "please input account")]
        public string Account { get; set; }

        /// <summary>
        /// password
        /// </summary>
        [Required(ErrorMessage = "please input password")]
        public string Password { get; set; }
    }
}
