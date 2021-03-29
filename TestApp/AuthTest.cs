using AppliAuth.Controllers;
using AppliAuth.Models;
using AppliAuth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static AppliAuth.Models.AuthModels;

namespace TestApp
{
    class AuthTest
    {

        [Test]
        public void LoginTestSuccess()
        {


            string password = "test";


            Mock<IHttpContextAccessor> mockAccessor = new Mock<IHttpContextAccessor>();

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Host= new HostString("http://test.test");
            mockAccessor.Setup(x=> x.HttpContext).Returns(httpContext);
           

            Mock<IMongoCollection<User>> mockCollection = new Mock<IMongoCollection<User>>();

            Mock<MongoRepository<User>> mockDbUser = new Mock<MongoRepository<User>>(mockCollection.Object);
            mockDbUser.Setup(x => x.FindOne(It.IsAny<Expression<Func<User, bool>>>())).Returns(new User {Id = new MongoDB.Bson.ObjectId(), Email = "test@test.test", Name = "mock" , Password = BCrypt.Net.BCrypt.HashPassword(password)});

            Mock<MongoDatabaseContext> context = new Mock<MongoDatabaseContext>(new AppliAuth.Models.DatabaseOptions
            {
                ConnectionString = "mongodb+srv://test:test@test.349ur.mongodb.net/Archi?retryWrites=true&w=majority",
                Database = "test"
            });
            context.Setup(x => x.GetUserCollection()).Returns(mockDbUser.Object);

            SecurityService securityService = new SecurityService(mockAccessor.Object, new AppliAuth.Models.AppSettings { Secret = "Ceciestuntestjerepertececiestuntest"  });
            UserService u = new UserService(context.Object, securityService);

            AuthController controller = new AuthController(u);
            var response = controller.Login(new LoginModel { Password = password  , Username = "test@test.test" });
            

            Assert.IsAssignableFrom<OkObjectResult>(response);
            var okResult = (OkObjectResult)response;
            var body = (LoginResult)okResult.Value;
            Assert.IsTrue(body.success);
            Assert.IsNotNull(body.token);

        }

        [Test]
        public void LoginTestBadPassword()
        {


            string password = "test";


            Mock<IHttpContextAccessor> mockAccessor = new Mock<IHttpContextAccessor>();

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Host = new HostString("http://test.test");
            mockAccessor.Setup(x => x.HttpContext).Returns(httpContext);


            Mock<IMongoCollection<User>> mockCollection = new Mock<IMongoCollection<User>>();

            Mock<MongoRepository<User>> mockDbUser = new Mock<MongoRepository<User>>(mockCollection.Object);
            mockDbUser.Setup(x => x.FindOne(It.IsAny<Expression<Func<User, bool>>>())).Returns(new User { Id = new MongoDB.Bson.ObjectId(), Email = "test@test.test", Name = "mock", Password = BCrypt.Net.BCrypt.HashPassword(password) });

            Mock<MongoDatabaseContext> context = new Mock<MongoDatabaseContext>(new AppliAuth.Models.DatabaseOptions
            {
                ConnectionString = "mongodb+srv://test:test@test.349ur.mongodb.net/Archi?retryWrites=true&w=majority",
                Database = "test"
            });
            context.Setup(x => x.GetUserCollection()).Returns(mockDbUser.Object);

            SecurityService securityService = new SecurityService(mockAccessor.Object, new AppliAuth.Models.AppSettings { Secret = "Ceciestuntestjerepertececiestuntest" });
            UserService u = new UserService(context.Object, securityService);

            AuthController controller = new AuthController(u);
            var response = controller.Login(new LoginModel { Password = "wrongpassword", Username = "test@test.test" });


            Assert.IsAssignableFrom<UnauthorizedObjectResult>(response);
            var okResult = (UnauthorizedObjectResult)response;
            var body = (ApiResult)okResult.Value;
            Assert.IsFalse(body.success);

        }

        [Test]
        public void LoginTestUserUnknow()
        {

            string password = "test";
            Mock<IHttpContextAccessor> mockAccessor = new Mock<IHttpContextAccessor>();

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Host = new HostString("http://test.test");
            mockAccessor.Setup(x => x.HttpContext).Returns(httpContext);


            Mock<IMongoCollection<User>> mockCollection = new Mock<IMongoCollection<User>>();

            Mock<MongoRepository<User>> mockDbUser = new Mock<MongoRepository<User>>(mockCollection.Object);
            mockDbUser.Setup(x => x.FindOne(It.IsAny<Expression<Func<User, bool>>>())).Returns((User)null);

            Mock<MongoDatabaseContext> context = new Mock<MongoDatabaseContext>(new AppliAuth.Models.DatabaseOptions
            {
                ConnectionString = "mongodb+srv://test:test@test.349ur.mongodb.net/Archi?retryWrites=true&w=majority",
                Database = "test"
            });
            context.Setup(x => x.GetUserCollection()).Returns(mockDbUser.Object);

            SecurityService securityService = new SecurityService(mockAccessor.Object, new AppliAuth.Models.AppSettings { Secret = "Ceciestuntestjerepertececiestuntest" });
            UserService u = new UserService(context.Object, securityService);

            AuthController controller = new AuthController(u);
            var response = controller.Login(new LoginModel { Password = password, Username = "test@test.test" });


            Assert.IsAssignableFrom<UnauthorizedObjectResult>(response);
            var okResult = (UnauthorizedObjectResult)response;
            var body = (ApiResult)okResult.Value;
            Assert.IsFalse(body.success);

        }
    
    
    
    }
}
