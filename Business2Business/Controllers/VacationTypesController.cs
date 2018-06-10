using ManagementProject.Controllers;
using BusinessLogic.Core;
using BusinessLogic.Model;
using DataMapping.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DataMapping.Services;

namespace MangamentProject.Controllers
{
    [Authorize]
    public class VacationTypesController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult VacationTypesList(int? pageNumber)
        {
            var page = pageNumber ?? 0;
            List<VacationType> vacationsList = new List<VacationType>();
            try
            {
                vacationsList = VacationTypesLogic.GetVacationTypesList(page);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagmentProject/VacationTypes/Index",
                    Parameters = "pageNumber=" + page
                });
            }
            return PartialView(vacationsList);
        }
        public ActionResult Create()
        {
            return View(new VacationType());
        }
        [HttpPost]
        public ActionResult Create(VacationType model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.VacationLength > 0)
                    {
                        VacationTypesLogic.InsertVacationType(model);
                    }
                }
                catch (Exception e)
                {
                    LogsLogic.InsertLog(new Log()
                    {
                        Message = e.Message,
                        StackTrace = e.StackTrace,
                        StoryName = "ManagmentProject/VacationTypes/Create(Post)",
                        Parameters = new JavaScriptSerializer().Serialize(model)
                    });
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            VacationType model = new VacationType();
            try
            {
                if (id > 0)
                    model = VacationTypesLogic.GetVacationTypeById(id);
                else
                    return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagmentProject/VacationTypes/Edit",
                    Parameters = "id=" + id
                });
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(VacationType model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.VacationLength > 0)
                    {
                        VacationTypesLogic.UpdateVacationType(model);
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception e)
                {
                    LogsLogic.InsertLog(new Log()
                    {
                        Message = e.Message,
                        StackTrace = e.StackTrace,
                        StoryName = "ManagmentProject/VacationTypes/Edit(Post)"
                    });
                }
            }
            model.ErrorMessage = "Item Not Update Please Try Again!";
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                VacationTypesLogic.DeleteVacationType(id);
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
            return RedirectToAction("Index");
        }
        public ActionResult EmployeesInVacationType(int vacationTypeId)
        {
            List<EmployeeUsersDetails> model = new List<EmployeeUsersDetails>();
            try
            {
                if (vacationTypeId > 0)
                    model = VacationTypesLogic.GetEmployeesInVacationType(vacationTypeId);
                else
                    return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagmentProject/VacationTypes/GetEmployeesInVacationType",
                    Parameters = "vacationTypeId=" + vacationTypeId
                });
            }
            return View(model);
        }
    }
}