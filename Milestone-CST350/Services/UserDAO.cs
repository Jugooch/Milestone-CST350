using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace Milestone_CST350.Models
{
    public class UserDAO
    {
        //Connection String for the local DB
        String connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=Milestone-350;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public bool addUser(UserModel user)
        {
            bool success = false;
            string sql = "INSERT INTO dbo.Users(FirstName, LastName, Sex, Age, State, Email, Username, Password) VALUES(@firstname, @lastname, @sex, @age, @state, @email, @username, @password) COMMIT;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);

                //Define Placeholders
                command.Parameters.Add("@FIRSTNAME", System.Data.SqlDbType.VarChar, 50).Value = user.FirstName;
                command.Parameters.Add("@LASTNAME", System.Data.SqlDbType.VarChar, 50).Value = user.LastName;
                command.Parameters.Add("@SEX", System.Data.SqlDbType.VarChar, 50).Value = user.Sex;
                command.Parameters.Add("@AGE", System.Data.SqlDbType.VarChar, 50).Value = user.Age;
                command.Parameters.Add("@STATE", System.Data.SqlDbType.VarChar, 50).Value = user.State;
                command.Parameters.Add("@EMAIL", System.Data.SqlDbType.VarChar, 50).Value = user.Email;
                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 50).Value = user.Username;
                command.Parameters.Add("@PASSWORD", System.Data.SqlDbType.VarChar, 50).Value = user.Password;

                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows) { success = true; }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return success;
        }
    }
}
