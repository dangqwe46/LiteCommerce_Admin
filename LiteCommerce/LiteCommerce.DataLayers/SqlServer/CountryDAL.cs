
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SqlServer
{
    public class CountryDAL : ICountryDAL
    {
        private string connectionString;
        public CountryDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Country> GetAllCountry()
        {
            List<Country> data = new List<Country>();
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"select * from Countries";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            data.Add(new Country()
                            {
                                CountryID = Convert.ToInt32(dataReader["id"]),
                                CountryCode = Convert.ToString(dataReader["country_code"]),
                                CountryName = Convert.ToString(dataReader["country_name"])
                            });
                        }
                    }
                }
                connection.Close();

            }
            return data;
        }
    }
}