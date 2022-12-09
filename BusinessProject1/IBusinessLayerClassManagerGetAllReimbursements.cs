using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelProject1;

namespace BusinessProject1
{
    public interface IBusinessLayerClassManagerGetAllReimbursements
    {
        List<ReimbursementDataClass> ManagerGetAllReimbursements();
    }
}