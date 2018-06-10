using BusinessLogic.Core;
using BusinessLogic.Model;
using DataMapping.Entities;
using DataMapping.Enums;
using MangamentProject.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagementProject.Controllers
{
    [Authorize]
    public class VacationYearsController : Controller
    {
        //
        // GET: /VacationYears/
        public ActionResult VacationYear(int vacationYearStatusId)
        {
            List<VacationYear> vacationYearsList = new List<VacationYear>();                                                                                                                                                                                                                                                                                          
            if (vacationYearStatusId == (int)Y.Current)
            {
                vacationYearsList.Add(VacationYearsLogic.GetCurrentVacationYear());
                vacationYearsList.FirstOrDefault().TitleName = DateTime.Now.Year;
            }
            else if (vacationYearStatusId == (int)Y.Next)
            {
                vacationYearsList.Add(VacationYearsLogic.GetNextVacationYear());
                vacationYearsList.FirstOrDefault().TitleName = DateTime.Now.AddYears(1).Year;
            }
            else
            {
                vacationYearsList = VacationYearsLogic.GetPreviousVacationYearsList();
                vacationYearsList.FirstOrDefault().TitleName = DateTime.Now.AddYears(-1).Year;
            }
            return View("Index", vacationYearsList);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                VacationYearsLogic.DeleteVacationYear(id);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagmentProject/VacationTypes/Delete",
                    Parameters = "id=" + id
                });
            }
            return RedirectToAction("Current");
        }
    }
}
