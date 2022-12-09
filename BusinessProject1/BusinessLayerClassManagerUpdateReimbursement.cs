using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RepoProject1;

namespace BusinessProject1
{
    public class BusinessLayerClassManagerUpdateReimbursement : IBusinessLayerClassManagerUpdateReimbursement
    {
        private readonly IRepoClassManagerUpdateReimbursement _IRepoClassManagerUpdateReimbursement;

        public BusinessLayerClassManagerUpdateReimbursement(IRepoClassManagerUpdateReimbursement irepoClassManagerUpdateReimbursement)
        {
            _IRepoClassManagerUpdateReimbursement = irepoClassManagerUpdateReimbursement;
        }
        public string ManagerUpdateReimbursement(int reimbursementID, bool reimbursementApproved)
        {
            return _IRepoClassManagerUpdateReimbursement.ManagerUpdateReimbursement(reimbursementID, reimbursementApproved);
        }
    }
}