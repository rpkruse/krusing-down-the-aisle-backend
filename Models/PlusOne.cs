using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace krusing_down_the_aisle_backend.Models
{
   public class PlusOne
   {
      public PlusOne()
      {

      }

      public int Id { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public bool HasAllergy { get; set; }
      public string Allergy { get; set; }
      public int FoodId { get; set; }
      public int PersonId { get; set; }

      public virtual Food Food { get; set; }
      public virtual Person Person { get; set; }
   }
}
