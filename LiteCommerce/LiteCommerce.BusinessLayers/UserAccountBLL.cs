using LiteCommerce.DataLayers;
using LiteCommerce.DataLayers.SqlServer;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    public static class UserAccountBLL
    {
        public static string connectionString;
        public static void Initialize(string connectionString)
        {
            UserAccountBLL.connectionString = connectionString;
        }
        public static UserAccount Authorize(string userName,string password,UserAccountTypes userType)
        {
            IUserAccountDAL userAccountDB;
            switch (userType)
            {
                case UserAccountTypes.Employee:
                    userAccountDB = new EmployeeUserAccountDAL(connectionString);
                    break;
                case UserAccountTypes.Customer:
                    userAccountDB = new EmployeeUserAccountDAL(connectionString); //có thể đổi loại user account
                    break;
                default:
                    throw new Exception("Usertype is not valid");
            }
            return userAccountDB.Authorize(userName, password);
        }
    }
}
