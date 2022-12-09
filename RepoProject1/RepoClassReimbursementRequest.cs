using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace RepoProject1
{
    public class RepoClassReimbursementRequest : IRepoClassReimbursementRequest
    {
    string AzureConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build().GetSection("ConnectionStrings")["RevDatabase"]!;

        public string ReimbursementRequest(string ticketType, double reimbursementAmount, string LogedInUserName)
    {
        string sql = $"INSERT INTO [dbo].[ReimbursementDataClass]([UserID], [ReimbursementType], [ReimbursementAmount],[ReimbursementApproved],[ReimbursementPendingStatus]) VALUES((SELECT UserID From [dbo].[UserDataClass] WHERE UserName = @LogedInUserName), @ticketType, @reimbursementAmount, 0, 1)";
        try
        {
            using (SqlConnection connection = new SqlConnection(AzureConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ticketType", ticketType);
                    command.Parameters.AddWithValue("@reimbursementAmount", reimbursementAmount);
                    command.Parameters.AddWithValue("@LogedInUserName", LogedInUserName);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        return "Reimbursement Requested";
                    }
                }
            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }
        return "Reimbursement Request Failed";
    }
    }
}