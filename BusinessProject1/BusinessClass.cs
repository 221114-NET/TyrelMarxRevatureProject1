using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using ModelProject1;
using RepoProject1;

namespace BusinessProject1;

public interface IBusinessLayerClass
{
    string AuthUserLogin(string username, string password);
    List<ReimbursementDataClass> GetUserReimbursements(string currentUser);
    List<ReimbursementDataClass> ManagerGetAllReimbursements();
    ReimbursementDataClass ManagerUpdateReimbursement(ReimbursementDataClass reimbursement);
    string NewUser(string username, string password);
    ReimbursementDataClass ReimbursementRequest(ReimbursementDataClass reimbursement, string LogedInUserName);
    List<ReimbursementDataClass> UpdateUserInformation(string currentUser);
}

public class BusinessLayerClass : IBusinessLayerClass
{
    private readonly IRepoClass _repoClass;

    public BusinessLayerClass(IRepoClass irepoClass)
    {
        _repoClass = irepoClass;
    }

    public string AuthUserLogin(string username, string password)
    {
        return _repoClass.AuthUserLogin(username, password);
    }

    public List<ReimbursementDataClass> GetUserReimbursements(string currentUser)
    {
        return _repoClass.GetUserReimbursements(currentUser);
    }


    public List<ReimbursementDataClass> ManagerGetAllReimbursements()
    {
        return _repoClass.ManagerGetAllReimbursements();
    }

    public ReimbursementDataClass ManagerUpdateReimbursement(ReimbursementDataClass reimbursement)
    {
        return _repoClass.ManagerUpdateReimbursement(reimbursement);
    }

    public string NewUser(string username, string password)
    {
        string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
     @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
     @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        Regex re = new Regex(strRegex);
        if (re.IsMatch(username) && password.Length > 1 && password != null)
        {
            return _repoClass.NewUser(username, password);
        }
        else
        {
            //if email is not valid tell user to enter valid email
            return "Invalid email or password";
        }
    }

    public ReimbursementDataClass ReimbursementRequest(ReimbursementDataClass reimbursement, string LogedInUserName)
    {
        reimbursement.Username = LogedInUserName;
        reimbursement.Approved = false;
        return _repoClass.ReimbursementRequest(reimbursement);
    }

    public List<ReimbursementDataClass> UpdateUserInformation(string currentUser)
    {
        return _repoClass.UpdateUserInformation(currentUser);
    }
}

