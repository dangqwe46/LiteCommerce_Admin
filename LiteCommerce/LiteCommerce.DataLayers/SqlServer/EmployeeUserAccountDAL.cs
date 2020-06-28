
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SqlServer
{
    /// <summary>
    /// kiem tra thong tin dang nhap cua nhan vien
    /// </summary>
    public class EmployeeUserAccountDAL : IUserAccountDAL
    {
        private string connectionString;

        public EmployeeUserAccountDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        /// <summary>
        /// Authorize nhan ven 
        /// </summary>
        /// <param name="userName">dia chi email cua nhan vien</param>
        /// <param name="password">mat khau (da ma hoa md5)</param>
        /// <returns></returns>
        public UserAccount Authorize(string userName, string password)
        {
            UserAccount data = null;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"Select Email,FirstName,LastName,PhotoPath,GroupNames from Employees 
                                            where Email =@email and Password=@pass";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@email", userName);
                    cmd.Parameters.AddWithValue("@pass", GetMd5Hash(password));
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            string fName = Convert.ToString(dataReader["FirstName"]);
                            string lName = Convert.ToString(dataReader["LastName"]);
                            data = new UserAccount()
                            {
                                UserID = Convert.ToString(dataReader["Email"]),
                                FullName = fName + lName,
                                Photo = Convert.ToString(dataReader["PhotoPath"]),
                                GroupName = Convert.ToString(dataReader["GroupNames"])


                            };
                        }
                    }
                }
                connection.Close();
            }
            return data;

            //return new UserAccount() {UserID = "employee@gmail.com", FullName="employee" ,Photo ="abc.img"};


        }


        public bool ChangePwd(string userID, string newPass)
        {
            int isSuccess = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"Update Employees 
                                                set 
                                                NewPassword = @NewPassword
                                                Where Email = @Email";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@NewPassword", newPass);
                    cmd.Parameters.AddWithValue("@Email", userID);
                    isSuccess = Convert.ToInt32(cmd.ExecuteNonQuery());
                }
                connection.Close();
            }
            return isSuccess > 0;
        }

        public UserAccount User(string email)
        {
            UserAccount user = null;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"select Password, Email, from Employees Where Email = @Email";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@Email", email);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        if (dbReader.Read())
                        {
                            user = new UserAccount()
                            {
                                Password = Convert.ToString(dbReader["Password"]),
                                UserID = Convert.ToString(dbReader["Email"]),
                            };
                        }
                }
                connection.Close();
            }
            return user;
        }

        private string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();

            }
        }
    }
}
