using System;

namespace DataMapping.Services
{
   public class OnlineUser
    {
        public int UserId { get; set; }
        public string Name { get; set; }      
        public DateTime? SignInDate { get; set; }
        public DateTime? SignOutDate { get; set; }

    }
}
