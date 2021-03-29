using AppliAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AppliAuth.Models.AuthModels;

namespace AppliAuth.Extensions
{
    public static class RegisterModelExtension
    {
        public static User ToUser(this RegisterModel model)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var role = model.Role;

            if(!Roles.ToList().Exists(role => role == model.Role))
            {
                role = "Utilisateur";
            }

             
            return new User { Authorized = model.Authorized , 
                             Email = model.Email, 
                              Name = model.Name , 
                              Societe = model.Societe, 
                              Password = passwordHash,
                              Role = role
                             };
        }
    }
}
