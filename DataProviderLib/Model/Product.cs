using KatyaRyrs.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatyaRyrs.Model
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Weight { get; set; }
    
        public string Image { get; set; }
        public decimal Price { get; set; }
     //   public int ProductTypeID { get; set; }
        public ProductType CurrentProductType { get; set; }

        public Uri ImagePreview
        {
            get
            {
                var imageName = Environment.CurrentDirectory + (Image ?? "");
                return System.IO.File.Exists(imageName) ? new Uri(imageName) : null;
            }
        }

        public void Save()
        {
            if(Name == "")
            {
                throw new Exception("Имя продукта не заполнено");
            }

            if(CurrentProductType == null)
            {
                throw new Exception("Тип продукта не заполнен");
            }

            if(Price <= 0)
            {
                throw new Exception("Цена продукта не может быть меньше или равна нулю");
            }

            if(Weight == "")
            {
                throw new Exception("Вес продукта не заполнен");
            }

            if(Number <= 0)
            {
                throw new Exception("Номер продукта не может быть меньше или равен нулю");
            }

        }

        public void Delete()
        {
            if(ID == 0)
            {
                throw new Exception("Нельзя удалить несуществующий продукт");
            }
        }

        

       
    }
}
