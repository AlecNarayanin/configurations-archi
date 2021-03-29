using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppliAuth.Models
{
    public static class Roles
    {

        public const string ADMIN = "Admin";
        public const string USER = "Utilisateur";


        public static IEnumerable<string> ToEnumerable()
        {
            yield return ADMIN;
            yield return USER;
        }

        public static List<string> ToList()
        {
            return new List<string> { ADMIN, USER };
        }


    }
}
