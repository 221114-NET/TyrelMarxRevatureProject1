using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelProject1;

namespace BusinessProject1
{
    public interface IBusinessLayerClassGetUserReimbursements
    {
    List<ReimbursementDataClass> GetUserReimbursements(string currentUser,TicketFilter filter);
    }
}