using System;

namespace ProfessionalProfile.Models
{
    public class Network
    {   
        public int NetworkId { get; set; }

        public int UserFollowedId { get; set; }
        public User UserFollowed { get; set; }

        public int FollowerId { get; set; }
        public User Follower { get; set; }

        public int Status { get; set; }
    }
}