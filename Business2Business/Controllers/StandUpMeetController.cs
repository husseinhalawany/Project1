using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.Model;
using BusinessLogic.Core;
using DataMapping.Entities;
using DataMapping.Services;
using BusinessLogic.Helpers;
using Bussinesslogic.Model;

namespace ManagementProject.Controllers
{
    [Authorize]
    public class StandUpMeetController : BaseController
    {
        public ActionResult Index()
        {
            List<StandUpMeetingDetails> model = new List<StandUpMeetingDetails>();
            try
            {
                model = StandUpMeetingsLogic.GetStandUpMeetingList();
                List<StandUpMeetingDetails> helperList = model.Where(x => x.TotalDegree < 0).ToList();
                helperList.AddRange(model.Where(x => x.TotalDegree >= 0).OrderByDescending(x => x.TotalDegree).ToList());
                model = helperList;
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/StandUpMeeting/Index"
                });
            }
            return View(model);
        }
        public ActionResult MissedStandUpMeetings()
        {
            List<StandUpMeetingDetails> model = new List<StandUpMeetingDetails>();
            try
            {
                model = StandUpMeetingsLogic.GetMissedStandUpMeetingList();
                List<StandUpMeetingDetails> helperList = model.Where(x => x.TotalDegree < 0).ToList();
                helperList.AddRange(model.Where(x => x.TotalDegree >= 0).OrderByDescending(x => x.TotalDegree).ToList());
                model = helperList;
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/StandUpMeeting/Index"
                });
            }
            return View(model);
        }

        public ActionResult Details(int standUpMeetingId)
        {
            StandUpMeetingDetails standUpMeetingDetails = StandUpMeetingsLogic.GetStandUpMeeting(standUpMeetingId);
            return PartialView(standUpMeetingDetails);
        }
        public ActionResult History()
        {
            //Story mm = new Story();
            StandUpMeetingMonthlyHistoryModel model = new StandUpMeetingMonthlyHistoryModel();
            try
            {
                model = StandUpMeetingsLogic.GetMonthlyHistory(SessionData.UserId);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/StandUpMeeting/History"
                });
            }
            return View(model);
        }

        public ActionResult Create()
        {
            return View(new StandUpMeetingDetails());
        }
        [HttpPost]
        public ActionResult Create(StandUpMeetingDetails standUpMeetingDetails)
        {
            if (ModelState.IsValid)
            {
                StandUpMeeting model = new StandUpMeeting();
                Suggestion suggestion = new Suggestion();
                try
                {
                    standUpMeetingDetails.Name = SessionData.UserName;
                    model.Reading = standUpMeetingDetails.Reading;
                    model.Suggestion = standUpMeetingDetails.Suggestion;
                    model.TodayJob = standUpMeetingDetails.TodayJob;
                    model.UserId = SessionData.UserId;
                    model.YesterdayJob = standUpMeetingDetails.YesterdayJob;
                    model.YesterdayObstruction = standUpMeetingDetails.YesterdayObstruction;
                    standUpMeetingDetails.Date = DateTimeHelper.Today();
                    StandUpMeetingsLogic.InsertNewStandUpMeeting(model);
                    EmailHelper.SendEmail(standUpMeetingDetails);

                    if (!string.IsNullOrWhiteSpace(model.Suggestion))
                    {
                        suggestion.CreateDate = DateTimeHelper.Today();
                        suggestion.SuggestByUserId = SessionData.UserId;
                        suggestion.Title = SessionData.UserName;
                        suggestion.Description = model.Suggestion;
                        SuggestionsLogic.InsertNewSuggestion(suggestion);
                    }
                }
                catch (Exception e)
                {
                    LogsLogic.InsertLog(new Log()
                    {
                        Message = e.Message,
                        StackTrace = e.StackTrace,
                        StoryName = "ManagementProject/StandUpMeeting/Create"
                    });
                }

                return RedirectToAction("Index");
            }
            return View(standUpMeetingDetails);
        }
        [HttpPost]
        public ActionResult Evaluate(int standUpMeetingId, int standUpMeetingUserId, int yasterdayJobDegree, int todayJobDegree, int readingDegree, int suggestionDegree)
        {

            try
            {
                yasterdayJobDegree = yasterdayJobDegree > 0 ? yasterdayJobDegree : 0;
                todayJobDegree = todayJobDegree > 0 ? todayJobDegree : 0;
                readingDegree = readingDegree > 0 ? readingDegree : 0;
                suggestionDegree = suggestionDegree > 0 ? suggestionDegree : 0;
                StandUpMeetingDetails standUpMeetingDetails = new StandUpMeetingDetails()
                {
                    UserId = standUpMeetingUserId,
                    Id = standUpMeetingId,
                    YasterdayJobDegree = yasterdayJobDegree,
                    TodayJobDegree = todayJobDegree,
                    ReadingDegree = readingDegree,
                    SuggestionDegree = suggestionDegree,
                    TotalDegree = yasterdayJobDegree + todayJobDegree + readingDegree
                };
                StandUpMeetingsLogic.EvaluateStandUpMeeting(standUpMeetingDetails);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Evaluate/History"
                });
            }
            return PartialView("JavascriptRedirect", new JavascriptRedirectModel("/StandUpMeet/Index"));
        }
        public ActionResult MonthlyStandUpMeetings(int userId)
        {
            StandUpMeetingMonthlyHistoryModel model = new StandUpMeetingMonthlyHistoryModel();
            try
            {
                UserProfile user = UserProfilesLogic.GetUserById(userId);
                model.StandUpMeetingDetailsList = StandUpMeetingsLogic.GetStandUpMeetingList(userId);
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;

            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/StandUpMeeting/MonthlyStandUpMeetings"
                });
            }
            return View(model);
        }
    }
}
