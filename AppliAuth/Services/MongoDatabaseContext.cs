using AppliAuth.Extensions;
using AppliAuth.Models;
using AppliAuth.Models.Produits;
using AppliAuth.Services;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppliAuth.Services
{
    public class MongoDatabaseContext
    {

        private readonly IMongoDatabase mongoClient;



        public MongoDatabaseContext(DatabaseOptions settings)
        {
            mongoClient = new MongoClient(settings.ConnectionString).GetDatabase(settings.Database);
        }


        public IMongoCollection<Object>  GetCollection(string collectionName)
        {
            return mongoClient.GetCollection<Object>(collectionName);

        }

       
        public virtual MongoRepository<User> GetUserCollection()
        {
            IMongoCollection<User> collection = mongoClient.GetCollection<User>(typeof(User).GetCollectionName());
            return new MongoRepository<User>(collection);

        }


        public MongoRepository<CircularProductConfiguration> GetCircularCollection()
        {
            IMongoCollection<CircularProductConfiguration> collection = mongoClient.GetCollection<CircularProductConfiguration>(typeof(CircularProductConfiguration).GetCollectionName());
            return new MongoRepository<CircularProductConfiguration>(collection);

        }

        public MongoRepository<RectangularProductConfiguration> GetRectangularCollection()
        {
            IMongoCollection<RectangularProductConfiguration> collection = mongoClient.GetCollection<RectangularProductConfiguration>(typeof(RectangularProductConfiguration).GetCollectionName());
            return new MongoRepository<RectangularProductConfiguration>(collection);

        }


    }



}
