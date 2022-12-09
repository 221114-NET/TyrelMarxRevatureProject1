using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ModelProject1;

namespace RepoProject1
{
    public class RepoClassManagerUpdateReimbursement : IRepoClassManagerUpdateReimbursement
    {
        public string ManagerUpdateReimbursement(int reimbursementID, bool reimbursementApproved)
        {
            string AzureConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build().GetSection("ConnectionStrings")["RevDatabase"]!;

            ReimbursementDataClass reimbursement = new ReimbursementDataClass();
            string sql;
            if (reimbursementApproved)
            {
                sql = $"UPDATE [dbo].[ReimbursementDataClass] SET ReimbursementApproved = 1, ReimbursementPendingStatus = 0 WHERE ReimbursementID = @reimbursementID and ReimbursementPendingStatus = 1";
            }
            else
            {
                sql = $"UPDATE [dbo].[ReimbursementDataClass] SET ReimbursementApproved = 0, ReimbursementPendingStatus = 0 WHERE ReimbursementID = @reimbursementID and ReimbursementPendingStatus = 1";
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(AzureConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@reimbursementID", reimbursementID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            return "Reimbursement Updated";
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return "Reimbursement Updat Failed";
        }
    }
}