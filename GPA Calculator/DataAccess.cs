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
                var output = connection.Query<User>($"select * from Users where name = '{name}'").ToList();
                return output;
            }
        }
    }
}
