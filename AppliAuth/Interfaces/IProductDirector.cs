using AppliAuth.Models.Produits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AppliAuth.Interfaces
{
    public interface IProductDirector
    {



        public void Insert(ProductConfiguration obj);

        public ProductConfiguration Parse(object obj);

        public IEnumerable<ProductConfiguration> Get(ProductConfiguration calcul);



    }
}
