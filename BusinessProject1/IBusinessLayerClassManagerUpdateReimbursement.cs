using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessProject1
{
    public interface IBusinessLayerClassManagerUpdateReimbursement
    {
    string ManagerUpdateReimbursement(int reimbursementID, bool reimbursementApproved);
    }
}