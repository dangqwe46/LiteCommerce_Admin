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
    public interface ISupplierDAL
    {
        /// <summary>
        /// Bổ sung thêm 1 supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns>ID của supplier được bổ sung (nhỏ hơn hoặc băng 0 nếu lỗi)</returns>
        int Add(Supplier data);
        bool Update(Supplier data);
        bool Delete(int[] supplierIDs);
        Supplier Get(int supplierID);
        List<Supplier> List(int page, int pageSize,string searchValue);
        int Count(string searchValue);
        List<Supplier> List();
        List<Supplier> List_CompanyName_And_SupplierID();
    }
}
