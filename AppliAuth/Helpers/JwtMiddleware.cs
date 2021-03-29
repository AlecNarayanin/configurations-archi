using AppliAuth.Models;
using AppliAuth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppliAuth.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, SecurityService userService, UserService UserService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachUserToContext(context, userService, UserService , token);

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, SecurityService security,  UserService UserService , string token)
        {
            try
            {
                if (security.ValidateToken(token))
                {
                   var userId = security.GetUserFromToken(token);
                   context.Items["User"] = UserService.GetById(userId);
                }

               
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}

