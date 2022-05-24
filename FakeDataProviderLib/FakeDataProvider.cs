using KatyaRyrs.Class;
using KatyaRyrs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeDataProviderLib
{
    public class FakeDataProvider : IDataProvider
    {
        Product[] ProductArray = new Product[]
        {
            new Product{ID=133, Name="Парацитомол", Number=123, Weight="100г", Image="Фото", Price=124, CurrentProductType = ProductTypeArray[0]},
            new Product{ID=233, Name="Пентальгин", Number=1123, Weight="50г", Image="Фоточка", Price=128,  CurrentProductType = ProductTypeArray[1]},
            new Product{ID=333, Name="Анальгин", Number=17474, Weight="50г", Image="Фоточки", Price=129,  CurrentProductType = ProductTypeArray[2]}
        };
        static ProductType[] ProductTypeArray = new ProductType[]
        {
            new ProductType{ID=111, Title = "Обезболивающее"},
            new ProductType{ID=21, Title = "Болеутоляющее"},
            new ProductType{ID=111, Title = "Противовирусное"}
        };
        public void DeleteProduct(Product DelProduct)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProduct()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductType> GetProductTypes()
        {
            throw new NotImplementedException();
        }

        public void SaveProduct(Product ChangedProduct)
        {
            throw new NotImplementedException();
        }
    }
}
