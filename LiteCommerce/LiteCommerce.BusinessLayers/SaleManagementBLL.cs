using LiteCommerce.DataLayers.SqlServer;
using LiteCommerce.DomainModel;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    public static class SaleManagementBLL
    {
        private static OrderDAL OrderDB { get; set; }
        /// <summary>
        /// Hàm này phải được gọi để khởi tạo các chức năng của hệ thống
        /// </summary>
        public static void Initialize(string connectionString)
        {
            OrderDB = new DataLayers.SqlServer.OrderDAL(connectionString);
        }
        /// <summary>
        /// get all orders
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="country"></param>
        /// <param name="customerID"></param>
        /// <param name="employeeID"></param>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static List<Order> Order_List(int page, int pageSize, string country, string customerID, int employeeID, int shipperID)
        {
            if (page < 1) page = 1;
            if (pageSize <= 0) pageSize = 5;
            return OrderDB.List(page, pageSize, country, customerID, employeeID, shipperID);
        }
        /// <summary>
        /// ddeems soos order timf thaays
        /// </summary>
        /// <param name="Country"></param>
        /// <param name="CustomerID"></param>
        /// <param name="EmployeeID"></param>
        /// <param name="ShipperID"></param>
        /// <returns></returns>
        public static int Order_Count(string Country, string CustomerID, int EmployeeID, int ShipperID)
        {
            return OrderDB.Count(Country, CustomerID, EmployeeID, ShipperID);
        }
        /// <summary>
        /// get order by orderID
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static Order Order_Get(int orderID)
        {
            return OrderDB.Get(orderID);
        }

        public static List<Customer> Order_List_CustomerName_And_CustomerID()
        {
            return OrderDB.List_CustomerName_And_CustomerID();
        }
    }
}
