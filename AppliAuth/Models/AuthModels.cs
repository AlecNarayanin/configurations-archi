using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppliAuth.Models
{
    public class AuthModels
    {

        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }


        public class LoginResult : ApiResult
        {
            public string token { get; set; }

        }

        public class RegisterModel
        {
            public bool Authorized { get; set; }

            public string Password { get; set; }

            public string Name { get; set; }

            public string Email { get; set; }

            public string Societe { get; set; }

            public string Role { get; set; }
        }
    }
}
