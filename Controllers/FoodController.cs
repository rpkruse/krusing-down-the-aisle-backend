using krusing_down_the_asile_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace krusing_down_the_asile_backend.Controllers
{
   [Produces("application/json")]
   [Route("api/Foods")]
   public class FoodController : Controller
   {
      private readonly DataContext _context;

      public FoodController(DataContext context)
      {
         _context = context;
      }

      [HttpGet("")]
      public IEnumerable<Food> GetFoods()
      {
         return _context.Food;
      }

      [HttpGet("{id}")]
      public async Task<IActionResult> GetFood([FromRoute] int id)
      {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         var food = await _context.Food.SingleOrDefaultAsync(m => m.Id == id);

         if (food == null)
         {
            return NotFound();
         }

         return Ok(food);
      }
   }
}

