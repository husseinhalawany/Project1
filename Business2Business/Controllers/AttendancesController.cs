using BusinessLogic.Core;
using DataMapping.Entities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessLogic.Model;
using DataMapping.Services;
using BusinessLogic.Helpers;
using Bussinesslogic.Core;
using ManagementProject.Controllers;


namespace MangamentProject.Controllers
{
    [Authorize]
    public class AttendancesController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UsersNotSignOutList(int? pageNo)
        {
            var page = pageNo ?? 0;
            List<UserSign> userSign = new List<UserSign>();
            try
            {
                userSign = AttendancesLogic.GetAllUsersNotSignOut(page);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Attendances/UsersNotSignOutList",
                    Parameters = "pageNum =" + page.ToString()  
                });
            }
            return PartialView("UserSignList", userSign);
        }
        public ActionResult UserSign(string errorMessage = "")
         {
            SignInModel model = new SignInModel();
            try
            {
                Attendance attendance = AttendancesLogic.GetLastSignByUserId(SessionData.UserId);
                model.CurrentDateTime = DateTimeHelper.Today();
                
                model.UserName = SessionData.UserName;
                if (attendance != null)
                {
                    model.LastSignIn = (DateTime)attendance.SignInDate;
                    if (attendance.SignOutDate != null)
                        model.lastSignOut = (DateTime)attendance.SignOutDate;
                    SignHelper.PrepareSign(model);
                }
                model.Succeeded = true;
                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                  
                    model.ErrorMessage = errorMessage;
                }
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Attendances/UserSign"
                });
                model.Succeeded = false;
                model.ErrorMessage = "Failed To Sign in";
                return RedirectToAction("GeneralError", "Error", new { ErrorMessage = "Error in server Connection", parameter = "" });
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult SignIn(string latitude = "", string longitude = "")
        {
            try
            {
                Attendance attendancee = new Attendance();

                DateTime signInDate = DateTimeHelper.Today();
                if (signInDate != null)
                    signInDate = DateTimeHelper.Today();
                else
                    return RedirectToAction("UserSign", "Attendances", new { ErrorMessage = "Error in Time Server Connection", parameter = "" });

                if (!string.IsNullOrEmpty(latitude) && !string.IsNullOrEmpty(longitude) && signInDate.Hour < 12)
                {
                    Attendance attendance = new Attendance()
                    {
                        EmpUserId = SessionData.UserId,
                        SignInDate = signInDate,
                        Latitude = latitude,
                        Longitude = longitude
                    };
                    AttendancesLogic.InsertNewAttendance(attendance);
                    ActionRate actionRate = ActionRatesLogic.GetActionRateByName("Sign In");
                    EmployeePointsLogic.InsertNewEmployeePoint(new EmployeePoint()
                    {
                        ActionRateId = actionRate.Id,
                        Date = DateTime.Now,
                        UserId = attendance.EmpUserId,
                        Rate = actionRate.MaxRate
                    });
                }
                else if (signInDate.Hour >= 12)
                {
                    LogsLogic.InsertLog(new Log()
                    {
                        Message = "this User breake the client side validation and try to signin within not correct time or maybe change his system time by mistck",
                        StackTrace = "",
                        Parameters = "user id = " + SessionData.UserId + " and username = " + SessionData.UserName,
                        StoryName = "ManagementProject/Attendances/SignIn"
                    });
                    return RedirectToAction("UserSign", "Attendances", new { errorMessage = "Your Time System is not Correct!" });
                }
                else if (string.IsNullOrEmpty(latitude) || string.IsNullOrEmpty(longitude))
                {
                    LogsLogic.InsertLog(new Log()
                    {
                        Message = "this User breake the client side validation and try to signin within not correct geoLocation or maybe something was wrong happened!",
                        StackTrace = "",
                        Parameters = "user id = " + SessionData.UserId + " and username = " + SessionData.UserName,
                        StoryName = "SignIn"
                    });
                    return RedirectToAction("UserSign", "Attendances", new { errorMessage = "Please check Location Permissions to allow to Signin!" });
                }
                List<StandUpMeetingDetails> model = StandUpMeetingsLogic.GetTodayStandUpMeeting(SessionData.UserId);
                if (model.Count == 0)
                {
                    return RedirectToAction("Create", "StandUpMeet");
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Attendances/SignIn"
                });
                return RedirectToAction("GeneralError", "Error", new { ErrorMessage = Error.ServerNotRespond });
            }
        }
        public ActionResult SignOut()
        {
            try
            {
                Attendance attendance = AttendancesLogic.GetLastSignByUserId(SessionData.UserId);
                AttendancesLogic.UpdateAttendance(attendance);
                ActionRate actionRate = ActionRatesLogic.GetActionRateByName("Sign Out");
                EmployeePointsLogic.InsertNewEmployeePoint(new EmployeePoint()
                {
                    ActionRateId = actionRate.Id,
                    Date = DateTimeHelper.Today(),
                    UserId = attendance.EmpUserId,
                    Rate = actionRate.MaxRate
                });

                return RedirectToAction("UserSign");
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Attendances/SignOut"
                });
                return RedirectToAction("GeneralError", "Error", new { ErrorMessage = Error.ServerNotRespond });
            }
        }
        [AllowAnonymous]
        public ActionResult SetSignOut(int id)
        {
            try
            {
                AttendancesLogic.UpdateAttendance(id);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Attendances/SetSignOut",
                    Parameters = "id= " + id.ToString()
                });
            }
            return RedirectToAction("Index");
            
        }
        public ActionResult SetSignOutToAll()
        {
            try
            {
                AttendancesLogic.SignOutToAllUsers();
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Attendances/SetSignOutToAll"
                });
            }
            return RedirectToAction("Index");

        }

    }
}