﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace krusing_down_the_aisle_backend.Models
{
   public partial class Person
   {
      public Person()
      {

      }

      public Person(string fName, string lName)
        {
          this.FirstName = fName;
          this.LastName = lName;
          this.HasRSVPD = false;
          this.PartyMembers = new List<PartyMember> { };
        }

      public int Id { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public bool HasPlusone { get; set; }
      public int FoodId { get; set; }
      public bool HasAllergy { get; set; }
      public string Allergy { get; set; }
      public bool HasRSVPD { get; set; }
      public bool CanAttend { get; set; }
    
      public List<PartyMember> PartyMembers { get; set; }

      public virtual Food Food { get; set; }
      public virtual PlusOne PlusOne { get; set; }
      //public virtual PartyMember[] PartyMembers { get; set; }
   }
}
