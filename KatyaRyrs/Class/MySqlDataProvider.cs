using KatyaRyrs.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KatyaRyrs.Class
{
    public class MySqlDataProvider : IDataProvider
    {
        private MySqlConnection Connection = new MySqlConnection();
        public MySqlDataProvider()
        {
            try
            {
                Connection = new MySqlConnection("Server=home.kolei.ru;Database=esharapova;port=3306;UserId=esharapova;password=030104");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        public IEnumerable<Product> GetProduct()
        {
            GetProductTypes();
            List<Product> Listproducts = new List<Product>();
            string Sql = @"SELECT 
                      p.*,
                    pt.`Name`
                    FROM Kt_Product p
                    LEFT JOIN 
                    Kt_ProductType pt ON p.Category = pt.ID";
            try
            {

                Connection.Open();
                try
                {
                    MySqlCommand Command = new MySqlCommand(Sql, Connection);
                    MySqlDataReader Reader = Command.ExecuteReader();
                    while (Reader.Read())
                    {
                        Product NewProduct = new Product();
                        NewProduct.ID = Reader.GetInt32("ID");
                        NewProduct.Name = Reader.GetString("Name");
                        NewProduct.Number = Reader.GetInt32("Number");
                        NewProduct.Weight = Reader.GetString("Weight");

                        NewProduct.Image = Reader["Image"].ToString();
                        NewProduct.Price = Reader.GetDecimal("Price");
                        NewProduct.CurrentProductType = GetProductType(Reader.GetInt32("Category"));
                        NewProduct.CurrentProductType = GetProductType(Reader.GetInt32("ProductTypeID"));
                        Listproducts.Add(NewProduct);
                    }
                }
                finally
                {
                    Connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return Listproducts;
        }
        private List<ProductType> ProductTypes = null;
        private ProductType GetProductType(int Id)
        {

            return ProductTypes.Find(pt => pt.ID == Id);
        }
        public IEnumerable<ProductType> GetProductTypes()
        {
            if (ProductTypes == null)
            {
                ProductTypes = new List<ProductType>();
                string Query = "SELECT * FROM Kt_ProductType";
                try
                {
                    Connection.Open();
                    try
                    {
                        MySqlCommand Command = new MySqlCommand(Query, Connection);
                        MySqlDataReader Reader = Command.ExecuteReader();

                        while (Reader.Read())
                        {
                            ProductType NewProductType = new ProductType();
                            NewProductType.ID = Reader.GetInt32("ID");
                            NewProductType.Name = Reader.GetString("Name");

                            ProductTypes.Add(NewProductType);
                        }
                    }
                    finally
                    {
                        Connection.Close();
                    }
                }
                catch (Exception)
                {
                }
            }

            return ProductTypes;
        }

        public void SaveProduct(Product ChangedProduct)
        {
            throw new NotImplementedException();
        }
    }
}
