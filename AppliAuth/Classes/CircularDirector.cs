using AppliAuth.Extensions;
using AppliAuth.Interfaces;
using AppliAuth.Models.Produits;
using AppliAuth.Services;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace AppliAuth.Classes
{
    public class CircularDirector : IProductDirector
    {

        private MongoRepository<CircularProductConfiguration> repository;

        public CircularDirector(MongoRepository<CircularProductConfiguration> repository)
        {
            this.repository = repository;

        }

        public void Insert(ProductConfiguration obj)
        {
            var circularObj = (CircularProductConfiguration)obj;
            repository.InsertOne(circularObj);
        }

        public ProductConfiguration Parse(object obj)
        {
            var str = obj.ToString();
            return JsonConvert.DeserializeObject<CircularProductConfiguration>(str);

        }

        public IEnumerable<ProductConfiguration> Get(ProductConfiguration calcul)
        {
            var circ = (CircularProductConfiguration)calcul;

            FilterDefinition<CircularProductConfiguration> combineFilters= Builders<CircularProductConfiguration>.Filter.Empty;

            Type t = typeof(CircularProductConfiguration);
            List<FieldInfo> fields = t.GetAllFields();

            foreach (var field in fields)
            {
                string fieldName = field.Name.Split("<")[1].Split(">")[0];

                if (fieldName != "Id" && fieldName != "ProductId")
                {
                    var data = Convert.ToInt32(field.GetValue(circ));
                    if (data != 0)
                    {
                        combineFilters = combineFilters & Builders<CircularProductConfiguration>.Filter.Eq(fieldName, data);
                    }
                }
            }

            return   repository._collection.Find(combineFilters).ToList();

        }



    }
}
