namespace UnitTestRevP1;

using System.Collections.Generic;
using BusinessProject1;
using ModelProject1;
using RepoProject1;


public class UnitTest1
{
    internal class testRepo : IRepoClassAuthUserLogin, IRepoClassNewUser
    {
        public string AuthUserLogin(string username, string password)
        {
            return "false";
        }

        public string NewUser(string username, string password)
        {
            return "Login Successful";
        }

    }
    internal class testRepoValidUserLogin : IRepoClassAuthUserLogin
    {
        public string AuthUserLogin(string username, string password)
        {
            return "true";
        }
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("user", "")]
    [InlineData("", "pass")]
    [InlineData("admin", "admin")]
    public void CreateNewUserTestInvalid(string value1, string value2)
    {
        System.Console.WriteLine(value1, value2);
        BusinessLayerClassNewUser test = new BusinessLayerClassNewUser(new testRepo());
        var result = test.NewUser(value1, value2);
        Assert.Contains("Invalid email or password", result);
    }

    [Theory]
    [InlineData($"test@test.com", "temp")]
    [InlineData($"h@t.com", "temp2")]
    [InlineData($"grilled@cheese.com", "test3")]
    public void CreateNewUserTestValid(string value1, string value2)
    {
        System.Console.WriteLine(value1, value2);
        BusinessLayerClassNewUser test = new BusinessLayerClassNewUser(new testRepo());
        var result = test.NewUser(value1, value2);
        Assert.Contains("Login Successful", result);
    }

    [Theory]
    [InlineData("admin", "admin")]
    [InlineData("test", "test")]
    [InlineData("user", "user")]
    public void AuthUserLoginTestValid(string value1, string value2)
    {
        BusinessLayerClassAuthUserLogin test = new BusinessLayerClassAuthUserLogin(new testRepoValidUserLogin());
        var result = test.AuthUserLogin(value1, value2);
        Assert.DoesNotContain("User not found", result);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("user1", "")]
    [InlineData("", "pass1")]
    [InlineData("user1@test.com", "pass1")]

    public void AuthUserLoginTestInvalid(string value1, string value2)
    {
        BusinessLayerClassAuthUserLogin test = new BusinessLayerClassAuthUserLogin(new testRepo());
        var result = test.AuthUserLogin(value1, value2);
        Assert.Contains("User not found", result);
    }
}
