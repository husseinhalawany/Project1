using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BusinessLogic.Core;
using DataMapping.Entities;
using BusinessLogic.Model;
using TravelAPISolution.Code;
using DataMapping.Services;
using System.IO;
using BusinessLogic.Helpers;
using ManagementProject.Controllers;
using DataMapping.Enums;
using Bussinesslogic.Model;

namespace MangamentProject.Controllers
{
    public class HomeController : BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            try
            {

                if (SessionData.UserRole == UserRoles.Employee)
                {
                    Attendance attendance = AttendancesLogic.GetLastSignByUserId(SessionData.UserId);
                    if (attendance == null)
                    {
                        DateTime serverTime = DateTimeHelper.Today();
                        if (serverTime.Hour >= 12)
                            return RedirectToAction("Index", "Employees");
                        else
                            return RedirectToAction("UserSign", "Attendances");
                    }
                    List<StandUpMeetingDetails> model = StandUpMeetingsLogic.GetTodayStandUpMeeting(SessionData.UserId);
                    if (model.Count == 0)
                    {
                        return RedirectToAction("Create", "StandUpMeet");
                    }
                    return RedirectToAction("Index", "Employees");
                }
                else
                {
                    HomeIndexModel model = new HomeIndexModel();
                    model.Projects = ProjectsLogic.GetProjectlist();

                    List<Sprint> sprints = SprintsLogic.GetAllSprints();
                    model.PreviousSprint = sprints.FindLast(a => a.PreviousSprint);
                    model.CurrentSprint = sprints.Find(a => a.CurrentSprint);
                    model.FutureSprint = sprints.Find(a => a.FutureSprint);

                    model.CanCreateSprint = !sprints.Any(a => a.FutureSprint);
                    return View(model);
                }

            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Projects/GetProjectlist",

                });
                return RedirectToAction("GeneralError", "Error", new { ErrorMessage = Error.ServerNotRespond });
            }
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
    }
}