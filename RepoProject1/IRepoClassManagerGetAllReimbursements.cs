using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelProject1;

namespace RepoProject1
{
    public interface IRepoClassManagerGetAllReimbursements
    {
        List<ReimbursementDataClass> ManagerGetAllReimbursements();
    }
}