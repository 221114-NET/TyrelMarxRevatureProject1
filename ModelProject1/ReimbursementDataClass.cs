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
        string? ReimbursementType { get; set; }
        double ReimbursementAmount { get; set; }
        public string ReimbursementDescription { get; set; }
        bool ReimbursementApproved { get; set; }
        bool ReimbursementPendingStatus { get; set; }
    }

    public class ReimbursementDataClass : IReimbursementDataClass
    {
        [JsonPropertyName("reimbursementid")]
        public int ReimbursementID { get; set; }

        [JsonPropertyName("reimbursementtype")]
        public string? ReimbursementType { get; set; }

        [JsonPropertyName("reimbursementamount")]
        public double ReimbursementAmount { get; set; }

        [JsonPropertyName("reimbursementdescription")]
        public string ReimbursementDescription { get; set; }

        [JsonPropertyName("reimbursementapproved")]
        public bool ReimbursementApproved { get; set; }

        [JsonPropertyName("reimbursementpendingstatus")]
        public bool ReimbursementPendingStatus { get; set; } = true;

        public ReimbursementDataClass()
        {
        }
        public ReimbursementDataClass(int reimbursementID, string? ticketType, double reimbursementAmount,string reimbursementDescription, bool approved, bool pendingStatus)
        {
            this.ReimbursementID = reimbursementID;
            this.ReimbursementType = ticketType;
            this.ReimbursementAmount = reimbursementAmount;
            this.ReimbursementDescription = reimbursementDescription;
            this.ReimbursementApproved = approved;
            this.ReimbursementPendingStatus = pendingStatus;
        }
    }

}