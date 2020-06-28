using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    public static class HumanResourceBLL
    {
        private static IEmployeeDAL EmployeeDB { get; set; }

        public static void Initialize(string connectionString)
        {
            EmployeeDB = new DataLayers.SqlServer.EmployeeDAL(connectionString);
        }
        public static List<Employee> Employee_List(int page, int pageSize, string searchValue, string countryName)
        {
            if (page < 1)
                page = 1;
            if (pageSize <= 0)
                pageSize = 30;

            return EmployeeDB.List(page, pageSize, searchValue,countryName);
        }
        /// <summary>
        /// dem so employee
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static int Employee_Count(string searchValue, string countryName)
        {
            return EmployeeDB.CountOfSearchValue(searchValue,countryName);
        }
        /// <summary>
        /// them 1 employee
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int Employee_Add(Employee data)
        {
            return EmployeeDB.Add(data);
        }
        /// <summary>
        /// xoa 1 hoac nhieu employee
        /// </summary>
        /// <param name="employeeIDs"></param>
        /// <returns></returns>
        public static bool Employee_Delete(int[] employeeIDs)
        {
            return EmployeeDB.Delete(employeeIDs);
        }
        /// <summary>
        /// lay ve employee theo ID
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static Employee Employee_Get(int EmployeeID)
        {
            return EmployeeDB.Get(EmployeeID);
        }
        public static Employee Employee_GetByEmail(string email)
        {
            return EmployeeDB.GetByEmail(email);
        }
        /// <summary>
        /// cap nhat employee
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Employee_Update(Employee data)
        {
            return EmployeeDB.Update(data);
        }
        public static bool Employee_CheckEmail(string email, int EmployeeID)
        {
            return EmployeeDB.CheckEmail(email, EmployeeID);
        }
        public static List<Employee> Employee_List_FullName_And_EmployeeID()
        {
            return EmployeeDB.List_FullName_And_EmployeeID();
        }

    }
}
