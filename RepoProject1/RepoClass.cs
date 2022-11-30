using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using ModelProject1;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RepoProject1;

public interface IRepoClass
{
    string AuthUserLogin(string username, string password);
    List<ReimbursementDataClass> GetUserReimbursements();
    List<ReimbursementDataClass> ManagerGetAllReimbursements();
    List<ReimbursementDataClass> ManagerUpdateReimbursement();
    string NewUser(string username, string password);
    ReimbursementDataClass ReimbursementRequest(ReimbursementDataClass reimbursement);
    List<ReimbursementDataClass> UpdateUserInformation();
}

public class RepoClass : IRepoClass
{
    //FIXME temp storage for user data when database is added remove this
    List<UserDataClass> usersList = new List<UserDataClass>();
    List<ReimbursementDataClass> reimbursementDataList = new List<ReimbursementDataClass>();


    public string AuthUserLogin(string username, string password)
    {
        if (!usersList.Exists(x => x.Username == "admin" && x.Password == "admin"))
        {
            usersList.Add(new UserDataClass("admin", "admin", "admin"));
        }
        //TODO: Add code to check if user exists in database
        if (usersList.Exists(x => x.Username == username && x.Password == password))
        {
            #region testing
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, username),
            new Claim(ClaimTypes.Role, "user")
        };

            var token = new JwtSecurityToken
            (
                issuer: "https://localhost:5117",
                audience: "https://localhost:5117",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication")),
                    SecurityAlgorithms.HmacSha256)
            );

            string LoginToken = new JwtSecurityTokenHandler().WriteToken(token);
            return LoginToken;
            #endregion
        }
        else
        {
            return "User not found";
        }
    }

    public List<ReimbursementDataClass> GetUserReimbursements()
    {
        //TODO pull current user reimbursements from database
        throw new NotImplementedException();
    }

    public List<ReimbursementDataClass> ManagerGetAllReimbursements()
    {
        throw new NotImplementedException();
    }

    public List<ReimbursementDataClass> ManagerUpdateReimbursement()
    {
        throw new NotImplementedException();
    }

    public string NewUser(string username, string password)
    {
        if (usersList.IsNullOrEmpty())
        {
            usersList.Add(new UserDataClass(username, password, "user"));
            return "User created";
        }
        else
        {
            foreach (var item in usersList)
            {
                if (item.Username == username)
                {
                    return "Username already exists";
                }
                else
                {
                    usersList.Add(new UserDataClass(username, password, "user"));
                    return "User created";
                }
            }
        }
        return "Something went wrong";
    }

    public ReimbursementDataClass ReimbursementRequest(ReimbursementDataClass reimbursement)
    {
        //TODO put reimbursement request into database
        //get data from database and return it
        //if the reimbursementDataList is empty return empty list
        
        throw new NotImplementedException();
    }

    public List<ReimbursementDataClass> UpdateUserInformation()
    {
        throw new NotImplementedException();
    }
}
