using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModel
{
    public class Order
    {
        /// <summary>
        /// 
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CustomerID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int EmployeeID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime RequiredDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ShippedDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ShipperID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal Freight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ShipAddress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ShipCity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ShipCountry { get; set; }

    }
}
