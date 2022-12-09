using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelProject1;
using RepoProject1;

namespace BusinessProject1
{
    public class BusinessClassGetUserReimbursements : IBusinessLayerClassGetUserReimbursements
    {
        private readonly IRepoClassGetUserReimbursements _IRepoClassGetUserReimbursements;

        public BusinessClassGetUserReimbursements(IRepoClassGetUserReimbursements irepoClassGetUserReimbursements)
        {
            _IRepoClassGetUserReimbursements = irepoClassGetUserReimbursements;
        }
        public List<ReimbursementDataClass> GetUserReimbursements(string currentUser)
        {
            return _IRepoClassGetUserReimbursements.GetUserReimbursements(currentUser);
        }
    }
}