using DataMapping.Enums;

namespace DataMapping.Services
{
    public class UserDataSession
    {
        public string UserName { get; set; }
        public UserRoles UserRole { get; set; }
        public int UserId { get; set; }
        public string profileImageUrl { get; set; }
        public string ImgUrl { get; set; }
    }
}