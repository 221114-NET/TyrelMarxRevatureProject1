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
    ReimbursementDataClass ManagerUpdateReimbursement(ReimbursementDataClass reimbursement);
    string NewUser(string username, string password);
    ReimbursementDataClass ReimbursementRequest(ReimbursementDataClass reimbursement);
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
        String sql = $"SELECT * FROM [dbo].[ReimbursementDataClass]";

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

    public ReimbursementDataClass ManagerUpdateReimbursement(ReimbursementDataClass reimbursement)
    {
        //TODO tell the database to update the reimbursement if pendingStatus = true else throw error
        throw new NotImplementedException();
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

    public ReimbursementDataClass ReimbursementRequest(ReimbursementDataClass reimbursement)
    {
        //TODO put reimbursement request into database
        //get data from database and return it
        //if the reimbursementDataList is empty return empty list

        throw new NotImplementedException();
    }

    public List<ReimbursementDataClass> UpdateUserInformation(string currentUser)
    {
        //TODO pull current user information from database and make sure current user is the one updating the information then update user information
        throw new NotImplementedException();
    }
}
