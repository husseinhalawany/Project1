using DataMapping.Entities;
using DataMapping.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataMapping.JSONData
{
    public class StandUpMeetingData : IJson, IResult, IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string YesterdayJob { get; set; }

        public string TodayJob { get; set; }

        public string YesterdayObstruction { get; set; }

        public string Reading { get; set; }

        public string Suggestion { get; set; }

        public int? UserId { get; set; }

        public DateTime? Date { get; set; }
        public string Image { get; set; }
        public int TotalDegree { get; set; }
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }

        public IJson InitByJson(string jsonObject)
        {
            return JsonConvert.DeserializeObject<StandUpMeetingData>(jsonObject);
        }
        public string ToJson()
        {
            var json = JsonConvert.SerializeObject(this);
            return json;
        }
        public IJson InitByEntity(object obj)
        {
            StandUpMeeting standUpMeeting = (StandUpMeeting)obj;
            Id = standUpMeeting.Id;
            UserId = standUpMeeting.UserId;
            YesterdayJob = standUpMeeting.YesterdayJob;
            TodayJob = standUpMeeting.TodayJob;
            YesterdayObstruction = standUpMeeting.YesterdayObstruction;
            Reading = standUpMeeting.Reading;
            Suggestion = standUpMeeting.Suggestion;
            Date = standUpMeeting.Date;
            TotalDegree = standUpMeeting.TotalDegree;
            return this;
        }
        public object ToEntity()
        {
            return new StandUpMeeting()
            {
                Id = Id,
                UserId = UserId,
                YesterdayJob = YesterdayJob,
                TodayJob = TodayJob,
                YesterdayObstruction = YesterdayObstruction,
                Reading = Reading,
                Suggestion = Suggestion,
                Date = Date,
                TotalDegree = TotalDegree
            };
        }
    }
}