using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace krusing_down_the_aisle_backend.Models
{
   public partial class WeddingParty
   {
      public WeddingParty()
      {

      }

      public int Id { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string Spot { get; set; }
      public string About { get; set; }
      public string Picture { get; set; }
      public bool IsBridal { get; set; }
   }
}
