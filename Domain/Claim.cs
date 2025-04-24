using System;
using System.Collections.Generic;

namespace Domain
{
    public class Claim
    {
        public long ClaimId { get; set; }
        public string ClaimNumber { get; set; }
        public long PolicyId { get; set; }
        public Policy Policy { get; set; }
        public DateTime DateFiled { get; set; }
        public long ClaimStatusId { get; set; }
        public ClaimStatus ClaimStatus { get; set; }
        public decimal ClaimAmount { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public string Description { get; set; }
        public ICollection<ClaimAssignment> ClaimAssignments { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
