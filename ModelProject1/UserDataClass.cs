using System.Security.Claims;

namespace ModelProject1;

public interface IUserDataClass
{
    string Username { get; set; }
    string Password { get; set; }
    string Role { get; set; }

}

public class UserDataClass : IUserDataClass
{
    public UserDataClass(string username, string password, string role)
    {
        this.Username = username;
        this.Password = password;
        this.Role = role;
    }

    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    

}



