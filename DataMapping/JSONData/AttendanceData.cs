using DataMapping.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapping.JSONData
{
    public class AttendanceData : IJson, IResult
    {
        public int EmpUserId { get; set; }
        public DateTime SignInDate { get; set; }
        public DateTime SignOutDate { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }

        public IJson InitByJson(string jsonObject)
        {
            return JsonConvert.DeserializeObject<AttendanceData>(jsonObject);
        }
        public string ToJson()
        {
            var json = JsonConvert.SerializeObject(this);
            return json;
        }
    }
}
