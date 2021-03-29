using AppliAuth.Interfaces;
using AppliAuth.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppliAuth.Models.Produits
{

    [BsonCollection("Products")]
    public class Product : IMongoDocument
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }


        public string Name { get; set; }

        public float BasePrice { get; set; }


        


    }
}
