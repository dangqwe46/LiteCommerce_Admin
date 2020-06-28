using LiteCommerce.DomainModel;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IOrderDAL
    {
        /// <summary>
        /// get Order
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        Order Get(int OrderID);
        /// <summary>
        /// get list Order
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="Country"></param>
        /// <param name="CustomerID"></param>
        /// <param name="EmployeeID"></param>
        /// <param name="ShipperID"></param>
        /// <returns></returns>
        List<Order> List(int page, int pagesize, string Country, string CustomerID, int EmployeeID, int ShipperID);
        /// <summary>
        /// count order tim thay
        /// </summary>
        /// <param name="Country"></param>
        /// <param name="CustomerID"></param>
        /// <param name="EmployeeID"></param>
        /// <param name="ShipperID"></param>
        /// <returns></returns>
        int Count(string Country, string CustomerID, int EmployeeID, int ShipperID);
        /// <summary>
        /// order details
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>

        List<Customer> List_CustomerName_And_CustomerID();
    }
}
