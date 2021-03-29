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
    [BsonCollection("RectProducts")]
    public class RectangularProductConfiguration :   ProductConfiguration
    {
        
        [DataMember]
        public int Width { get; set; }
        [DataMember]
        public int Height { get; set; }
        [DataMember]
        public int Thickness { get; set; }
      


        public override float GetSurface()
        {
            throw new NotImplementedException();
        }
    }
}
