using AppliAuth.Extensions;
using AppliAuth.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AppliAuth.Models.AuthModels;

namespace AppliAuth.Services
{
    public class UserService
    {

        private readonly MongoRepository<User> Repository;
        private readonly SecurityService Security;

        public UserService(MongoDatabaseContext mongoDatabaseContext, SecurityService securityService)
        {
            Repository = mongoDatabaseContext.GetUserCollection();
            Security = securityService;
        }


        public User GetById(string id)
        {

            var userId = ObjectId.Parse(id);
            var user = Repository.FindOne(user => user.Id == userId);
            user.Password = null;
            return user;
        }

        public virtual ApiResult Login(LoginModel model)
        {
            var currentUser = Repository.FindOne(user => user.Email.Equals(model.Username));
            if (currentUser == null)
            {
                return new ApiResult { success = false, message = "User unknown" };
            }
            else
            {

                var jwt = Security.GenerateToken(currentUser.Id.ToString(), currentUser.Email);

                bool verified = BCrypt.Net.BCrypt.Verify(model.Password, currentUser.Password);

                if (verified)
                {
                    return new LoginResult { success = true, token = Security.WriteToken(jwt) };
                }
                else
                {
                    return new ApiResult { success = false, message = "Bad password" };
                }
            }

        }

        public virtual void Register(RegisterModel model)
        {
            Repository.InsertOne(model.ToUser());
        }
    }
}
