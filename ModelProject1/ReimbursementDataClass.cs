using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelProject1
{
    public class ReimbursementDataClass
    {
        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("tickettype")]
        public string? TicketType { get; set; }

        [JsonPropertyName("reimbursementammount")]
        public double ReimbursementAmmount { get; set; }

        [JsonPropertyName("approvedorrejected")]
        public string? ApprovedOrRejected { get; set; }

        [JsonPropertyName("pendingstatus")]
        public bool PendingStatus { get; set; } = true;

        public ReimbursementDataClass(string? ticketType, double reimbursementAmmount, string? approvedOrRejected, bool pendingStatus)
        {
            this.TicketType = ticketType;
            this.ReimbursementAmmount = reimbursementAmmount;
            this.ApprovedOrRejected = approvedOrRejected;
            this.PendingStatus = pendingStatus;
        }
        public ReimbursementDataClass(string? userName, string? ticketType, double reimbursementAmmount, string? approvedOrRejected, bool pendingStatus)
        {
            this.Username = userName;
            this.TicketType = ticketType;
            this.ReimbursementAmmount = reimbursementAmmount;
            this.ApprovedOrRejected = approvedOrRejected;
            this.PendingStatus = pendingStatus;
        }
    }

}