using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfessionalProfile.Models
{
    public class User
    {   
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }

        [InverseProperty("UserFollowed")]
        public List<Network> Follower { get; set; }

        [InverseProperty("Follower")]
        public List<Network> UserFollowed { get; set; }


        public User()
        {
            Follower = new List<Network>();
            UserFollowed = new List<Network>();
        }
    }
}