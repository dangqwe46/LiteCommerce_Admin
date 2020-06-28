
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SqlServer
{
    public class CustomerDAL : ICustomerDAL
    {
        private string connectionString;
        public CustomerDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public string Add(Customer data)
        {
            string CustomerId ="";
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Customers
                                          (
                                              CustomerID,
	                                          CompanyName,
	                                          ContactName,
	                                          ContactTitle,
	                                          Address,
	                                          City,
	                                          Country,
	                                          Phone,
	                                          Fax
	                                          
                                          )
                                          VALUES
                                          (
                                              @CustomerID,
	                                          @CompanyName,
	                                          @ContactName,
	                                          @ContactTitle,
	                                          @Address,
	                                          @City,
	                                          @Country,
	                                          @Phone,
	                                          @Fax
                                          );
                                          SELECT @@IDENTITY;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@CustomerID", data.CustomerID);
                cmd.Parameters.AddWithValue("@CompanyName", data.CompanyName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@ContactTitle", data.ContactTitle);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);
                cmd.Parameters.AddWithValue("@Fax", data.Fax);

                CustomerId = Convert.ToString(cmd.ExecuteScalar());

                connection.Close();
            }

            return CustomerId;
        }

        public int Count(string searchValue)
        {
            int rowCount = 0;
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = "%" + searchValue + "%";
            }
            // tao doi tuong ket noi csdl
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // mo ket noi
                connection.Open();
                // cau lenh thuc thi yeu cau truy van du lieu
                using (SqlCommand cmd = new SqlCommand())
                {
                    // chuoi chua cau lenh can thuc thi
                    cmd.CommandText = @"select count(*) 
                                        from Customers 
                                                where (@searchValue = N'') 
                                                or (CompanyName like @searchValue)";
                    //cho biet lenh su dung de thuc thi ở dạng nào
                    cmd.CommandType = System.Data.CommandType.Text;
                    //
                    cmd.Connection = connection;
                    //dua du lieu vao cau lenh sql
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);

                    //Thuc thi cau lenh (cmd.ExecuteReader)
                    //SqlDataReader dbReader tao doi tuong luu tru du lieu 
                    rowCount = Convert.ToInt32(cmd.ExecuteScalar());
                }
                //dong ket noi
                connection.Close();
            }
            return rowCount;
        }

        public bool Delete(string[] customerIDs)
        {
            bool result = true;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Customers
                                            WHERE(CustomerID = @customerID)";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.Add("@customerID", SqlDbType.NVarChar);
                foreach (string customerID in customerIDs)
                {
                    cmd.Parameters["@customerID"].Value = customerID;
                    cmd.ExecuteNonQuery();
                }

                connection.Close();
            }
            return result;
        }

        public Customer Get(string customerID)
        {
            Customer data = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Customers WHERE CustomerID = @customerID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@customerID", customerID);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Customer()
                        {
                            CustomerID = Convert.ToString(dbReader["CustomerID"]),
                            CompanyName = Convert.ToString(dbReader["CompanyName"]),
                            ContactName = Convert.ToString(dbReader["ContactName"]),
                            ContactTitle = Convert.ToString(dbReader["ContactTitle"]),
                            Address = Convert.ToString(dbReader["Address"]),
                            City = Convert.ToString(dbReader["City"]),
                            Country = Convert.ToString(dbReader["Country"]),
                            Fax = Convert.ToString(dbReader["Fax"]),
                            Phone = Convert.ToString(dbReader["Phone"])
                        };
                    }
                }

                connection.Close();
            }
            return data;
        }

        public List<Customer> List(int page, int pagesize, string searchValue)
        {
            List<Customer> data = new List<Customer>();
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = "%" + searchValue + "%";
            }
            // tao doi tuong ket noi csdl
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // mo ket noi
                connection.Open();
                // cau lenh thuc thi yeu cau truy van du lieu
                using (SqlCommand cmd = new SqlCommand())
                {
                    // chuoi chua cau lenh can thuc thi
                    cmd.CommandText = @"select * 
                                        from
                                        (
                                        select * , ROW_NUMBER() over(order by CustomerID) as RowNumber
                                        from Customers
                                        where (@searchValue = N'') 
                                                or (CompanyName like @searchValue)
                                        ) as t
                                        where t.RowNumber between (@page -1) * @pageSize +1  and @page* @pageSize
                                        order by t.RowNumber";
                    //cho biet lenh su dung de thuc thi ở dạng nào
                    cmd.CommandType = System.Data.CommandType.Text;
                    //
                    cmd.Connection = connection;
                    //dua du lieu vao cau lenh sql
                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@pageSize", pagesize);
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);

                    //Thuc thi cau lenh (cmd.ExecuteReader)
                    //SqlDataReader dbReader tao doi tuong luu tru du lieu 
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        // lay du lieu dua vao list<supplier>
                        while (dbReader.Read())
                        {
                            data.Add(new Customer()
                            {
                                CustomerID = Convert.ToString(dbReader["CustomerID"]),
                                CompanyName = Convert.ToString(dbReader["CompanyName"]),
                                ContactName = Convert.ToString(dbReader["ContactName"]),
                                ContactTitle = Convert.ToString(dbReader["ContactTitle"]),
                                Address = Convert.ToString(dbReader["Address"]),
                                City = Convert.ToString(dbReader["City"]),
                                Country = Convert.ToString(dbReader["Country"]),
                                Phone = Convert.ToString(dbReader["Phone"]),
                                Fax = Convert.ToString(dbReader["Fax"])

                            });
                        }

                    }
                }
                //dong ket noi
                connection.Close();
            }
            return data;
        }

        public bool Update(Customer data)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"UPDATE Customers SET 
                                                CompanyName = @CompanyName,
                                                ContactName = @ContactName,
                                                ContactTitle =@ContactTitle,
                                                Address= @Address,
                                                City= @City,
                                                Country= @Country,
                                                Phone = @Phone,
                                                Fax = @Fax                                                
                                                WHERE CustomerID = @customerID";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@CompanyName", data.CompanyName);
                    cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                    cmd.Parameters.AddWithValue("@ContactTitle", data.ContactTitle);
                    cmd.Parameters.AddWithValue("@Address", data.Address);
                    cmd.Parameters.AddWithValue("@City", data.City);
                    cmd.Parameters.AddWithValue("@Country", data.Country);
                    cmd.Parameters.AddWithValue("@Phone", data.Phone);
                    cmd.Parameters.AddWithValue("@Fax", data.Fax);
                    cmd.Parameters.AddWithValue("@customerID", data.CustomerID);

                    rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                }
                connection.Close();
            }
            return rowsAffected > 0;
        }
    }
}
