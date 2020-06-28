using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin
{
    public static class AppSettings
    {
        public static int DefaultPageSize
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["DefaultPageSize"]);
            }
        }
        public static int DefaultEmployeePageSize
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["DefaultEmployeePageSize"]);
            }
        }
    }
}