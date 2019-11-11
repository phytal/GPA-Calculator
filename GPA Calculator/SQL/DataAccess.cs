using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace GPA_Calculator
{
    public class DataAccess
    {
        public List<User> GetUser(string name)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("GPA Calculator")))
            {
                var output = connection.Query<User>("dbo.GPACalculator_UserByName @Name", new { Name = name }).ToList();
                return output;
            }
        }

        public void InsertUser(string username, string password, string name)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("GPA Calculator")))
            {
                List<User> users = new List<User>();
                users.Add(new User { Username= username, Password = password, Name = name });
                connection.Execute("dbo.GPACalculator_InsertUser @Username, @Password, @Name", users);
            }
        }

        public void DeleteUser(string username, string password, string name)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("GPA Calculator")))
            {
                List<User> users = new List<User>();
                users.Add(new User { Username = username, Password = password, Name = name });
                connection.Execute("dbo.GPACalculator_DeleteUser @Username, @Password, @Name", users);
            }
        }

        public void EditUser(string username, string password, string name)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("GPA Calculator")))
            {
                List<User> users = new List<User>();
                users.Add(new User { Username = username, Password = password, Name = name });
                connection.Execute("dbo.GPACalculator_EditUser @Username, @Password, @Name", users);
            }
        }
    }
}
