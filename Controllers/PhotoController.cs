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
   [Route("api/Photos")]
   public class PhotoController : Controller
   {
      private readonly DataContext _context;

      public PhotoController(DataContext context)
      {
         _context = context;
      }

      [HttpGet("")]
      public IEnumerable<Photo> GetPhotos()
      {
         return _context.Photo;
      }
   }
}

