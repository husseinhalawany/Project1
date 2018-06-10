using System;

namespace DataMapping.Services
{
    public class UserBirthdayDetails
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string ProfilePictureUrl { get; set; }
        public DateTime BirthDate { get; set; }
    }
}