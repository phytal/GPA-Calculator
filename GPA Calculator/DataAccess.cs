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
    }
}
