using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RepoProject1;

namespace BusinessProject1
{
    public class BusinessLayerClassReimbursementRequest : IBusinessLayerClassReimbursementRequest
    {
    private readonly IRepoClassReimbursementRequest _IRepoClassReimbursementRequest;

    public BusinessLayerClassReimbursementRequest(IRepoClassReimbursementRequest irepoClassReimbursementRequest)
    {
        _IRepoClassReimbursementRequest = irepoClassReimbursementRequest;
    }
        public string ReimbursementRequest(string ticketType, double reimbursementAmount, string LogedInUserName)
    {
        return _IRepoClassReimbursementRequest.ReimbursementRequest(ticketType, reimbursementAmount, LogedInUserName);
    }
    }
}