using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    public static class CountryBLL
    {
        private static ICountryDAL CountryDB { get; set; }
        public static void Initialize(string connectionString)
        {
            CountryDB = new DataLayers.SqlServer.CountryDAL(connectionString);

        }
        public static List<Country> GetAllCountry()
        {
            return CountryDB.GetAllCountry();
        }
    }
}