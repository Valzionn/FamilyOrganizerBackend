using FamilyOrganizerBackend.Data;
using FamilyOrganizerBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamilyOrganizerBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalendarEventController : ControllerBase
    {
        private readonly FamilyOrganizerContext _context;

        public CalendarEventController(FamilyOrganizerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalendarEvent>>> GetCalendarEvents()
        {
            return await _context.CalendarEvents.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<CalendarEvent>> PostCalendarEvent(CalendarEvent calendarEvent)
        {
            _context.CalendarEvents.Add(calendarEvent);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCalendarEvents), new { id = calendarEvent.Id }, calendarEvent);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalendarEvent(int id)
        {
            var calendarEvent = await _context.CalendarEvents.FindAsync(id);
            if (calendarEvent == null)
            {
                return NotFound();
            }

            _context.CalendarEvents.Remove(calendarEvent);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}