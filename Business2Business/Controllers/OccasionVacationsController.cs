using BusinessLogic.Core;
using DataMapping.Entities;
using MangamentProject.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagementProject.Controllers
{
    public class OccasionVacationsController : BaseController
    {
        //
        // GET: /OccasionVacations/
        public ActionResult Index(VacationYear vacationYear)
            {

            OccasionVacation occassionVacation = new OccasionVacation() {
                VacationYearId = vacationYear.Id,
                VacationYearStatusId = vacationYear.VacationYearStatusId,
                StartDate=(vacationYear.StartDate).ToString("yyyy/MM/dd"),
                EndDate = (vacationYear.EndDate).ToString("yyyy/MM/dd")
            };
            return View(occassionVacation);
        }
        public ActionResult OccasionVacationsList(int? pageNumber, int vacationYearId,string startDate)
        {
            var page = pageNumber ?? 0;
            List<OccasionVacation> occasionVacationsList = new List<OccasionVacation>();
            try
            {
                occasionVacationsList = OccasionVacationsLogic.GetOccasionVacationsList(page, vacationYearId, startDate);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagmentProject/OccasionVacations/Index",
                    Parameters = "pageNumber = " + page +" & VacationYearId = " + vacationYearId
                });
            }
            return PartialView("OccasionVacationsList", occasionVacationsList);
        }
        public ActionResult Create(OccasionVacation occationVacation,int vacationId=0)
        {
            if (occationVacation .VacationYearId< 1)
            {
                return RedirectToAction("Current", "VacationYears");
            }
            return View(occationVacation);
        }
        [HttpPost]
        public ActionResult Create(OccasionVacation model = null)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreatedByUserId = SessionData.UserId;
                    if(!OccasionVacationsLogic.InsertNewOccasionVacation(model))
                    {
                        return View(model);
                    }
                }
                catch (Exception e)
                {
                    LogsLogic.InsertLog(new Log()
                    {
                        Message = e.Message,
                        StackTrace = e.StackTrace,
                        StoryName = "ManagmentProject/OccasionVacations/Create(Post)",
                    });
                }
            }
            return RedirectToAction("Index", VacationYearsLogic.GetVacationYear(model.VacationYearId));
        }

        public ActionResult Edit(int id,string startDate,string endDate)
        {
            OccasionVacation model = new OccasionVacation();
            try
            {
                    model = OccasionVacationsLogic.GetOccasionVacationById(id);
                    model.StartDate = startDate;
                    model.EndDate = endDate;
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagmentProject/OccasionVacations/Edit(Get)",
                    Parameters = "id=" + id
                });
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(OccasionVacation model)
        {
            model.ErrorMessage = "Item Not Update Please Try Again!";
            if (ModelState.IsValid)
            {
                try
                {
                    bool isUpdate = OccasionVacationsLogic.UpdateOccasionVacation(model);
                    if (isUpdate)
                    {
                        return RedirectToAction("Index", VacationYearsLogic.GetVacationYear(model.VacationYearId));
                    }
                }
                catch (Exception e)
                {
                    LogsLogic.InsertLog(new Log()
                    {
                        Message = e.Message,
                        StackTrace = e.StackTrace,
                        StoryName = "ManagmentProject/OccasionVacations/Edit(Post)"
                    });
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int vacationYearId,int occasionVacationId)
        {
            try
            {
                OccasionVacationsLogic.DeleteOccasionVacation(occasionVacationId);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagmentProject/OccasionVacations/Delete",
                    Parameters = "id=" + vacationYearId
                });
            }
            return RedirectToAction("Index",VacationYearsLogic.GetVacationYear(vacationYearId) );///////////////////
        }

        
    }
}
