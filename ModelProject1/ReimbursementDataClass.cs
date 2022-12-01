using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelProject1
{
    public class ReimbursementDataClass
    {
        public string? username { get; set; }
        public string? tickettype { get; set; }
        public double reimbursementammount { get; set; }
        public string? approvedorrejected { get; set; }
        public bool pendingstatus { get; set; } = true;

        public ReimbursementDataClass(string? ticketType, double reimbursementAmmount, string? approvedOrRejected, bool pendingStatus)
        {
            this.tickettype = ticketType;
            this.reimbursementammount = reimbursementAmmount;
            this.approvedorrejected = approvedOrRejected;
            this.pendingstatus = pendingStatus;
        }
        public ReimbursementDataClass(string? userName, string? ticketType, double reimbursementAmmount, string? approvedOrRejected, bool pendingStatus)
        {
            this.username = userName;
            this.tickettype = ticketType;
            this.reimbursementammount = reimbursementAmmount;
            this.approvedorrejected = approvedOrRejected;
            this.pendingstatus = pendingStatus;
        }
    }

}