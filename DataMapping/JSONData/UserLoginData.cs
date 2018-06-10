using DataMapping.Entities;
using DataMapping.Interfaces;
using Newtonsoft.Json;
using System;

namespace DataMapping.JSONData
{
    public class UserLoginData : IJson, IResult
    {

        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int SessionId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }
        public IJson InitByJson(string json)
        {
            return JsonConvert.DeserializeObject<UserLoginData>(json);
        }
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
      
    

    }
}
