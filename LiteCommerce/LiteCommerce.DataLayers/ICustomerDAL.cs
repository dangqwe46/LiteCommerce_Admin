using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICustomerDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string Add(Customer data);
        bool Update(Customer data);
        bool Delete(string[] customerIDs);
        Customer Get(string customerID);
        List<Customer> List(int page, int pageSize, string searchValue);
        int Count(string searchValue);
    }
}
