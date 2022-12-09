using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelProject1;
using RepoProject1;

namespace BusinessProject1
{
    public class BusinessLayerClassManagerGetAllReimbursements : IBusinessLayerClassManagerGetAllReimbursements
    {
        private readonly IRepoClassManagerGetAllReimbursements _IRepoClassManagerGetAllReimbursements;

        public BusinessLayerClassManagerGetAllReimbursements(IRepoClassManagerGetAllReimbursements irepoClassManagerGetAllReimbursements)
        {
            _IRepoClassManagerGetAllReimbursements = irepoClassManagerGetAllReimbursements;
        }
        public List<ReimbursementDataClass> ManagerGetAllReimbursements()
        {
            return _IRepoClassManagerGetAllReimbursements.ManagerGetAllReimbursements();
        }
    }
}