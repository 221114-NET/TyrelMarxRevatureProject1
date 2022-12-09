using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace RepoProject1
{
    public class RepoClassUpdateUserInformation : IRepoClassUpdateUserInformation
    {
        public string UpdateUserInformation(string newUserName, string newUserPass, string currentUser)
        {
            string AzureConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build().GetSection("ConnectionStrings")["RevDatabase"]!;

            string sql = $"UPDATE [dbo].[UserDataClass] SET UserName = @newUserName, UserPassword = @newUserPass WHERE Username = @currentUser";
            try
            {
                using (SqlConnection connection = new SqlConnection(AzureConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@newUserName", newUserName);
                        command.Parameters.AddWithValue("@newUserPass", newUserPass);
                        command.Parameters.AddWithValue("@currentUser", currentUser);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            return "User Information Updated";
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                return "User Information Update Failed";
            }
        }
    }
}