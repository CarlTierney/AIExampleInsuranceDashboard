using System;
using System.Collections.Generic;

namespace Domain
{
    public class Policy
    {
        public long PolicyId { get; set; }
        public string PolicyNumber { get; set; }
        public string PolicyType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PremiumAmount { get; set; }
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Claim> Claims { get; set; }
    }
}
