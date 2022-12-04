using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using ModelProject1;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace RepoProject1;

public interface IRepoClass
{
    string AuthUserLogin(string username, string password);
    List<ReimbursementDataClass> GetUserReimbursements(string currentUser);
    List<ReimbursementDataClass> ManagerGetAllReimbursements();
    string ManagerUpdateReimbursement(int reimbursementID, bool reimbursementApproved);
    string NewUser(string username, string password);
    string ReimbursementRequest(string ticketType, double reimbursementAmount, string LogedInUserName);
    List<ReimbursementDataClass> UpdateUserInformation(string currentUser);
}

public class RepoClass : IRepoClass
{
    // //FIXME temp storage for user data when database is added remove this
    // List<UserDataClass> usersList = new List<UserDataClass>();
    // List<ReimbursementDataClass> reimbursementDataList = new List<ReimbursementDataClass>();
    // List<ReimbursementDataClass> reimbursementDataList2 = new List<ReimbursementDataClass>();
    // //end of temp storage
    string AzureConnectionString = "Server=tcp:revdbo.database.windows.net,1433;Initial Catalog=RevP1;Persist Security Info=False;User ID=tyrel;Password=password1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


    public string AuthUserLogin(string username, string password)
    {
        String sql = $"SELECT * FROM [dbo].[UserDataClass]WHERE UserName = '{username}' and UserPassword = '{password}'";
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
                            while (reader.Read())
                            {
                                return reader.GetString(2);
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
        String sql = $"SELECT * FROM [dbo].[ReimbursementDataClass] WHERE UserName = '{currentUser}'";

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
                                reimbursementDataList.Add(new ReimbursementDataClass(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDouble(3), reader.GetBoolean(4), reader.GetBoolean(5)));
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
                                reimbursementDataList.Add(new ReimbursementDataClass(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDouble(3), reader.GetBoolean(4), reader.GetBoolean(5)));
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
        //TODO test this pending stats is not updating corectly prolly did the sql query wrong
        string sql = $"UPDATE [dbo].[ReimbursementDataClass] SET ReimbursementApproved = {reimbursementApproved}, ReimbursementPendingStatus = 0 WHERE ReimbursementID = {reimbursementID} and ReimbursementPendingStatus = 1";
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
                            while (reader.Read())
                            {
                                return "Reimbursement Updated";
                            }
                        }
                        else
                        {
                            return "Reimbursement Updat Failed";
                        }
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
        String sql = $"INSERT INTO [dbo].[UserDataClass]([UserName], [UserPassword], [UserRole]) VALUES('{username}', '{password}', 'user')";
        try
        {
            using (SqlConnection connection = new SqlConnection(AzureConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
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
        string sql = $"INSERT INTO [dbo].[ReimbursementDataClass]([UserName], [ReimbursementType], [ReimbursementAmount],[ReimbursementApproved],[ReimbursementPendingStatus]) VALUES('{LogedInUserName}', '{ticketType}', {reimbursementAmount}, 0, 1)";
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
                            while (reader.Read())
                            {
                                return "Reimbursement Requested";
                            }
                        }
                        else
                        {
                            return "Reimbursement Request Failed";
                        }
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

    public List<ReimbursementDataClass> UpdateUserInformation(string currentUser)
    {
        //TODO pull current user information from database and make sure current user is the one updating the information then update user information
        throw new NotImplementedException();
    }
}
