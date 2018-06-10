using BusinessLogic.Core;
using BusinessLogic.Model;
using DataMapping.Entities;
using MangamentProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagementProject.Controllers
{
    public class EmployeesController : BaseController
    {
         [Authorize]
        public ActionResult Index()
        {
            return View(SessionData);
        }
        public ActionResult OnlineUser()
        {
            OnlineUserModel model = new OnlineUserModel();
            try
            {
                model = EmployeesLogic.GetOnlineUsers();
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Employees/OnlineUser"                    
                });
            }
            return View(model);
        }

        public ActionResult MonthlyBirthdate()
        {
            var model = EmployeesLogic.GetMonthlyBirthday();
            return View(model);
        }
    }
}
