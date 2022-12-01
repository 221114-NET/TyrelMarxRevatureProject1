using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using ModelProject1;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RepoProject1;

public interface IRepoClass
{
    bool AuthUserLogin(string username, string password);
    List<ReimbursementDataClass> GetUserReimbursements(string currentUser);
    List<ReimbursementDataClass> ManagerGetAllReimbursements();
    ReimbursementDataClass ManagerUpdateReimbursement(ReimbursementDataClass reimbursement);
    string NewUser(string username, string password);
    ReimbursementDataClass ReimbursementRequest(ReimbursementDataClass reimbursement);
    List<ReimbursementDataClass> UpdateUserInformation(string currentUser);
}

public class RepoClass : IRepoClass
{
    //FIXME temp storage for user data when database is added remove this
    List<UserDataClass> usersList = new List<UserDataClass>();
    List<ReimbursementDataClass> reimbursementDataList = new List<ReimbursementDataClass>();
    List<ReimbursementDataClass> reimbursementDataList2 = new List<ReimbursementDataClass>();
    //end of temp storage

    public bool AuthUserLogin(string username, string password)
    {
        //check if user and pass is in database and return true or false
        throw new NotImplementedException();
    }

    public List<ReimbursementDataClass> GetUserReimbursements(string currentUser)
    {
        //TODO pull current user reimbursements from database and remove this
        reimbursementDataList.Add(new ReimbursementDataClass("user", "car", 12.1, false, true));
        reimbursementDataList.Add(new ReimbursementDataClass("user", "boat", 14.1, false, true));
        reimbursementDataList.Add(new ReimbursementDataClass("admin", "plane", 1000.1, false, true));
        reimbursementDataList.Add(new ReimbursementDataClass("admin", "space ship", 1000000, false, true));
        //end of remove

        foreach (var item in reimbursementDataList)
        {
            if (item.Username == currentUser)
            {
                reimbursementDataList2.Add(item);
            }
        }

        return reimbursementDataList2;
    }

    public List<ReimbursementDataClass> ManagerGetAllReimbursements()
    {
        //TODO remove these and use data from database
        reimbursementDataList.Add(new ReimbursementDataClass("user", "car", 12.1, false, true));
        reimbursementDataList.Add(new ReimbursementDataClass("user", "boat", 14.1, false, true));
        reimbursementDataList.Add(new ReimbursementDataClass("admin", "plane", 1000.1, false, true));
        reimbursementDataList.Add(new ReimbursementDataClass("admin", "space ship", 1000000, false, true));
        //end of remove
        return reimbursementDataList;
    }

    public ReimbursementDataClass ManagerUpdateReimbursement(ReimbursementDataClass reimbursement)
    {
        //TODO tell the database to update the reimbursement if pendingStatus = true else throw error
        throw new NotImplementedException();
    }

    public string NewUser(string username, string password)
    {
        //TODO add new user to database and remove this
        //if user is not already in the database
        usersList.Add(new UserDataClass(username, password, "user"));
        return "User created";

    }

    public ReimbursementDataClass ReimbursementRequest(ReimbursementDataClass reimbursement)
    {
        //TODO put reimbursement request into database
        //get data from database and return it
        //if the reimbursementDataList is empty return empty list

        throw new NotImplementedException();
    }

    public List<ReimbursementDataClass> UpdateUserInformation(string currentUser)
    {
        //TODO pull current user information from database and make sure current user is the one updating the information then update user information
        throw new NotImplementedException();
    }
}
