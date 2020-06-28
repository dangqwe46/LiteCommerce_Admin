using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LiteCommerce.DataLayer.SqlServer
{
    public class ProductAttributeDAL : IProductAttributeDAL
    {
        private string connectionString;

        public ProductAttributeDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int Add(ProductAttributes data)
        {
            int attributeId = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO ProductAttributes
                                          (
                                                ProductID,
                                                AttributeName,
                                                AttributeValues,
                                                DisplayOrder
                                          )
                                          VALUES
                                          (
	                                            @ProductID,
                                                @AttributeName,
                                                @AttributeValues,
                                                @DisplayOrder
                                          );
                                          SELECT @@IDENTITY;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@AttributeValues", data.AttributeValues);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);

                attributeId = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return attributeId;
        }

        public bool Delete(int[] attributeIDs)
        {
            bool result = true;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM ProductAttributes
                                            WHERE(AttributeID = @AttributeID)";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.Add("@AttributeID", SqlDbType.Int);
                foreach (int attributeID in attributeIDs)
                {
                    cmd.Parameters["@AttributeID"].Value = attributeID;
                    cmd.ExecuteNonQuery();
                }

                connection.Close();
            }
            return result;
        }

        public ProductAttributes Get(int ProductID)
        {
            ProductAttributes data = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM ProductAttributes WHERE ProductID = @ProductID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@ProductID", ProductID);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new ProductAttributes()
                        {
                            AttributeID = Convert.ToInt32(dbReader["AttributeID"]),
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            AttributeName = Convert.ToString(dbReader["AttributeName"]),
                            AttributeValues = Convert.ToString(dbReader["AttributeValues"]),
                            DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"]),
                        };
                    }
                }
                connection.Close();
            }
            return data;
        }

        public List<ProductAttributes> List(int page, int pageSize, string searchValue)
        {
            throw new NotImplementedException();
        }

        public List<ProductAttributes> List(int productID)
        {
            List<ProductAttributes> data = new List<ProductAttributes>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"select * from ProductAttributes WHERE ProductID = @ProductID order by AttributeName ";
                    cmd.CommandType = CommandType.Text; //cho biet lenh thuc thi la lenh dang gi
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ProductID", productID);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new ProductAttributes()
                            {
                                AttributeID = Convert.ToInt32(dbReader["AttributeID"]),
                                ProductID = Convert.ToInt32(dbReader["ProductID"]),
                                AttributeName = Convert.ToString(dbReader["AttributeName"]),
                                AttributeValues = Convert.ToString(dbReader["AttributeValues"]),
                                DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"]),
                            });
                        }
                    }
                    connection.Close();
                }
                return data;
            }
        }

        public bool Update(ProductAttributes data)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"Update ProductAttributes
                                    SET
                                        ProductID = @ProductID,
                                        AttributeName = @AttributeName,
                                        AttributeValues = @AttributeValues,
                                        DisplayOrder = @DisplayOrder
                                    WHERE
                                        AttributeID = @AttributeID;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@AttributeValues", data.AttributeValues);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@AttributeID", data.AttributeID);

                rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                connection.Close();
            }
            return rowsAffected > 0;
        }
        public bool DeleteByProductID(int productID)
        {
            bool result = true;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"DELETE FROM ProductAttributes
                                            WHERE ProductID = @ProductID

                                             ";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;

                    cmd.Parameters.AddWithValue("@ProductID", productID);

                    cmd.ExecuteNonQuery();


                }
                connection.Close();
            }
            return result;
        }
    }
}