using System.Collections.Generic;

namespace Domain
{
    public class Agent
    {
        public long AgentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICollection<ClaimAssignment> ClaimAssignments { get; set; }
    }
}
