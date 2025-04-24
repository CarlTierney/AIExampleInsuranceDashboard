using System.Collections.Generic;

namespace Domain
{
    public class ClaimStatus
    {
        public long ClaimStatusId { get; set; }
        public string StatusName { get; set; }
        public ICollection<Claim> Claims { get; set; }
    }
}
