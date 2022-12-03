namespace UnitTestRevP1;

using System.Collections.Generic;
using System.Security.Claims;
using BusinessProject1;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ModelProject1;
using RepoProject1;


public class UnitTest1
{
    internal class testRepo : IRepoClass
    {
        public string AuthUserLogin(string username, string password)
        {
            return "false";
        }

        public List<ReimbursementDataClass> GetUserReimbursements(string currentUser)
        {
            throw new NotImplementedException();
        }

        public List<ReimbursementDataClass> ManagerGetAllReimbursements()
        {
            throw new NotImplementedException();
        }

        public ReimbursementDataClass ManagerUpdateReimbursement(ReimbursementDataClass reimbursement)
        {
            throw new NotImplementedException();
        }

        public string NewUser(string username, string password)
        {
            return "true";
        }

        public ReimbursementDataClass ReimbursementRequest(ReimbursementDataClass reimbursement)
        {
            throw new NotImplementedException();
        }

        public List<ReimbursementDataClass> UpdateUserInformation(string currentUser)
        {
            throw new NotImplementedException();
        }
    }

    [Theory]
    [InlineData($"test@test.com", "temp")]
    [InlineData("", "")]
    [InlineData("user", "")]
    [InlineData("", "pass")]
    [InlineData("admin", "admin")]
    public void Test1(string value1, string value2)
    {
        System.Console.WriteLine(value1, value2);
        BusinessLayerClass test = new BusinessLayerClass(new testRepo());
        var result = test.NewUser(value1, value2);
        Assert.Contains("true", result);
    }

    [Theory]
    [InlineData("Admin", "Admin")]
    [InlineData("user", "user")]
    public void Test2(string value1, string value2)
    {
        BusinessLayerClass test = new BusinessLayerClass(new testRepo());
        var result = test.AuthUserLogin(value1, value2);

        #region Authentication
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, value1),
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
        #endregion
        Assert.Contains(LoginToken, result);

    }

    [Theory]
    [InlineData("", "")]
    [InlineData("user", "")]
    [InlineData("", "pass")]
    [InlineData("user@test.com", "pass")]
    [InlineData("admin", "admin")]
    public void Test3(string value1, string value2)
    {
        BusinessLayerClass test = new BusinessLayerClass(new testRepo());
        var result = test.AuthUserLogin(value1, value2);
        Assert.Contains("User not found", result);
    }
}
