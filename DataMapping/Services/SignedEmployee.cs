using System;
namespace DataMapping.Services
{
   public  class SignedEmployee
    {
        public DateTime? SignInDate { get; set; }
        public DateTime? SignOutDate { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
