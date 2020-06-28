
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IEmployeeDAL
    {
        /// <summary>
        /// Bổ sung thêm 1 Employee
        /// </summary>
        /// <param name="data"></param>
        /// <returns>ID của Employee (<=0 nếu lỗi)</returns>
        int Add(Employee data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Employee data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmployeeIDs"></param>
        /// <returns></returns>
        bool Delete(int[] EmployeeIDs);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        Employee Get(int EmployeeID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Employee> List(int page, int pageSize, string searchValue, string countryName);
        int CountOfSearchValue(string searchValue, string countryName);

        bool CheckEmail(string email, int EmployeeID);
        List<Employee> List_FullName_And_EmployeeID();
        Employee GetByEmail(string email);

    }
}
