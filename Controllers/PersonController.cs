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
   [Route("api/Persons")]
   public class PersonController : Controller
   {
      private readonly DataContext _context;

      public PersonController(DataContext context)
      {
         _context = context;
      }

      [HttpGet("")]
      public IEnumerable<Person> GetPersons()
      {
         return _context.Person;
      }

      [HttpGet("FoodDetails")]
      public IEnumerable<Person> GetPersonsFoodDetials()
      {
         return _context.Person.Include(f => f.Food).AsNoTracking();
      }

      [HttpGet("PlusOneDetails")]
      public IEnumerable<Person> GetPersonsPlusOneDetials()
      {
         return _context.Person.Include(p => p.PlusOne).ThenInclude(f => f.Food).AsNoTracking();
      }

      [HttpGet("{id}")]
      public async Task<IActionResult> GetPerson([FromRoute] int id)
      {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         var person = await _context.Person.Include(f => f.Food).AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);

         if (person == null)
         {
            return NotFound();
         }

         

         if (person.PlusOneId > 0)
            person.PlusOne = await _context.PlusOne
                                          .Include(f => f.Food)
                                          .AsNoTracking()
                                          .SingleOrDefaultAsync(po => po.Id == person.PlusOneId);

         return Ok(person);
      }

      [HttpGet("Lookup")]
      public async Task<IActionResult> GetPersonLookup([FromBody] string fullName)
      {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         var names = fullName.Split(" ");

         if (names.Length < 2)
         {
            ModelState.AddModelError("Error", "Please enter first name followed by a space and your last name only");
            return BadRequest(ModelState);
         }

         var person = await _context.Person.FirstOrDefaultAsync(p => p.FirstName.Equals(names[0]) && p.LastName.Equals(names[1]));

         if (person == null)
         {
            ModelState.AddModelError("Error", string.Format("Unable to find RSVP with name {0} {1}", names[0], names[1]));
            return NotFound(ModelState);
         }

         return Ok(person);
      }

      [HttpPut("{id}")]
      public async Task<IActionResult> PutPerson([FromRoute] int id, [FromBody] Person person)
      {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         if (id != person.Id)
         {
            return BadRequest(); //TODO: Add error message here?
         }

         _context.Entry(person).State = EntityState.Modified;

         try
         {
            await _context.SaveChangesAsync();
         } catch (DbUpdateConcurrencyException)
         {
            if (!PersonExists(id))
               return NotFound();

            throw;
              
         }

         return NoContent();
      }

      [HttpPost]
      public async Task<IActionResult> PostPerson([FromBody] Person person)
      {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         _context.Person.Add(person);
         await _context.SaveChangesAsync();

         return CreatedAtAction("GetPerson", new { id = person.Id }, person);
      }

      private bool PersonExists(int id)
      {
         return _context.Person.Any(e => e.Id == id);
      }
   }
}
