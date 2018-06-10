using DataMapping.Entities;
using DataMapping.Interfaces;
using Newtonsoft.Json;
using System;

namespace DataMapping.JSONData
{
    public class UserData : IJson, IResult, IEntity
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string ProfilePictureUrl { get; set; }
        public bool Status { get; set; }
        public double CurrentSalary { get; set; }
        public double PreviousSalary { get; set; }
        public int RoleId { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime? SignInDate { get; set; }
        public DateTime? SignOutDate { get; set; }
        public bool IsSignedIn { get; set; }
        public bool IsSentStandUpMeeting { get; set; }
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }

        public IJson InitByJson(string jsonObject)
        {
            return JsonConvert.DeserializeObject<UserData>(jsonObject);
        }
        public string ToJson()
        {
            var json = JsonConvert.SerializeObject(this);
            return json;
        }
        public IJson InitByEntity(object obj)
        {
            UserProfile userProfile = (UserProfile)obj;
            Id = userProfile.UserId;
            UserName = userProfile.UserName;
            FirstName = userProfile.FirstName;
            LastName = userProfile.LastName;
            Email = userProfile.Email;
            BirthDate = userProfile.BirthDate;
            Phone1 = userProfile.Phone1;
            Phone2 = userProfile.Phone2;
            Address = userProfile.Address;
            ProfilePictureUrl = userProfile.ProfilePictureUrl;
            return this;
        }
        public object ToEntity()
        {
            return new UserProfile()
            {
                UserId = Id,
                UserName = UserName,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                BirthDate = BirthDate,
                Phone1 = Phone1,
                Phone2 = Phone2,
                Address = Address,
                ProfilePictureUrl = ProfilePictureUrl,
            };
        }
    }
}