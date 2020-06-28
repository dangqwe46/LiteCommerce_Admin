using LiteCommerce.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    public class Order_Result : PaginationResult
    {
        public List<Order> Data;
        public int employeeID { get; set; }
        public int shipperID { get; set; }
        public string customerID { get; set; }
        public string ShipCountry { get; set; }
    }
}