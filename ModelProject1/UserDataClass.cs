
namespace ModelProject1;


public interface IUserDataClass
{
    string username { get; set; }
    string password { get; set; }
    string role { get; set; }

}

public class UserDataClass : IUserDataClass
{
    public UserDataClass(string username, string password, string role)
    {
        this.username = username;
        this.password = password;
        this.role = role;
    }
    public string username { get; set; }
    public string password { get; set; }
    public string role { get; set; }
    

}



