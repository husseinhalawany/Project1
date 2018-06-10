using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMapping.Entities;
using DataMapping.Services;
using DataMapping.JSONData;

namespace DataAccess.Repositories
{
    public class StandUpMeetingRepository
    {
        public static List<StandUpMeetingDetails> GetStandUpMeetingList(DateTime myDate)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var Meetings = (from standUpMeetings in db.StandUpMeetings
                                join userProfiles in db.UserProfiles on standUpMeetings.UserId equals userProfiles.UserId
                                where (
                                standUpMeetings.Date.Value == myDate
                                )
                                select new StandUpMeetingDetails
                                {
                                    Date = standUpMeetings.Date,
                                    Id = standUpMeetings.Id,
                                    Name = userProfiles.UserName,
                                    Reading = standUpMeetings.Reading,
                                    Suggestion = standUpMeetings.Suggestion,
                                    TodayJob = standUpMeetings.TodayJob,
                                    UserId = standUpMeetings.UserId,
                                    YesterdayJob = standUpMeetings.YesterdayJob,
                                    YesterdayObstruction = standUpMeetings.YesterdayObstruction,
                                    Image = userProfiles.ProfilePictureUrl,
                                    TotalDegree = standUpMeetings.TotalDegree,
                                    YasterdayJobDegree = standUpMeetings.YesterdayJobDegree,
                                    TodayJobDegree = standUpMeetings.TodayJobDegree,
                                    ReadingDegree = standUpMeetings.ReadingDegree,
                                    SuggestionDegree = standUpMeetings.SuggestionDegree,
                                    StandUpEmployeePointId = standUpMeetings.StandUpEmployeePointId,
                                    SuggestionEmployeePointId = standUpMeetings.SuggestionEmployeePointId
                                }).ToList();
                return Meetings;
            }
        }
        public static List<StandUpMeetingDetails> GetMissedStandUpMeetingList(DateTime myDate)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var Meetings = (from standUpMeetings in db.StandUpMeetings
                                join userProfiles in db.UserProfiles on standUpMeetings.UserId equals userProfiles.UserId
                                where (
                                standUpMeetings.Date.Value<= myDate && standUpMeetings.TotalDegree==-1 
                                )
                                select new StandUpMeetingDetails
                                {
                                    Date = standUpMeetings.Date,
                                    Id = standUpMeetings.Id,
                                    Name = userProfiles.UserName,
                                    Reading = standUpMeetings.Reading,
                                    Suggestion = standUpMeetings.Suggestion,
                                    TodayJob = standUpMeetings.TodayJob,
                                    UserId = standUpMeetings.UserId,
                                    YesterdayJob = standUpMeetings.YesterdayJob,
                                    YesterdayObstruction = standUpMeetings.YesterdayObstruction,
                                    Image = userProfiles.ProfilePictureUrl,
                                    TotalDegree = standUpMeetings.TotalDegree,
                                    YasterdayJobDegree = standUpMeetings.YesterdayJobDegree,
                                    TodayJobDegree = standUpMeetings.TodayJobDegree,
                                    ReadingDegree = standUpMeetings.ReadingDegree,
                                    SuggestionDegree = standUpMeetings.SuggestionDegree,
                                    StandUpEmployeePointId = standUpMeetings.StandUpEmployeePointId,
                                    SuggestionEmployeePointId = standUpMeetings.SuggestionEmployeePointId
                                }).ToList();
                return Meetings;
            }
        }

        public static StandUpMeetingDetails GetStandUpMeeting(int standUpMeetingId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                StandUpMeetingDetails standUpMeetingDetails = (from standUpMeetings in db.StandUpMeetings
                                                               join userProfiles in db.UserProfiles on standUpMeetings.UserId equals userProfiles.UserId
                                                               where (
                                                               standUpMeetings.Id == standUpMeetingId
                                                               )
                                                               select new StandUpMeetingDetails
                                                               {
                                                                   Date = standUpMeetings.Date,
                                                                   Id = standUpMeetings.Id,
                                                                   Name = userProfiles.UserName,
                                                                   Reading = standUpMeetings.Reading,
                                                                   Suggestion = standUpMeetings.Suggestion,
                                                                   TodayJob = standUpMeetings.TodayJob,
                                                                   UserId = standUpMeetings.UserId,
                                                                   YesterdayJob = standUpMeetings.YesterdayJob,
                                                                   YesterdayObstruction = standUpMeetings.YesterdayObstruction,
                                                                   Image = userProfiles.ProfilePictureUrl,
                                                                   TotalDegree = standUpMeetings.TotalDegree,
                                                                   YasterdayJobDegree = standUpMeetings.YesterdayJobDegree,
                                                                   TodayJobDegree = standUpMeetings.TodayJobDegree,
                                                                   ReadingDegree = standUpMeetings.ReadingDegree,
                                                                   SuggestionDegree = standUpMeetings.SuggestionDegree,
                                                                   StandUpEmployeePointId = standUpMeetings.StandUpEmployeePointId,
                                                                   SuggestionEmployeePointId = standUpMeetings.SuggestionEmployeePointId
                                                               }).FirstOrDefault();
                return standUpMeetingDetails;
            }
        }
        public static List<StandUpMeetingDetails> GetStandUpMeetingList(int userId, DateTime myDate)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var Meetings = (from standUpMeetings in db.StandUpMeetings
                                join userProfiles in db.UserProfiles on standUpMeetings.UserId equals userProfiles.UserId
                                where (
                                userProfiles.UserId == userId &&
                                standUpMeetings.Date.Value.Month == myDate.Month
                                )
                                select new StandUpMeetingDetails
                                {
                                    Date = standUpMeetings.Date,
                                    Id = standUpMeetings.Id,
                                    Name = userProfiles.UserName,
                                    Reading = standUpMeetings.Reading,
                                    Suggestion = standUpMeetings.Suggestion,
                                    TodayJob = standUpMeetings.TodayJob,
                                    UserId = standUpMeetings.UserId,
                                    YesterdayJob = standUpMeetings.YesterdayJob,
                                    YesterdayObstruction = standUpMeetings.YesterdayObstruction,
                                    Image = userProfiles.ProfilePictureUrl,
                                    TotalDegree = standUpMeetings.TotalDegree
                                }).ToList();
                return Meetings;
            }
        }
        public static List<StandUpMeetingDetails> GetStandUpMeetingForLastDaysByUserId(int userId, DateTime myDate)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {

                var Meetings = (from standUpMeetings in db.StandUpMeetings
                                join userProfiles in db.UserProfiles on standUpMeetings.UserId equals userProfiles.UserId
                                where (
                                standUpMeetings.Date.Value.Month == myDate.Month &&
                                userProfiles.UserId == userId
                                )
                                select new StandUpMeetingDetails
                                {
                                    Date = standUpMeetings.Date,
                                    Id = standUpMeetings.Id,
                                    Name = userProfiles.UserName,
                                    Reading = standUpMeetings.Reading,
                                    Suggestion = standUpMeetings.Suggestion,
                                    TodayJob = standUpMeetings.TodayJob,
                                    UserId = standUpMeetings.UserId,
                                    YesterdayJob = standUpMeetings.YesterdayJob,
                                    YesterdayObstruction = standUpMeetings.YesterdayObstruction,
                                    Image = userProfiles.ProfilePictureUrl,
                                    TotalDegree = standUpMeetings.TotalDegree
                                }).ToList();


                return Meetings.OrderByDescending(m => m.Date).ToList();

            }

        }
        public static List<StandUpMeetingDetails> GetStandUpMeetingTodayByUserId(int userId, DateTime myDate)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {

                var Meetings = (from standUpMeetings in db.StandUpMeetings
                                join userProfiles in db.UserProfiles on standUpMeetings.UserId equals userProfiles.UserId
                                where (
                                    standUpMeetings.Date.Value == myDate &&
                                    standUpMeetings.UserId == userId
                                )
                                select new StandUpMeetingDetails
                                {
                                    Date = standUpMeetings.Date,
                                    Id = standUpMeetings.Id,
                                    Name = userProfiles.UserName,
                                    Reading = standUpMeetings.Reading,
                                    Suggestion = standUpMeetings.Suggestion,
                                    TodayJob = standUpMeetings.TodayJob,
                                    UserId = standUpMeetings.UserId,
                                    YesterdayJob = standUpMeetings.YesterdayJob,
                                    YesterdayObstruction = standUpMeetings.YesterdayObstruction,
                                    Image = userProfiles.ProfilePictureUrl,
                                    TotalDegree = standUpMeetings.TotalDegree
                                }).ToList();


                return Meetings;

            }
        }
        public static List<StandUpMeetingData> GetStandUpMeetingDataList(DateTime myDate)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {

                var Meetings = (from standUpMeetings in db.StandUpMeetings
                                join userProfiles in db.UserProfiles on standUpMeetings.UserId equals userProfiles.UserId
                                where (
                                standUpMeetings.Date.Value== myDate
                                )
                                select new StandUpMeetingData
                                {
                                    Date = standUpMeetings.Date,
                                    Id = standUpMeetings.Id,
                                    Name = userProfiles.UserName,
                                    Reading = standUpMeetings.Reading,
                                    Suggestion = standUpMeetings.Suggestion,
                                    TodayJob = standUpMeetings.TodayJob,
                                    UserId = standUpMeetings.UserId,
                                    YesterdayJob = standUpMeetings.YesterdayJob,
                                    YesterdayObstruction = standUpMeetings.YesterdayObstruction,
                                    Image = userProfiles.ProfilePictureUrl,
                                    TotalDegree = standUpMeetings.TotalDegree
                                }).ToList();


                return Meetings;

            }
        }
        public static void InsertNewStory(StandUpMeeting standUpMeeting)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                db.StandUpMeetings.Add(standUpMeeting);
                db.SaveChanges();
            }
        }
        internal static bool IsUserSentStandUpMeeting(int userId, DateTime serverTime)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                StandUpMeeting standUpMeeting = db.StandUpMeetings.Where(x => x.UserId == userId && x.Date.Value.Date == serverTime.Date).FirstOrDefault();
                return standUpMeeting != null;
            }
        }
        public static void EvaluateStandUpMeeting(StandUpMeetingDetails standUpMeetingDetails)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                StandUpMeeting standUpMeeting = db.StandUpMeetings.SingleOrDefault(x => x.Id == standUpMeetingDetails.Id);
                if (standUpMeeting != null)
                {
                    standUpMeeting.YesterdayJobDegree = standUpMeetingDetails.YasterdayJobDegree;
                    standUpMeeting.TodayJobDegree = standUpMeetingDetails.TodayJobDegree;
                    standUpMeeting.ReadingDegree = standUpMeetingDetails.ReadingDegree;
                    standUpMeeting.SuggestionDegree = standUpMeetingDetails.SuggestionDegree;
                    standUpMeeting.TotalDegree = standUpMeetingDetails.TotalDegree;
                    standUpMeeting.StandUpEmployeePointId = standUpMeetingDetails.StandUpEmployeePointId;
                    standUpMeeting.SuggestionEmployeePointId = standUpMeetingDetails.SuggestionEmployeePointId;
                    db.SaveChanges();
                }
            }
        }
    }
}
