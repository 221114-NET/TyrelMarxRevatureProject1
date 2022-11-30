using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ModelProject1;
namespace RepoProject1;

public interface IRepoClass
{
    string AuthUserLogin(string username, string password);
    List<ReimbursementDataClass> GetUserReimbursements();
    string NewUser(string username, string password);
    ReimbursementDataClass ReimbursementRequest(ReimbursementDataClass reimbursement);
}

public class RepoClass : IRepoClass
{
    //FIXME temp storage for user data when database is added remove this
    List<UserDataClass> users = new List<UserDataClass>();

    public string AuthUserLogin(string username, string password)
    {
        if (!users.Exists(x => x.Username == "admin" && x.Password == "admin"))
        {
            users.Add(new UserDataClass("admin", "admin", "admin"));
        }
        //TODO: Add code to check if user exists in database
        if (users.Exists(x => x.Username == username && x.Password == password))
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

    public string NewUser(string username, string password)
    {
        if (users.Any(x => x.Username == username))
        {
            return "Username already exists";
        }
        else
        {
            users.Add(new UserDataClass(username, password, "user"));
            return "User created";
        }
    }

    public ReimbursementDataClass ReimbursementRequest(ReimbursementDataClass reimbursement)
    {
        //TODO put reimbursement request into database
        throw new NotImplementedException();
    }
}
