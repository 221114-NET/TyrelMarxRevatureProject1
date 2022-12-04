using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelProject1
{
    public interface IReimbursementDataClass
    {
        public int ReimbursementID { get; set; }
        string? UserName { get; set; }
        string? ReimbursementType { get; set; }
        double ReimbursementAmount { get; set; }
        bool ReimbursementApproved { get; set; }
        bool ReimbursementPendingStatus { get; set; }
    }

    public class ReimbursementDataClass : IReimbursementDataClass
    {
        [JsonPropertyName("reimbursementid")]
        public int ReimbursementID { get; set; }

        [JsonPropertyName("username")]
        public string? UserName { get; set; }

        [JsonPropertyName("tickettype")]
        public string? ReimbursementType { get; set; }

        [JsonPropertyName("reimbursementamount")]
        public double ReimbursementAmount { get; set; }

        [JsonPropertyName("approvedorrejected")]
        public bool ReimbursementApproved { get; set; }

        [JsonPropertyName("pendingstatus")]
        public bool ReimbursementPendingStatus { get; set; } = true;

        public ReimbursementDataClass()
        {
        }
        public ReimbursementDataClass(string? ticketType, double reimbursementAmount, bool approved, bool pendingStatus)
        {
            this.ReimbursementType = ticketType;
            this.ReimbursementAmount = reimbursementAmount;
            this.ReimbursementApproved = approved;
            this.ReimbursementPendingStatus = pendingStatus;
        }
        public ReimbursementDataClass(int reimbursementID,string? userName, string? ticketType, double reimbursementAmount, bool approved, bool pendingStatus)
        {
            this.ReimbursementID = reimbursementID;
            this.UserName = userName;
            this.ReimbursementType = ticketType;
            this.ReimbursementAmount = reimbursementAmount;
            this.ReimbursementApproved = approved;
            this.ReimbursementPendingStatus = pendingStatus;
        }
    }

}