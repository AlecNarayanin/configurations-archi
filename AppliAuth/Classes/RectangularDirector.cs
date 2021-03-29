using AppliAuth.Extensions;
using AppliAuth.Interfaces;
using AppliAuth.Models.Produits;
using AppliAuth.Services;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AppliAuth.Classes
{
    public class RectangularDirector : IProductDirector
    {

        private MongoRepository<RectangularProductConfiguration> repository;

        public RectangularDirector(MongoRepository<RectangularProductConfiguration> repository)
        {
            this.repository = repository;

        }

        public void Insert(ProductConfiguration obj)
        {
            var rectObj = (RectangularProductConfiguration)obj;
            repository.InsertOne(rectObj);
        }

        public ProductConfiguration Parse(object obj)
        {
            var str = obj.ToString();
            return JsonConvert.DeserializeObject<RectangularProductConfiguration>(str);

        }

        public IEnumerable<ProductConfiguration> Get(ProductConfiguration calcul)
        {
            var rect = (RectangularProductConfiguration)calcul;

            FilterDefinition<RectangularProductConfiguration> combineFilters = Builders<RectangularProductConfiguration>.Filter.Empty;

            Type t = typeof(RectangularProductConfiguration);
            List<FieldInfo> fields = t.GetAllFields();

            foreach (var field in fields)
            {
                string fieldName = field.Name.Split("<")[1].Split(">")[0];

                if(fieldName != "Id" && fieldName != "ProductId")
                {
                    var data = Convert.ToInt32(field.GetValue(rect));
                    if (data != 0)
                    {
                        combineFilters = combineFilters & Builders<RectangularProductConfiguration>.Filter.Eq(fieldName, data);
                    }
                }
            }
            /*

            if (rect.DB1 != 0)
                combineFilters = combineFilters & Builders<RectangularProductConfiguration>.Filter.Eq(x => x.DB1, rect.DB1);
            if (rect.DB10 != 0)
                combineFilters = combineFilters & Builders<RectangularProductConfiguration>.Filter.Eq(x => x.DB10, rect.DB10);
            if (rect.DB2 != 0)
                combineFilters = combineFilters & Builders<RectangularProductConfiguration>.Filter.Eq(x => x.DB2, rect.DB2);
            if (rect.DB5 != 0)
                combineFilters = combineFilters & Builders<RectangularProductConfiguration>.Filter.Eq(x => x.DB5, rect.DB5);
            if (rect.Depth != 0)
                combineFilters = combineFilters & Builders<RectangularProductConfiguration>.Filter.Eq(x => x.Depth, rect.Depth);

            if (rect.Height != 0)
                combineFilters = combineFilters & Builders<RectangularProductConfiguration>.Filter.Eq(x => x.Height, rect.Height);

            if (rect.Width != 0)
                combineFilters = combineFilters & Builders<RectangularProductConfiguration>.Filter.Eq(x => x.Width, rect.Width);

            if (rect.Thickness != 0)
                combineFilters = combineFilters & Builders<RectangularProductConfiguration>.Filter.Eq(x => x.Thickness, rect.Thickness);*/
          
            return repository._collection.Find(combineFilters).ToList();

        }

    }

}
