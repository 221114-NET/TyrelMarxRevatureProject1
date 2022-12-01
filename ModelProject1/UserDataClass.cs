
using System.Text.Json.Serialization;

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
    [JsonPropertyName("username")]
    public string Username { get; set; }
    
    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("role")]
    public string Role { get; set; }
    

}



