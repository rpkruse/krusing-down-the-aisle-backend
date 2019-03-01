using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace krusing_down_the_aisle_backend.Models
{
   public partial class Photo
   {
      public Photo()
      {

      }

      public int Id { get; set; }
      public string Img { get; set; }
      public string Desc { get; set; }
   }
}
