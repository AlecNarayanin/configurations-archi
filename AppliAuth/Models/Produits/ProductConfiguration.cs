using AppliAuth.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Runtime.Serialization;

namespace AppliAuth.Models.Produits
{
    [DataContract]
    public abstract class ProductConfiguration : IMongoDocument
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }


        public MongoDBRef ProductId { get; set; }

        [DataMember]
        public int Depth { get; set; }

        [DataMember]
        public float DB1 { get; set; }

        [DataMember]
        public float DB2 { get; set; }

        [DataMember]
        public float DB5 { get; set; }

        [DataMember]
        public float DB10  { get; set; }


        public abstract float GetSurface();




    }
}
