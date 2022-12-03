using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ModelProject1;
using RepoProject1;

namespace BusinessProject1;

public interface IBusinessLayerClass
{
    string AuthUserLogin(string username, string password);
    List<ReimbursementDataClass> GetUserReimbursements(string currentUser);
    List<ReimbursementDataClass> ManagerGetAllReimbursements();
    ReimbursementDataClass ManagerUpdateReimbursement(ReimbursementDataClass reimbursement);
    string NewUser(string username, string password);
    ReimbursementDataClass ReimbursementRequest(ReimbursementDataClass reimbursement, string LogedInUserName);
    List<ReimbursementDataClass> UpdateUserInformation(string currentUser);
}

public class BusinessLayerClass : IBusinessLayerClass
{
    //FIXME temp storage for user data when database is added remove this
    List<UserDataClass> usersList = new List<UserDataClass>();
    List<ReimbursementDataClass> reimbursementDataList = new List<ReimbursementDataClass>();
    List<ReimbursementDataClass> reimbursementDataList2 = new List<ReimbursementDataClass>();
    //end of temp storage
    private readonly IRepoClass _repoClass;

    public BusinessLayerClass(IRepoClass irepoClass)
    {
        _repoClass = irepoClass;
    }

    public string AuthUserLogin(string username, string password)
    {
        //FIXME temp storage for user data when database is added remove this
        //check if repo has users/password return true or false
        //_repoClass.AuthUserLogin(username, password)
        // if (!usersList.Exists(x => x.UserName == "admin" && x.UserPassword == "admin"))
        // {
        //     usersList.Add(new UserDataClass("Admin", "Admin", "admin"));
        //     usersList.Add(new UserDataClass("user", "user", "user"));
        // }
        //end temp storage
        string result = _repoClass.AuthUserLogin(username, password);
        if (!result.Equals("false"))
        {
            #region Authentication
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, username),
            new Claim(ClaimTypes.Role, result)
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
        return _repoClass.GetUserReimbursements(currentUser);
    }


    public List<ReimbursementDataClass> ManagerGetAllReimbursements()
    {
        return _repoClass.ManagerGetAllReimbursements();
    }

    public ReimbursementDataClass ManagerUpdateReimbursement(ReimbursementDataClass reimbursement)
    {
        return _repoClass.ManagerUpdateReimbursement(reimbursement);
    }

    public string NewUser(string username, string password)
    {
        string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
     @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
     @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        Regex re = new Regex(strRegex);
        if (re.IsMatch(username) && password.Length > 1 && password != null)
        {
            return _repoClass.NewUser(username, password);
        }
        else
        {
            //if email is not valid tell user to enter valid email
            return "Invalid email or password";
        }
    }

    public ReimbursementDataClass ReimbursementRequest(ReimbursementDataClass reimbursement, string LogedInUserName)
    {
        reimbursement.UserName = LogedInUserName;
        reimbursement.ReimbursementApproved = false;
        return _repoClass.ReimbursementRequest(reimbursement);
    }

    public List<ReimbursementDataClass> UpdateUserInformation(string currentUser)
    {
        return _repoClass.UpdateUserInformation(currentUser);
    }
}

