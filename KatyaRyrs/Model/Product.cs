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
        public int ProductTypeID { get; set; }
        public ProductType CurrentProductType { get; set; }

        public Uri ImagePreview
        {
            get
            {
                var imageName = Environment.CurrentDirectory + (Image ?? "");
                return System.IO.File.Exists(imageName) ? new Uri(imageName) : null;
            }
        }

       
    }
}
