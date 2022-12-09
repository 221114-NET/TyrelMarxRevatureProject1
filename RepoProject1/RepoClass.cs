using Microsoft.Data.SqlClient;
using ModelProject1;
using Microsoft.Extensions.Configuration;


namespace RepoProject1;

public class RepoClass : IRepoClass
{
    string AzureConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build().GetSection("ConnectionStrings")["RevDatabase"]!;

    
    public string AuthUserLogin(string username, string password)
    {
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

    public List<ReimbursementDataClass> GetUserReimbursements(string currentUser)
    {
        List<ReimbursementDataClass> reimbursementDataList = new List<ReimbursementDataClass>();
        String sql = $"SELECT * FROM [dbo].[ReimbursementDataClass] WHERE UserID = (SELECT UserID From [dbo].[UserDataClass] WHERE UserName = @currentUser)";

        try
        {
            using (SqlConnection connection = new SqlConnection(AzureConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@currentUser", currentUser);
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

    public List<ReimbursementDataClass> ManagerGetAllReimbursements()
    {
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

    public string ManagerUpdateReimbursement(int reimbursementID, bool reimbursementApproved)
    {
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

    public string NewUser(string username, string password)
    {
        String sql = $"INSERT INTO [dbo].[UserDataClass]([UserName], [UserPassword], [UserRole]) VALUES(@username, @password, 'user')";
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
                        return "User Created";
                    }
                }
            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
            return "User Name Taken";
        }
    }

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

    public string UpdateUserInformation(string newUserName, string newUserPass, string currentUser)
    {
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
