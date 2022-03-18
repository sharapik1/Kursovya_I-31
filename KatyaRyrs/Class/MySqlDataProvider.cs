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
            List<Product> Listproducts = new List<Product>();
            string Sql = "SELECT * FROM Kt_Product";
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
                        NewProduct.id = Reader.GetInt32("id");
                        NewProduct.Name = Reader.GetString("Name");
                        NewProduct.Number = Reader.GetInt32("Number");
                        NewProduct.Weight = Reader.GetString("Weight");
                        NewProduct.Type = Reader.GetString("Type");
                        NewProduct.Image = Reader.GetString("Image");
                        NewProduct.Price = Reader.GetDecimal("Price");
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
        
    }
}
