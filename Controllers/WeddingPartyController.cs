using krusing_down_the_aisle_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace krusing_down_the_aisle_backend.Controllers
{
   [Produces("application/json")]
   [Route("api/WeddingParty")]
   public class WeddingPartyController : Controller
   {
      private readonly DataContext _context;

      public WeddingPartyController(DataContext context)
      {
         _context = context;
      }

      [HttpGet("")]
      public IEnumerable<WeddingParty> GetWeddingParty()
      {
         return _context.WeddingParty;
      }

      [HttpGet("BridalParty")]
      public async Task<IActionResult> GetBridalParty()
      {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         var bridalParty = await _context.WeddingParty.Where(m => m.IsBridal).AsNoTracking().ToListAsync();

         if (bridalParty == null)
         {
            return NotFound();
         }

         return Ok(bridalParty);
      }

      [HttpGet("GroomsParty")]
      public async Task<IActionResult> GetGroomParty()
      {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         var groomParty = await _context.WeddingParty.Where(m => !m.IsBridal).AsNoTracking().ToListAsync();

         if (groomParty == null)
         {
            return NotFound();
         }

         return Ok(groomParty);
      }
   }
}

