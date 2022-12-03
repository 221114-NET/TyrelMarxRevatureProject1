
using System.Text.Json.Serialization;

namespace ModelProject1;


public interface IUserDataClass
{
    string UserName { get; set; }
    string UserPassword { get; set; }
    string UserRole { get; set; }

}

public class UserDataClass : IUserDataClass
{
    public UserDataClass(string username, string userpassword, string userrole)
    {
        this.UserName = username;
        this.UserPassword = userpassword;
        this.UserRole = userrole;
    }
    [JsonPropertyName("username")]
    public string UserName { get; set; }
    
    [JsonPropertyName("password")]
    public string UserPassword { get; set; }

    [JsonPropertyName("role")]
    public string UserRole { get; set; }
    

}



