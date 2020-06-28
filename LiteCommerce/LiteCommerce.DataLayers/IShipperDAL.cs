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
    public interface IShipperDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Shipper data);
        bool Update(Shipper data);
        bool Delete(int[] shipperIDs);
        Shipper Get(int shipperID);
        List<Shipper> List(int page, int pageSize, string searchValue);
        int Count(string searchValue);
        List<Shipper> List_Shipper_And_ShipperID();
    }
}
