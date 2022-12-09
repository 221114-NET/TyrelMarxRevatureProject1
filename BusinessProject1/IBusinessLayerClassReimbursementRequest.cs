using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessProject1
{
    public interface IBusinessLayerClassReimbursementRequest
    {
    string ReimbursementRequest(string ticketType, double reimbursementAmount, string LogedInUserName);
    }
}