using FamilyOrganizerBackend.Data;
using FamilyOrganizerBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamilyOrganizerBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChoreController : ControllerBase
    {
        private readonly FamilyOrganizerContext _context;

        public ChoreController(FamilyOrganizerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chore>>> GetChores()
        {
            return await _context.Chores.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Chore>> PostChore(Chore chore)
        {
            _context.Chores.Add(chore);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetChores), new { id = chore.Id }, chore);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChore(int id, Chore chore)
        {
            if (id != chore.Id)
            {
                return BadRequest();
            }
            
            _context.Entry(chore).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChoreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        private bool ChoreExists(int id)
        {
            return _context.Chores.Any(e => e.Id == id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChore(int id)
        {
            var chore = await _context.Chores.FindAsync(id);
            if (chore == null)
            {
                return NotFound();
            }

            _context.Chores.Remove(chore);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}