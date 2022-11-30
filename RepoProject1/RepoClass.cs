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
    List<ReimbursementDataClass> GetUserReimbursements(string currentUser);
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
    List<ReimbursementDataClass> reimbursementDataList2 = new List<ReimbursementDataClass>();
    //end of temp storage

    public string AuthUserLogin(string username, string password)
    {
        //FIXME temp storage for user data when database is added remove this
        if (!usersList.Exists(x => x.Username == "admin" && x.Password == "admin"))
        {
            usersList.Add(new UserDataClass("admin", "admin", "admin"));
            usersList.Add(new UserDataClass("user", "user", "user"));
        }
        //end temp storage
        if (usersList.Exists(x => x.Username == username && x.Password == password))
        {
            #region Authentication
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

    public List<ReimbursementDataClass> GetUserReimbursements(string currentUser)
    {
        //TODO pull current user reimbursements from database and remove this
        reimbursementDataList.Add(new ReimbursementDataClass("user", "car", 12.1, "", true));
        reimbursementDataList.Add(new ReimbursementDataClass("user", "boat", 14.1, "", true));
        reimbursementDataList.Add(new ReimbursementDataClass("admin", "plane", 1000.1, "", true));
        reimbursementDataList.Add(new ReimbursementDataClass("admin", "space ship", 1000000, "", true));
        //end of remove

        foreach (var item in reimbursementDataList)
        {
            if (item.userName == currentUser)
            {
                reimbursementDataList2.Add(item);
            }
        }

        return reimbursementDataList2;
    }

    public List<ReimbursementDataClass> ManagerGetAllReimbursements()
    {
        //TODO remove these and use data from database
        reimbursementDataList.Add(new ReimbursementDataClass("user", "car", 12.1, "", true));
        reimbursementDataList.Add(new ReimbursementDataClass("user", "boat", 14.1, "", true));
        reimbursementDataList.Add(new ReimbursementDataClass("admin", "plane", 1000.1, "", true));
        reimbursementDataList.Add(new ReimbursementDataClass("admin", "space ship", 1000000, "", true));
        //end of remove
        return reimbursementDataList;
    }

    public List<ReimbursementDataClass> ManagerUpdateReimbursement()
    {
        throw new NotImplementedException();
    }

    public string NewUser(string username, string password)
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
        return "User created";
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
