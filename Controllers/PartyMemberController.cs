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
   [Route("api/PartyMembers")]
   public class PartyMemberController : Controller
   {
      private readonly DataContext _context;

      public PartyMemberController(DataContext context)
      {
         _context = context;
      }

      [HttpGet("")]
      public IEnumerable<PartyMember> GetPartyMembers()
      {
         return _context.PartyMember;
      }

      [HttpGet("{id}")]
      public async Task<IActionResult> GetPartyMember([FromRoute] int id)
      {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         var partyMember = await _context.PartyMember.Include(f => f.Food).AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
         
         if (partyMember == null)
         {
            return NotFound();
         }

         return Ok(partyMember);
      }

      [HttpPut("{id}")]
      public async Task<IActionResult> PutPartyMember([FromRoute] int id, [FromBody] PartyMember partyMember)
      {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         if (id != partyMember.Id)
         {
            return BadRequest(); //TODO: Add error message here?
         }

         _context.Entry(partyMember).State = EntityState.Modified;

         try
         {
            await _context.SaveChangesAsync();
         } catch (DbUpdateConcurrencyException)
         {
            if (!PartyMemberExists(id))
               return NotFound();

            throw;
              
         }

         return NoContent();
      }

      [HttpPost]
      public async Task<IActionResult> PostPartyMember([FromBody] PartyMember partyMember)
      {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         _context.PartyMember.Add(partyMember);
         await _context.SaveChangesAsync();

         return CreatedAtAction("GetPartyMember", new { id = partyMember.Id }, partyMember);
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> DeletePartyMember([FromRoute] int id)
      {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         PartyMember pm = _context.PartyMember.SingleOrDefault(p => p.Id == id);

         if (pm == null)
            return NotFound();

         _context.PartyMember.Remove(pm);
         await _context.SaveChangesAsync();

         return Ok(pm);
      }

      private bool PartyMemberExists(int id)
      {
         return _context.PartyMember.Any(e => e.Id == id);
      }
   }
}
