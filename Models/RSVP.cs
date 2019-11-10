using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace krusing_down_the_aisle_backend.Models
{
   public class RSVP
   {
      public RSVP ()
      {

      }

      public virtual Person Person { get; set; }

      public virtual PartyMember[] PartyMembers { get; set; }
   }
}
