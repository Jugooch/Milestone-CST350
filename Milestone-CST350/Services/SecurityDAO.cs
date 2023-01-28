using Milestone_CST350.Models;
using System.Data.SqlClient;

namespace Milestone_CST350.Services
{
    public class SecurityDAO
    {
        //Connection String for the local DB
        String connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=Milestone-350;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public bool findUser(UserModel user) 
        {
            bool success = false;
            string sql = "SELECT * FROM dbo.Users WHERE Username = @username and Password = @password";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);

                //Define Plaeholders
                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 50).Value = user.Username;
                command.Parameters.Add("@PASSWORD", System.Data.SqlDbType.VarChar, 50).Value = user.Password;

                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if(reader.HasRows) { success = true; }
                }
                catch(Exception e) 
                { 
                    Console.WriteLine(e.Message);
                }
            }
            return success;
        }
    }
}
