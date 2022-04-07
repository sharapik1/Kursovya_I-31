using KatyaRyrs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatyaRyrs.Class
{
    public interface IDataProvider

    {
        IEnumerable<Product> GetProduct();
        IEnumerable<ProductType> GetProductTypes();
        void SaveProduct(Product ChangedProduct);
    }
}
