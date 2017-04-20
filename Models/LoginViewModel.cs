using System;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class LoginUserViewModel
    {
 
        [Required]
        [Display(Name="Email")]
        [EmailAddress]
        public string LogEmail { get; set; }
 
        [Required]
        [Display(Name = "Password")]
        [MinLength(8)]        
        [DataType(DataType.Password)]
        public string LogPassword { get; set; }
    }
}