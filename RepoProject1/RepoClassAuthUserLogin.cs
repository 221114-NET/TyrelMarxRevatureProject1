using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace RepoProject1
{
    public class RepoClassAuthUserLogin: IRepoClassAuthUserLogin
    {
        public string AuthUserLogin(string username, string password)
    {
        string AzureConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build().GetSection("ConnectionStrings")["RevDatabase"]!;
        String sql = $"SELECT * FROM [dbo].[UserDataClass]WHERE UserName = @username and UserPassword = @password";
        try
        {
            using (SqlConnection connection = new SqlConnection(AzureConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                return reader.GetString(3);
                            }
                        }
                        else
                        {
                            return "false";
                        }
                    }
                }
            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
            return "false";
        }
        return "false";
    }
    }
}