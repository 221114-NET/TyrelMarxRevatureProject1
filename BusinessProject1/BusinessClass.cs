using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using ModelProject1;
using RepoProject1;

namespace BusinessProject1;

public interface IBusinessLayerClass
{
    string AuthUserLogin(string username, string password);
    List<ReimbursementDataClass> GetUserReimbursements();
    List<ReimbursementDataClass> ManagerGetAllReimbursements();
    List<ReimbursementDataClass> ManagerUpdateReimbursement();
    string NewUser(string username, string password);
    ReimbursementDataClass ReimbursementRequest(ReimbursementDataClass reimbursement);
    List<ReimbursementDataClass> UpdateUserInformation();
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

        /**string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
     @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
     @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";


        Regex re = new Regex(strRegex);
        if (re.IsMatch(user.username) && user.password.Length > 1 && user.password != null)
        {
            user.validUserData = true;
        }
        else
        {
            //if email is not valid tell user to enter valid email
            user.validUserData = false;
            return user;
        }*/

    }

    public List<ReimbursementDataClass> GetUserReimbursements()
    {
        return _repoClass.GetUserReimbursements();
    }

    public List<ReimbursementDataClass> ManagerGetAllReimbursements()
    {
        return _repoClass.ManagerGetAllReimbursements();
    }

    public List<ReimbursementDataClass> ManagerUpdateReimbursement()
    {
        return _repoClass.ManagerUpdateReimbursement();
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

    public ReimbursementDataClass ReimbursementRequest(ReimbursementDataClass reimbursement)
    {
        return _repoClass.ReimbursementRequest(reimbursement);
    }

    public List<ReimbursementDataClass> UpdateUserInformation()
    {
        return _repoClass.UpdateUserInformation();
    }
}

