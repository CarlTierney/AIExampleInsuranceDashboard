using System;

namespace Domain
{
    public class Payment
    {
        public long PaymentId { get; set; }
        public long ClaimId { get; set; }
        public Claim Claim { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }
    }
}
