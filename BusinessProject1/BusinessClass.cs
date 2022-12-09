using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using ModelProject1;
using RepoProject1;

namespace BusinessProject1;

public class BusinessLayerClass : IBusinessLayerClass
{
    private readonly IRepoClassAuthUserLogin _IRepoClassAuthUserLogin;
    private readonly IRepoClassGetUserReimbursements _IRepoClassGetUserReimbursements;
    private readonly IRepoClassManagerGetAllReimbursements _IRepoClassManagerGetAllReimbursements;
    private readonly IRepoClassManagerUpdateReimbursement _IRepoClassManagerUpdateReimbursement;
    private readonly IRepoClassNewUser _IRepoClassNewUser;
    private readonly IRepoClassReimbursementRequest _IRepoClassReimbursementRequest;
    private readonly IRepoClassUpdateUserInformation _IRepoClassUpdateUserInformation;


    public BusinessLayerClass(
        IRepoClassAuthUserLogin irepoClassAuthUserLogin,
        IRepoClassGetUserReimbursements irepoClassGetUserReimbursements,
        IRepoClassManagerGetAllReimbursements irepoClassManagerGetAllReimbursements,
        IRepoClassManagerUpdateReimbursement irepoClassManagerUpdateReimbursement,
        IRepoClassNewUser irepoClassNewUser,
        IRepoClassReimbursementRequest irepoClassReimbursementRequest,
        IRepoClassUpdateUserInformation irepoClassUpdateUserInformation
        )
    {
        
        _IRepoClassAuthUserLogin = irepoClassAuthUserLogin;
        _IRepoClassGetUserReimbursements = irepoClassGetUserReimbursements;
        _IRepoClassManagerGetAllReimbursements = irepoClassManagerGetAllReimbursements;
        _IRepoClassManagerUpdateReimbursement = irepoClassManagerUpdateReimbursement;
        _IRepoClassNewUser = irepoClassNewUser;
        _IRepoClassReimbursementRequest = irepoClassReimbursementRequest;
        _IRepoClassUpdateUserInformation = irepoClassUpdateUserInformation;
    }

    public string AuthUserLogin(string username, string password)
    {
        string result = _IRepoClassAuthUserLogin.AuthUserLogin(username, password);
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
        return _IRepoClassGetUserReimbursements.GetUserReimbursements(currentUser);
    }

    public List<ReimbursementDataClass> ManagerGetAllReimbursements()
    {
        return _IRepoClassManagerGetAllReimbursements.ManagerGetAllReimbursements();
    }

    public string ManagerUpdateReimbursement(int reimbursementID, bool reimbursementApproved)
    {
        return _IRepoClassManagerUpdateReimbursement.ManagerUpdateReimbursement(reimbursementID, reimbursementApproved);
    }

    public string NewUser(string username, string password)
    {
        string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
     @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
     @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        Regex re = new Regex(strRegex);
        if (re.IsMatch(username) && password.Length > 1 && password != null)
        {
            return _IRepoClassNewUser.NewUser(username, password);
        }
        else
        {
            //if email is not valid tell user to enter valid email
            return "Invalid email or password";
        }
    }

    public string ReimbursementRequest(string ticketType, double reimbursementAmount, string LogedInUserName)
    {
        return _IRepoClassReimbursementRequest.ReimbursementRequest(ticketType, reimbursementAmount, LogedInUserName);
    }

    public string UpdateUserInformation(string newUserName, string newUserPass, string currentUser)
    {
        string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
     @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
     @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        Regex re = new Regex(strRegex);
        if (re.IsMatch(newUserName) && newUserPass.Length > 1 && newUserPass != null)
        {
            return _IRepoClassUpdateUserInformation.UpdateUserInformation(newUserName, newUserPass, currentUser);
        }
        else
        {
            //if email is not valid tell user to enter valid email
            return "Invalid email or password";
        }
    }
}

