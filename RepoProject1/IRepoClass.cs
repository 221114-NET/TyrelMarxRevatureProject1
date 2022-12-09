using ModelProject1;


namespace RepoProject1;

public interface IRepoClass
{
    string AuthUserLogin(string username, string password);
    List<ReimbursementDataClass> GetUserReimbursements(string currentUser);
    List<ReimbursementDataClass> ManagerGetAllReimbursements();
    string ManagerUpdateReimbursement(int reimbursementID, bool reimbursementApproved);
    string NewUser(string username, string password);
    string ReimbursementRequest(string ticketType, double reimbursementAmount, string LogedInUserName);
    string UpdateUserInformation(string newUserName, string newUserPass, string currentUser);
}
