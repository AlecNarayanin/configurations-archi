using AppliAuth.Interfaces;
using AppliAuth.Models.Produits;
using AppliAuth.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppliAuth.Classes
{
    public class Director
    {

        private MongoDatabaseContext databaseContext;

        public Director(MongoDatabaseContext mongoDatabaseContext)
        {
            databaseContext = mongoDatabaseContext;

        }


        public IProductDirector GetDirector(string directorType)
        {
            var type = directorType.ToUpper();

            if(type == "CIRCULAIRE")
            {
                return new CircularDirector(databaseContext.GetCircularCollection());
            }
            else if (type == "RECTANGULAIRE")
            {
                return new RectangularDirector(databaseContext.GetRectangularCollection());
            }
            else
            {
                throw new NotImplementedException("type non implementé");
            }

        }

    }
}
