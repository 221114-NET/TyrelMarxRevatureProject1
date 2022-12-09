using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ModelProject1;

namespace RepoProject1
{
    public class RepoClassManagerGetAllReimbursements : IRepoClassManagerGetAllReimbursements
    {
        public List<ReimbursementDataClass> ManagerGetAllReimbursements()
        {
            string AzureConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build().GetSection("ConnectionStrings")["RevDatabase"]!;

            List<ReimbursementDataClass> reimbursementDataList = new List<ReimbursementDataClass>();
            String sql = $"SELECT * FROM [dbo].[ReimbursementDataClass] WHERE ReimbursementPendingStatus = 1";

            try
            {
                using (SqlConnection connection = new SqlConnection(AzureConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                int i = 0;
                                while (reader.Read())
                                {
                                    reimbursementDataList.Add(new ReimbursementDataClass(reader.GetInt32(0), reader.GetString(2), reader.GetDouble(3), reader.GetBoolean(4), reader.GetBoolean(5)));
                                    i++;
                                }
                            }
                            else
                            {
                                return reimbursementDataList;
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                return reimbursementDataList;
            }
            return reimbursementDataList;
        }
    }
}