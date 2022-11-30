using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelProject1
{
    public class ReimbursementDataClass
    {
        public string? userName { get; set; }
        public string? ticketType { get; set; }
        public double reimbursementAmmount { get; set; }
        public string? approvedOrRejected { get; set; }
        public bool pendingStatus { get; set; } = true;

        public ReimbursementDataClass(string? ticketType, double reimbursementAmmount, string? approvedOrRejected, bool pendingStatus)
        {
            this.ticketType = ticketType;
            this.reimbursementAmmount = reimbursementAmmount;
            this.approvedOrRejected = approvedOrRejected;
            this.pendingStatus = pendingStatus;
        }
        public ReimbursementDataClass(string? userName, string? ticketType, double reimbursementAmmount, string? approvedOrRejected, bool pendingStatus)
        {
            this.userName = userName;
            this.ticketType = ticketType;
            this.reimbursementAmmount = reimbursementAmmount;
            this.approvedOrRejected = approvedOrRejected;
            this.pendingStatus = pendingStatus;
        }
    }

}