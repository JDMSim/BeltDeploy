using System;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
   public class Wedding
   {
       public int WeddingId { get; set; }
       public string FirstWedder { get; set; }
       public string SecondWedder { get; set; }
       public DateTime WeddingDate { get; set; }
       public string Address { get; set;}
       public int UserId { get; set; }
       public User User { get; set;}
       public List<Guest> Guests { get; set; }
       public Wedding()
       {
           Guests = new List<Guest>();
       }


   }
}