using System;

namespace Domain
{
    public class ClaimAssignment
    {
        public long ClaimAssignmentId { get; set; }
        public long ClaimId { get; set; }
        public Claim Claim { get; set; }
        public long AgentId { get; set; }
        public Agent Agent { get; set; }
        public DateTime AssignedDate { get; set; }
    }
}
