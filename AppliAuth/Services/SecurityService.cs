using AppliAuth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AppliAuth.Services
{
    public class SecurityService
    {
        private readonly IHttpContextAccessor Context;
        private SymmetricSecurityKey SIGNING_KEY;

        public SecurityService(IHttpContextAccessor httpContextAccessor , AppSettings appSettings)
        {
            Context = httpContextAccessor;
            SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Secret));
        }

       
        public JwtSecurityToken GenerateToken(string userId, string email)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("userId",userId),
                        new Claim("userEmail",email)
                }),
                Issuer = Context.HttpContext.Request.Host.Value,
                Audience = Context.HttpContext.Request.Host.Value,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(SIGNING_KEY,
                                                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = (JwtSecurityToken)tokenHandler.CreateToken(tokenDescriptor);
            return token;
        }

        public string WriteToken(JwtSecurityToken token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public bool ValidateToken(string tokenString)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidation = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidAudience = Context.HttpContext.Request.Host.Value,
                ValidIssuer = Context.HttpContext.Request.Host.Value,
                IssuerSigningKey = SIGNING_KEY,
            };

            try
            {
                SecurityToken token = null;
                var principal = tokenHandler.ValidateToken(tokenString, tokenValidation, out token);




                return principal.HasClaim(x => x.Type == "userId") && principal.HasClaim(x => x.Type == "userEmail");
                
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public string GetUserFromToken(string tokenString)
        {
            return GetClaimValueFromToken(tokenString, "userId");
        }


        private string GetClaimValueFromToken(string tokenString, string claimName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidation = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = Context.HttpContext.Request.Host.Value,
                ValidIssuer = Context.HttpContext.Request.Host.Value,
                IssuerSigningKey = SIGNING_KEY,
            };

            try
            {
                // Try to extract the user principal from the token
                SecurityToken token = null;
                var principal = tokenHandler.ValidateToken(tokenString, tokenValidation, out token);
                return principal.FindFirst(claimName).Value;
            }
            catch (Exception e)
            {
                return String.Empty;
            }
        }

    }
}
