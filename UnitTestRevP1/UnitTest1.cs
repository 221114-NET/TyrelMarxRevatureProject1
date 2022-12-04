namespace UnitTestRevP1;

using System.Collections.Generic;
using BusinessProject1;
using ModelProject1;
using RepoProject1;


public class UnitTest1
{
    internal class testRepo : IRepoClass
    {
        public string AuthUserLogin(string username, string password)
        {
            throw new NotImplementedException();
        }

        public List<ReimbursementDataClass> GetUserReimbursements(string currentUser)
        {
            throw new NotImplementedException();
        }

        public List<ReimbursementDataClass> ManagerGetAllReimbursements()
        {
            throw new NotImplementedException();
        }

        public string ManagerUpdateReimbursement(int reimbursementID, bool reimbursementApproved)
        {
            throw new NotImplementedException();
        }

        public string NewUser(string username, string password)
        {
            return "Login Successful";
        }

        public string ReimbursementRequest(string ticketType, double reimbursementAmount, string LogedInUserName)
        {
            throw new NotImplementedException();
        }

        public string UpdateUserInformation(string newUserName, string newUserPass, string currentUser)
        {
            throw new NotImplementedException();
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
        BusinessLayerClass test = new BusinessLayerClass(new testRepo());
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
        BusinessLayerClass test = new BusinessLayerClass(new testRepo());
        var result = test.NewUser(value1, value2);
        Assert.Contains("Login Successful", result);
    }

    [Theory]
    [InlineData("admin", "admin")]
    [InlineData("test", "test")]
    [InlineData("user", "user")]
    public void AuthUserLoinTestValid(string value1, string value2)
    {
        BusinessLayerClass test = new BusinessLayerClass(new RepoClass());
        var result = test.AuthUserLogin(value1, value2);
        Assert.DoesNotContain("User not found", result);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("user1", "")]
    [InlineData("", "pass1")]
    [InlineData("user1@test.com", "pass1")]

    public void AuthUserLoinTestInvalid(string value1, string value2)
    {
        BusinessLayerClass test = new BusinessLayerClass(new RepoClass());
        var result = test.AuthUserLogin(value1, value2);
        Assert.Contains("User not found", result);
    }
}
