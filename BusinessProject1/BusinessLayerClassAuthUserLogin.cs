using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using RepoProject1;

namespace BusinessProject1
{
    public class BusinessLayerClassAuthUserLogin : IBusinessLayerClassAuthUserLogin
    {
        private readonly IRepoClassAuthUserLogin _IRepoClassAuthUserLogin;

        public BusinessLayerClassAuthUserLogin(IRepoClassAuthUserLogin irepoClassAuthUserLogin)
        {
            _IRepoClassAuthUserLogin = irepoClassAuthUserLogin;
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
    }
}