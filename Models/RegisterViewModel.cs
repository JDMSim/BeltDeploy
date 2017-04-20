using System;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class RegisterUserViewModel
    {
        [DisplayAttribute(Name = "First Name")]
        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }
        
        [DisplayAttribute(Name = "First Name")]
        [Required]
        [MinLength(2)]
        public string LastName { get; set; }
 
        [Required]
        [EmailAddress]
        public string Email { get; set; }
 
        [Required]
        [MinLength(8)]        
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DisplayAttribute(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password does not match...")]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string ConfrimPassword { get; set; }
    }
}