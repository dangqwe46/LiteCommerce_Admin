using LiteCommerce.DomainModel;
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
    public class OrderDAL : IOrderDAL
    {
        private string connectionString;
        public OrderDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public int Count(string Country, string CustomerID, int EmployeeID, int ShipperID)
        {
            int rowCount = 0;


            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {

                connection.Open();

                using (SqlCommand cmd = new SqlCommand())
                {



                    cmd.CommandText = @"select count(*) 
                                        from Orders 
                                                 where  ((@EmployeeID = N'') or (EmployeeID = @EmployeeID)) and
                                                          ((@ShipCountry = N'') or (ShipCountry = @ShipCountry)) and
			                                            ((@ShipperID = N'') or (ShipperID = @ShipperID)) and
                                                        ((@CustomerID = N'') or (CustomerID = @CustomerID)) ";

                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.Connection = connection;

                    cmd.Parameters.AddWithValue("@ShipCountry", Country);
                    cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                    cmd.Parameters.AddWithValue("@ShipperID", ShipperID);
                    cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

                    rowCount = Convert.ToInt32(cmd.ExecuteScalar());
                }

                connection.Close();
            }
            return rowCount;
        }

        public Order Get(int OrderID)
        {
            Order data = null;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT * FROM Orders WHERE OrderID = @OrderID";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@OrderID", OrderID);
                    using (SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dataReader.Read())
                        {
                            data = new Order()
                            {
                                OrderID = Convert.ToInt32(dataReader["OrderID"]),
                                CustomerID = Convert.ToString(dataReader["CustomerID"]),
                                EmployeeID = Convert.ToInt32(dataReader["EmployeeID"]),
                                ShipperID = Convert.ToInt32(dataReader["ShipperID"]),
                                Freight = Convert.ToDecimal(dataReader["Freight"]),
                                ShipCity = Convert.ToString(dataReader["ShipCity"]),
                                ShipCountry = Convert.ToString(dataReader["ShipCountry"]),
                                ShipAddress = Convert.ToString(dataReader["ShipAddress"]),
                                OrderDate = Convert.ToDateTime(dataReader["OrderDate"]),
                                RequiredDate = Convert.ToDateTime(dataReader["RequiredDate"]),
                                ShippedDate = Convert.ToDateTime(dataReader["ShippedDate"])

                            };
                        }


                    }
                }
                connection.Close();
            }
            return data;
        }


        public List<Order> List(int page, int pagesize, string Country, string CustomerID, int EmployeeID, int ShipperID)
        {
            List<Order> data = new List<Order>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"select *
                                        from 
                                        (
	                                        select *,
	                                        ROW_NUMBER() over(order by OrderID) as RowNumber
	                                        from Orders
	                                        where   ((@EmployeeID = N'') or (EmployeeID = @EmployeeID)) and
			                                        ((@ShipperID = N'') or (ShipperID = @ShipperID)) and
                                                    ((@CustomerID = N'') or (CustomerID = @CustomerID))and
                                                     ((@ShipCountry = N'') or (ShipCountry = @ShipCountry))
                                                    
                                        ) as t
                                        where t.RowNumber between (@page-1)*@pageSize + 1 and @page*@pageSize
                                        order by t.RowNumber";

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@pageSize", pagesize);
                    cmd.Parameters.AddWithValue("@ShipCountry", Country);
                    cmd.Parameters.AddWithValue("@ShipperID", ShipperID);
                    cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                    cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

                    using (SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {

                        while (dataReader.Read())
                        {
                            data.Add(new Order()
                            {
                                OrderID = Convert.ToInt32(dataReader["OrderID"]),
                                CustomerID = Convert.ToString(dataReader["CustomerID"]),
                                EmployeeID = Convert.ToInt32(dataReader["EmployeeID"]),
                                ShipperID = Convert.ToInt32(dataReader["ShipperID"]),
                                Freight = Convert.ToDecimal(dataReader["Freight"]),
                                ShipCity = Convert.ToString(dataReader["ShipCity"]),
                                ShipCountry = Convert.ToString(dataReader["ShipCountry"]),
                                ShipAddress = Convert.ToString(dataReader["ShipAddress"]),
                                OrderDate = Convert.ToDateTime(dataReader["OrderDate"]),
                                RequiredDate = Convert.ToDateTime(dataReader["RequiredDate"]),
                                ShippedDate = Convert.ToDateTime(dataReader["ShippedDate"])

                            });
                        }

                    }
                }
                //dong ket noi
                connection.Close();
            }
            return data;
        }

        public List<Customer> List_CustomerName_And_CustomerID()
        {
            List<Customer> data = new List<Customer>();
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"select ContactName, CustomerID
                                                from Customers 
                                                where CustomerID in (select CustomerID from Orders)";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            data.Add(new Customer()
                            {
                                ContactName = Convert.ToString(dataReader["ContactName"]),
                                CustomerID = Convert.ToString(dataReader["CustomerID"])
                            });
                        }
                    }
                }
                connection.Close();

            }
            return data;
        }
    }
}
