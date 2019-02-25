using krusing_down_the_aisle_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace krusing_down_the_aisle_backend.Controllers.Controllers
{
   [Produces("application/json")]
   [Route("api/PlusOnes")]
   public class PlusOneController : Controller
   {
      private readonly DataContext _context;

      public PlusOneController(DataContext context)
      {
         _context = context;
      }

      [HttpGet("")]
      public IEnumerable<PlusOne> GetPlusOnes()
      {
         return _context.PlusOne;
      }

      [HttpGet("{id}")]
      public async Task<IActionResult> GetPlusOne([FromRoute] int id)
      {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         var plusOne = await _context.PlusOne.Include(f => f.Food).AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
         
         if (plusOne == null)
         {
            return NotFound();
         }

         return Ok(plusOne);
      }

      [HttpPut("{id}")]
      public async Task<IActionResult> PutPlusOne([FromRoute] int id, [FromBody] PlusOne plusOne)
      {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         if (id != plusOne.Id)
         {
            return BadRequest(); //TODO: Add error message here?
         }

         _context.Entry(plusOne).State = EntityState.Modified;

         try
         {
            await _context.SaveChangesAsync();
         } catch (DbUpdateConcurrencyException)
         {
            if (!PlusOneExists(id))
               return NotFound();

            throw;
              
         }

         return NoContent();
      }

      [HttpPost]
      public async Task<IActionResult> PostPlusOne([FromBody] PlusOne plusOne)
      {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         _context.PlusOne.Add(plusOne);
         await _context.SaveChangesAsync();

         return CreatedAtAction("GetPlusOnes", new { id = plusOne.Id }, plusOne);
      }

      private bool PlusOneExists(int id)
      {
         return _context.PlusOne.Any(e => e.Id == id);
      }
   }
}
