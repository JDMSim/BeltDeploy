using System;
using System.ComponentModel.DataAnnotations;

namespace ProfessionalProfile.Models
{
    public class RegisterUserViewModel
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
 
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

        [RequiredAttribute]
        public string Description { set; get; }
    }
}