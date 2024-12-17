using FamilyOrganizerBackend.Data;
using FamilyOrganizerBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamilyOrganizerBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContributionController : ControllerBase
    {
        private readonly FamilyOrganizerContext _context;

        public ContributionController(FamilyOrganizerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contribution>>> GetContributions()
        {
            return await _context.Contributions.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Contribution>> PostContribution(Contribution contribution)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Contributions.Add(contribution);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetContributions), new { id = contribution.Id }, contribution);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContribution(int id)
        {
            var contribution = await _context.Contributions.FindAsync(id);
            if (contribution == null)
            {
                return NotFound();
            }

            _context.Contributions.Remove(contribution);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}