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
    public interface ICategoryDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Category data);
        bool Update(Category data);
        bool Delete(int[] categoryIDs);
        Category Get(int categoryID);
        List<Category> List(int page, int pageSize, string searchValue);
        int Count(string searchValue);
        List<Category> List();
        List<Category> List_CategoryName_And_CategoryID();
    }
}
