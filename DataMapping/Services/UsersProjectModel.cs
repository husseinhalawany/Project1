namespace DataMapping.Services
{
    public class UsersProjectModel
    {        
        public int roleId { get; set; }
        public int UserId { get; set; }
        public int CreatorId { get; set; }        
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }


    }
}
