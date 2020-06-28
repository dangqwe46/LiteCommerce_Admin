using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using LiteCommerce.DomainModels;

namespace LiteCommerce.DataLayers.SqlServer
{
    public class EmployeeDAL : IEmployeeDAL
    {
        /// <summary>
        /// 
        /// </summary>
        private string connectionString;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public EmployeeDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public int Add(Employee data)
        {
            int EmployeeID = 0;
            using (SqlConnection conection = new SqlConnection(this.connectionString))
            {
                conection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @" 
                                        INSERT INTO Employees
                                        (

                                            LastName,
                                            FirstName,
                                            Title,
                                            BirthDate,
                                            HireDate,
                                            Email,
                                            Address,
                                            City,
                                            Country,
                                            HomePhone,
                                            Notes,
                                            PhotoPath,
                                            Password,
                                            GroupNames

                                        )
                                        VALUES
                                        (
                                            @LastName,
                                            @FirstName,
                                            @Title,
                                            @BirthDate,
                                            @HireDate,
                                            @Email,
                                            @Address,
                                            @City,
                                            @Country,
                                            @HomePhone,
                                            @Notes,
                                            @PhotoPath,
                                            @Password,
                                            @GroupNames
                                        )
                                        SELECT @@IDENTITY
                                        ";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conection;
                    cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", data.LastName);
                    cmd.Parameters.AddWithValue("@Title", data.Title);
                    cmd.Parameters.AddWithValue("@BirthDate", data.BirthDate);
                    cmd.Parameters.AddWithValue("@HireDate", data.HireDate);
                    cmd.Parameters.AddWithValue("@Email", data.Email);
                    cmd.Parameters.AddWithValue("@Address", data.Address);
                    cmd.Parameters.AddWithValue("@City", data.City);
                    cmd.Parameters.AddWithValue("@Country", data.Country);
                    cmd.Parameters.AddWithValue("@HomePhone", data.HomePhone);
                    cmd.Parameters.AddWithValue("@Notes", data.Notes);
                    cmd.Parameters.AddWithValue("@PhotoPath", data.PhotoPath);
                    cmd.Parameters.AddWithValue("@Password", GetMd5Hash(data.Password));
                    cmd.Parameters.AddWithValue("@GroupNames", data.GroupNames);
                    EmployeeID = Convert.ToInt32(cmd.ExecuteScalar());
                }
                conection.Close();
            }
            return EmployeeID;
        }
        public List<Employee> List_FullName_And_EmployeeID()
        {
            List<Employee> data = new List<Employee>();
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"select FirstName, LastName, EmployeeID from Employees ";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            data.Add(new Employee()
                            {
                                FirstName = Convert.ToString(dataReader["FirstName"]),
                                LastName = Convert.ToString(dataReader["LastName"]),
                                EmployeeID = Convert.ToInt32(dataReader["EmployeeID"])

                            });
                        }
                    }
                }
                connection.Close();

            }
            return data;
        }
        public Employee GetByEmail(string email)
        {
            Employee data = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"SELECT * FROM employees WHERE Email = @Email";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@Email", email);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dbReader.Read())
                        {
                            data = new Employee()
                            {
                                EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                                LastName = Convert.ToString(dbReader["LastName"]),
                                FirstName = Convert.ToString(dbReader["FirstName"]),
                                Title = Convert.ToString(dbReader["Title"]),
                                BirthDate = Convert.ToDateTime(dbReader["BirthDate"]),
                                HireDate = Convert.ToDateTime(dbReader["HireDate"]),
                                Email = Convert.ToString(dbReader["Email"]),
                                Address = Convert.ToString(dbReader["Address"]),
                                City = Convert.ToString(dbReader["City"]),
                                Country = Convert.ToString(dbReader["Country"]),
                                HomePhone = Convert.ToString(dbReader["HomePhone"]),
                                Notes = Convert.ToString(dbReader["Notes"]),
                                PhotoPath = Convert.ToString(dbReader["PhotoPath"]),
                                Password = Convert.ToString(dbReader["Password"]),
                                GroupNames = Convert.ToString(dbReader["GroupNames"])
                            };
                        }
                    }
                }
                connection.Close();
            }
            return data;
        }
        public bool Delete(int[] EmployeeIDs)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                int result = 0;
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"DELETE Employees
                                        where EmployeeID=@EmployeeID
	                                        AND EmployeeID NOT IN ( SELECT EmployeeID FROM Orders)";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
                    foreach (int id in EmployeeIDs)
                    {
                        cmd.Parameters["@EmployeeID"].Value = id;
                        result = Convert.ToInt32(cmd.ExecuteNonQuery());
                        if (result <= 0) return false;
                    }

                }
                connection.Close();
            }
            return true;
        }

        public Employee Get(int EmployeeID)
        {
            Employee data = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"SELECT * FROM employees WHERE EmployeeID = @EmployeeID";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dbReader.Read())
                        {
                            data = new Employee()
                            {
                                EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                                LastName = Convert.ToString(dbReader["LastName"]),
                                FirstName = Convert.ToString(dbReader["FirstName"]),
                                Title = Convert.ToString(dbReader["Title"]),
                                BirthDate = Convert.ToDateTime(dbReader["BirthDate"]),
                                HireDate = Convert.ToDateTime(dbReader["HireDate"]),
                                Email = Convert.ToString(dbReader["Email"]),
                                Address = Convert.ToString(dbReader["Address"]),
                                City = Convert.ToString(dbReader["City"]),
                                Country = Convert.ToString(dbReader["Country"]),
                                HomePhone = Convert.ToString(dbReader["HomePhone"]),
                                Notes = Convert.ToString(dbReader["Notes"]),
                                PhotoPath = Convert.ToString(dbReader["PhotoPath"]),
                                Password = Convert.ToString(dbReader["Password"]),
                                GroupNames = Convert.ToString(dbReader["GroupNames"])
                            };
                        }
                    }
                }
                connection.Close();
            }
            return data;
        }

        public List<Employee> List(int page, int pageSize, string searchValue, string countryName)
        {
            List<Employee> data = new List<Employee>();
            if (!string.IsNullOrEmpty(searchValue)) searchValue = "%" + searchValue + "%";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"select *
                                        from
                                        (
	                                        select *,
			                                        ROW_NUMBER() over(order by EmployeeID) as RowNumber
	                                        from Employees
	                                        where ((@searchValue = N'') or (FirstName like @searchValue) or (LastName like @searchValue))
                                                and
			                                        ((@countryName = N'') or (Country = @countryName))
                                        ) as t
                                        where t.RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize
                                        order by t.RowNumber";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                    cmd.Parameters.AddWithValue("@countryName", countryName);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new Employee()
                            {
                                EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                                LastName = Convert.ToString(dbReader["LastName"]),
                                FirstName = Convert.ToString(dbReader["FirstName"]),
                                Title = Convert.ToString(dbReader["Title"]),
                                BirthDate = Convert.ToDateTime(dbReader["BirthDate"]),
                                HireDate = Convert.ToDateTime(dbReader["HireDate"]),
                                Email = Convert.ToString(dbReader["Email"]),
                                Address = Convert.ToString(dbReader["Address"]),
                                City = Convert.ToString(dbReader["City"]),
                                Country = Convert.ToString(dbReader["Country"]),
                                HomePhone = Convert.ToString(dbReader["HomePhone"]),
                                Notes = Convert.ToString(dbReader["Notes"]),
                                PhotoPath = Convert.ToString(dbReader["PhotoPath"]),
                                Password = Convert.ToString(dbReader["Password"]),
                                GroupNames = Convert.ToString(dbReader["GroupNames"])
                                
                            });
                        }
                    }
                }
                connection.Close();
            }
            return data;
        }

        public bool Update(Employee data)
        {
            if (!CheckEmail(data.Email, data.EmployeeID))
            {
                int result = 0;
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = @"UPDATE Employees
                                        SET 
                                            LastName = @LastName,
                                            FirstName = @FirstName,
                                            Title = @Title,
                                            BirthDate = @BirthDate,
                                            HireDate = @HireDate,
                                            Email = @Email,
                                            Address = @Address,
                                            City = @City,
                                            Country = @Country,
                                            HomePhone = @HomePhone,
                                            Notes = @Notes,
                                            PhotoPath = @PhotoPath,
                                            Password = @Password,
                                            GroupNames = @groupNames
                                        WHERE EmployeeID=@EmployeeID";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", data.LastName);
                        cmd.Parameters.AddWithValue("@Title", data.Title);
                        cmd.Parameters.AddWithValue("@BirthDate", data.BirthDate);
                        cmd.Parameters.AddWithValue("@HireDate", data.HireDate);
                        cmd.Parameters.AddWithValue("@Email", data.Email);
                        cmd.Parameters.AddWithValue("@Address", data.Address);
                        cmd.Parameters.AddWithValue("@City", data.City);
                        cmd.Parameters.AddWithValue("@Country", data.Country);
                        cmd.Parameters.AddWithValue("@HomePhone", data.HomePhone);
                        cmd.Parameters.AddWithValue("@Notes", data.Notes);
                        cmd.Parameters.AddWithValue("@PhotoPath", data.PhotoPath);
                        cmd.Parameters.AddWithValue("@Password", data.Password);
                        cmd.Parameters.AddWithValue("@EmployeeID", data.EmployeeID);
                        cmd.Parameters.AddWithValue("@groupNames", data.GroupNames);
                        result = Convert.ToInt32(cmd.ExecuteNonQuery());
                    }
                    connection.Close();
                }
                return result > 0;
            }
            else return false;
        }

        public int CountOfSearchValue(string searchValue, string countryName)
        {
            int rowCount = 0;
            if (!string.IsNullOrEmpty(searchValue)) searchValue = "%" + searchValue + "%";
            using (SqlConnection conection = new SqlConnection(connectionString))
            {
                conection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"select count(*)
                                        from Employees where ((@searchValue = N'') or (FirstName like @searchValue) or (LastName like @searchValue)) and
                                                     ((@countryName = N'') or(Country = @countryName))";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conection;
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                    cmd.Parameters.AddWithValue("@countryName", countryName);
                    rowCount = Convert.ToInt32(cmd.ExecuteScalar());
                }
                conection.Close();
            }
            return rowCount;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="email">email người dùng nhập từ bàn phím</param>
        /// <param name="EmployeeID"> chia làm 2 trường hợp
        /// EmployeeID =0 và employeeID !=0
        /// </param>
        /// <returns></returns>
        public bool CheckEmail(string email, int EmployeeID)
        {
            // truong hop add new employee
            if (EmployeeID <= 0)
            {
                int id = -1;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = @"select EmployeeID from Employees where email=@email";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@email", email);
                        id = Convert.ToInt32(cmd.ExecuteScalar());  //lấy EmployeeID trong trường hợp tìm thấy
                    }
                    connection.Close();
                }
                return id > 0; // nếu email đã tồn tại trong hệ thống thì return true ngược lại return false
            }
            // truong hop update new employee
            else
            {
                Employee employee = Get(EmployeeID);
                if (employee.Email == email) return false;
                else
                {
                    int id;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText = @"select EmployeeID from Employees where email=@email";
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = connection;
                            cmd.Parameters.AddWithValue("@email", email);
                            id = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        connection.Close();
                    }
                    return id > 0;
                }
            }
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