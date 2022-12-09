using ModelProject1;

namespace BusinessProject1;

public interface IBusinessLayerClass
{
    string AuthUserLogin(string username, string password);
    List<ReimbursementDataClass> GetUserReimbursements(string currentUser);
    List<ReimbursementDataClass> ManagerGetAllReimbursements();
    string ManagerUpdateReimbursement(int reimbursementID, bool reimbursementApproved);
    string NewUser(string username, string password);
    string ReimbursementRequest(string ticketType, double reimbursementAmount, string LogedInUserName);
    string UpdateUserInformation(string newUserName, string newUserPass, string currentUser);
}

