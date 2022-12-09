using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoProject1
{
    public interface IRepoClassManagerUpdateReimbursement
    {
    string ManagerUpdateReimbursement(int reimbursementID, bool reimbursementApproved);
    }
}