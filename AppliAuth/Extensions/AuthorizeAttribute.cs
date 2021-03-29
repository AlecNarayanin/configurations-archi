using AppliAuth.Models;
using AppliAuth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppliAuth.Extensions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]

    public class AuthorizeAttribute : Attribute, IAuthorizationFilter {

        private string Role;

        public AuthorizeAttribute(string role = null)
        {
            Role = role;
        }
        

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User)context.HttpContext.Items["User"];

            if (Role == null)
            {
                if (user == null)
                    context.Result = new JsonResult(new { message = "Pas de token" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else
            {

                if(user==null || (Role != user.Role && user.Role != Roles.ADMIN))
                {
                    context.Result = new JsonResult(new { message = "Vous n'avez pas le bon role pour l'action" }) { StatusCode = StatusCodes.Status403Forbidden };
                }
            }
           
        

        }


     } 
}

