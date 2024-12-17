using FamilyOrganizerBackend.Data;
using FamilyOrganizerBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamilyOrganizerBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingItemController : ControllerBase
    {
        private readonly FamilyOrganizerContext _context;

        public ShoppingItemController(FamilyOrganizerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingItem>>> GetShoppingItems()
        {
            return await _context.ShoppingItems.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingItem>> PostShoppingItem(ShoppingItem shoppingItem)
        {
            _context.ShoppingItems.Add(shoppingItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetShoppingItems), new { id = shoppingItem.Id }, shoppingItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingItem(int id, ShoppingItem shoppingItem)
        {
            if (id != shoppingItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(shoppingItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingItemExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingItem(int id)
        {
            var shoppingItem = await _context.ShoppingItems.FindAsync(id);
            if (shoppingItem == null)
            {
                return NotFound();
            }

            _context.ShoppingItems.Remove(shoppingItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShoppingItemExists(int id)
        {
            return _context.ShoppingItems.Any(e => e.Id == id);
        }
    }
}