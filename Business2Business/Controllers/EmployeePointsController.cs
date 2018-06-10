using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.Helpers;
using Bussinesslogic.Core;
using DataMapping.Services;

namespace ManagementProject.Controllers
{
    public class EmployeePointsController : Controller
    {
        public ActionResult BestOfMonth()
        {
            List<EmployeePointDetails> employeePoints = EmployeePointsLogic.MonthBestEmployees();
            employeePoints.FirstOrDefault().TitleName = "Best Employees Of " + DateTimeHelper.Today().ToString("MMMM");
            return View("BestEmployeesPerPeriod", employeePoints);
        }
        public ActionResult BestOfQuarter()
        {
            string quarterName="";
            List<EmployeePointDetails> employeePoints = EmployeePointsLogic.QuarterBestEmployees(out quarterName);
            employeePoints.FirstOrDefault().TitleName = "Best Employees Of " + quarterName + " Quarter";
            return View("BestEmployeesPerPeriod", employeePoints);
        }
        public ActionResult BestEmployeeOfMonthAndQuarter()
        {
            List<EmployeePointDetails> employeePoints = EmployeePointsLogic.MonthAndQuarterBestEmployees();
            return PartialView(employeePoints);
        }
    }
}