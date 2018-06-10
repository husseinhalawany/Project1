using System;

namespace DataMapping.Services
{
   public class UserSign
    {
        public int AttendanceId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public DateTime? SignInDate { get; set; }
        public string Image { get; set; }
    }
}
