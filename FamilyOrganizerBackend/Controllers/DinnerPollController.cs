using FamilyOrganizerBackend.Data;
using FamilyOrganizerBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamilyOrganizerBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DinnerPollController : ControllerBase
    {
        private readonly FamilyOrganizerContext _context;

        public DinnerPollController(FamilyOrganizerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DinnerPoll>>> GetDinnerPolls()
        {
            return await _context.DinnerPolls.Include(dp => dp.Votes).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<DinnerPoll>> PostDinnerPoll(DinnerPoll dinnerPoll)
        {
            _context.DinnerPolls.Add(dinnerPoll);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDinnerPolls), new { id = dinnerPoll.Id }, dinnerPoll);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDinnerPoll(int id)
        {
            var dinnerPoll = await _context.DinnerPolls.FindAsync(id);
            if (dinnerPoll == null)
            {
                return NotFound();
            }

            _context.DinnerPolls.Remove(dinnerPoll);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id}/vote")]
        public async Task<IActionResult> Vote(int id, [FromBody] Vote vote)
        {
            var dinnerPoll = await _context.DinnerPolls.Include(dp => dp.Votes).FirstOrDefaultAsync(dp => dp.Id == id);
            if (dinnerPoll == null)
            {
                return NotFound();
            }

            var hasAlreadyVoted = dinnerPoll.Votes.Any(v => v.Voter == vote.Voter);
            if (hasAlreadyVoted)
            {
                return BadRequest("You have already voted for this poll.");
            }

            vote.DinnerPollId = id;
            dinnerPoll.Votes.Add(vote);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
