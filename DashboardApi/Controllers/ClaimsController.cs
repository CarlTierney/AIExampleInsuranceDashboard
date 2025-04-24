using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClaimsController : ControllerBase
    {
        private readonly InsuranceDbContext _context;
        public ClaimsController(InsuranceDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Claim>>> GetClaims()
        {
            return await _context.Claims
                .Include(c => c.Policy)
                .Include(c => c.ClaimStatus)
                .Include(c => c.Payments)
                .Include(c => c.ClaimAssignments)
                    .ThenInclude(a => a.Agent)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Claim>> GetClaim(int id)
        {
            var claim = await _context.Claims
                .Include(c => c.Policy)
                .Include(c => c.ClaimStatus)
                .Include(c => c.Payments)
                .Include(c => c.ClaimAssignments)
                    .ThenInclude(a => a.Agent)
                .FirstOrDefaultAsync(c => c.ClaimId == id);
            if (claim == null) return NotFound();
            return claim;
        }
    }
}
