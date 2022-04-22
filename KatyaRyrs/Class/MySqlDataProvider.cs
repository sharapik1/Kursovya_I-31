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
                    pt.`Title`
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
                        //  NewProduct.Category = Reader.GetInt32("Category");
                        NewProduct.CurrentProductType = GetProductType(Reader.GetInt32("Category"));
                        //  NewProduct.CurrentProductType = GetProductType(Reader.GetInt32("ProductTypeID"));
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
                            NewProductType.Title = Reader.GetString("Title");

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
            Connection.Open();
            try
            {
                if (ChangedProduct.ID == 0)
                {
                    string Query = @"INSERT INTO Kt_Product
                    (Name,
                    Number,
                    Weight,
                    Image,
                    Price,
                    Category)
                    VALUES
                    (@Name,
                    @Number,
                    @Weight,
                    @Image,
                    @Price,
                    @Category)";

                    MySqlCommand Command = new MySqlCommand(Query, Connection);
                    Command.Parameters.AddWithValue("@Name", ChangedProduct.Name);
                    Command.Parameters.AddWithValue("@Number", ChangedProduct.Number);
                    Command.Parameters.AddWithValue("@Weight", ChangedProduct.Weight);
                    Command.Parameters.AddWithValue("@Image", ChangedProduct.Image);
                    Command.Parameters.AddWithValue("@Price", ChangedProduct.Price);
                    Command.Parameters.AddWithValue("@Category", ChangedProduct.CurrentProductType.ID);
                    Command.ExecuteNonQuery();
                }
                else
                {
                    string Query = @"UPDATE Kt_Product
                    SET
                    Name = @Name,
                    Number = @Number,
                    Weight = @Weight,
                    Image = @Image,
                    Price = @Price,
                    Category = @Category
                        
                    WHERE ID = @ID";

                    MySqlCommand Command = new MySqlCommand(Query, Connection);
                    Command.Parameters.AddWithValue("@Name", ChangedProduct.Name);
                    Command.Parameters.AddWithValue("@Number", ChangedProduct.Number);
                    Command.Parameters.AddWithValue("@Weight", ChangedProduct.Weight);
                    Command.Parameters.AddWithValue("@Image", ChangedProduct.Image);
                    Command.Parameters.AddWithValue("@Price", ChangedProduct.Price);
                    Command.Parameters.AddWithValue("@Category", ChangedProduct.CurrentProductType.ID);
                    Command.Parameters.AddWithValue("@ID", ChangedProduct.ID);
                    Command.ExecuteNonQuery();
                }
            }
            finally
            {
                Connection.Close();
            }
        }

        public void DeleteProduct(Product DelProduct)
        {
            try
            {
                Connection.Open();
                try
                {
                    string Query = "DELETE FROM Kt_Product WHERE ID = @ID";
                    MySqlCommand command = new MySqlCommand(Query, Connection);
                    command.Parameters.AddWithValue("@ID", DelProduct.ID);
                    command.ExecuteNonQuery();
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
        }
    }
}
