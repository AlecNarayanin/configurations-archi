using AppliAuth.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AppliAuth.Models.Produits
{

    [DataContract]
    [BsonCollection("CircularProducts")] 
    public class CircularProductConfiguration :  ProductConfiguration
    {

        [DataMember]
        public int Diameter { get; set; }
      
        public override float GetSurface()
        {
            throw new NotImplementedException();
        }
    }
}
