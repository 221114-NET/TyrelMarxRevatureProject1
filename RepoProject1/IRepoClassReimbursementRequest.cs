using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoProject1
{
    public interface IRepoClassReimbursementRequest
    {
    string ReimbursementRequest(string ticketType, double reimbursementAmount, string LogedInUserName, string description);
    }
}