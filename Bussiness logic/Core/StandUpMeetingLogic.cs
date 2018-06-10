using DataMapping.Services;
using System;
using System.Collections.Generic;
using DataAccess.Repositories;
using DataMapping.Entities;
using DataMapping.JSONData;
using BusinessLogic.Helpers;
using Bussinesslogic.Model;

namespace BusinessLogic.Core
{
    public class StandUpMeetingsLogic
    {
        public static List<StandUpMeetingDetails> GetStandUpMeetingList()
        {
            DateTime todayDate = DateTimeHelper.Today();
            return StandUpMeetingRepository.GetStandUpMeetingList(todayDate);
        }
        public static List<StandUpMeetingDetails> GetMissedStandUpMeetingList()
        {
            DateTime todayDate = DateTimeHelper.Today();
            return StandUpMeetingRepository.GetMissedStandUpMeetingList(todayDate);
        }
        public static StandUpMeetingDetails GetStandUpMeeting(int id)
        {
            return StandUpMeetingRepository.GetStandUpMeeting(id);
        }
        public static List<StandUpMeetingDetails> GetStandUpMeetingList(int userId)
        {
            DateTime todayDate = DateTimeHelper.Today();
            return StandUpMeetingRepository.GetStandUpMeetingList(userId, todayDate);
        }
        public static List<StandUpMeetingDetails> GetTodayStandUpMeeting(int userId)
        {
            DateTime todayDate = DateTimeHelper.Today();
            string month = todayDate.ToString("MMMM");
            return StandUpMeetingRepository.GetStandUpMeetingTodayByUserId(userId, todayDate);
        }
        public static StandUpMeetingMonthlyHistoryModel GetMonthlyHistory(int userId)
        {
            DateTime todayDate = DateTimeHelper.Today();
            StandUpMeetingMonthlyHistoryModel model = new StandUpMeetingMonthlyHistoryModel()
            {
                StandUpMeetingDetailsList = new List<StandUpMeetingDetails>()
            };
            model.StandUpMeetingDetailsList = StandUpMeetingRepository.GetStandUpMeetingForLastDaysByUserId(userId, todayDate);
            model.ViewTitleName = todayDate.ToString("MMMM") + " Stand Up Meetings";
            return model;
        }
        public static List<StandUpMeetingData> GetStandUpMeetingDataList()
        {
            DateTime todayDate = DateTimeHelper.Today();
            return StandUpMeetingRepository.GetStandUpMeetingDataList(todayDate);
        }
        public static void InsertNewStandUpMeeting(StandUpMeeting standUpMeeting)
        {
            standUpMeeting.Date = DateTimeHelper.Today();
            standUpMeeting.TotalDegree = -1;
            standUpMeeting.ReadingDegree = -1;
            standUpMeeting.TodayJobDegree = -1;
            standUpMeeting.YesterdayJobDegree = -1;
            standUpMeeting.StandUpEmployeePointId = 0;
            standUpMeeting.SuggestionEmployeePointId = 0;
            StandUpMeetingRepository.InsertNewStory(standUpMeeting);
        }
        public static void EvaluateStandUpMeeting(StandUpMeetingDetails standUpMeetingDetails)
        {
            ActionRate actionRateStandUp = ActionRatesRepositories.GetActionRateByName("Stand Up Meeting");
            EmployeePoint standUpEmployeePoint = new EmployeePoint
            {
                ActionRateId = actionRateStandUp.Id,
                Date = DateTimeHelper.Today(),
                UserId = standUpMeetingDetails.UserId,
                Rate = standUpMeetingDetails.TotalDegree
            };
            if (standUpMeetingDetails.StandUpEmployeePointId>0)
            {
                standUpEmployeePoint.Id = standUpMeetingDetails.StandUpEmployeePointId;
                EmployeePointsRepositories.UpdateEmployeePoint(standUpEmployeePoint);
            }
            else
            {
                EmployeePointsRepositories.InsertNewEmployeePoint(standUpEmployeePoint);
                standUpMeetingDetails.StandUpEmployeePointId = standUpEmployeePoint.Id;
            }

            ActionRate actionRateSuggestion = ActionRatesRepositories.GetActionRateByName("Suggestion Bonus");
            EmployeePoint suggestionEmployeePoint = new EmployeePoint
            {
                ActionRateId = actionRateSuggestion.Id,
                Date = DateTimeHelper.Today(),
                UserId = standUpMeetingDetails.UserId,
                Rate = standUpMeetingDetails.SuggestionDegree
            };
            if (standUpMeetingDetails.SuggestionEmployeePointId > 0)
            {
                suggestionEmployeePoint.Id = standUpMeetingDetails.SuggestionEmployeePointId;
                EmployeePointsRepositories.UpdateEmployeePoint(suggestionEmployeePoint);
            }
            else
            {
                EmployeePointsRepositories.InsertNewEmployeePoint(suggestionEmployeePoint);
                standUpMeetingDetails.SuggestionEmployeePointId = suggestionEmployeePoint.Id;
            }
            StandUpMeetingRepository.EvaluateStandUpMeeting(standUpMeetingDetails);
        }
    }
}
