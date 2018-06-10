using BusinessLogic.Core;
using BusinessLogic.Helpers;
using BusinessLogic.Model;
using DataMapping.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagementProject.Controllers
{
    [Authorize]
    public class LogsController : Controller
    {
        public ActionResult Index()
        {
            LogIndexModel logIndexModel;
            try
            {
                logIndexModel = LogsLogic.GetLogIndexModel();
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StoryName = "ManagementSystem/LogsController/Index",
                    StackTrace = e.StackTrace
                });
                logIndexModel = new LogIndexModel();
                logIndexModel.Succeeded = false;
                logIndexModel.ErrorMessage = Error.ServerNotRespond;
            }
            return View(logIndexModel);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Log log = new Log();
            try
            {
                LogsLogic.DeleteLogById(Convert.ToInt32(id));
                log.Succeeded = true;
            }
            catch (Exception e)
            {
                log.Succeeded = false;
                log.ErrorMessage = Error.FailedToDeleteData;
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    Parameters = id.ToString(),
                    StoryName = "ManagementSystem/Logs/Delete"
                });
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAllLogsWithStoryName(string storyName)
        {
            Log log = new Log();
            try
            {
                LogsLogic.DeleteLogsByStoryName(storyName);
                log.Succeeded = true;
            }
            catch (Exception e)
            {
                log.Succeeded = false;
                log.ErrorMessage = Error.FailedToDeleteData;
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    Parameters = storyName,
                    StoryName = "ManagementSystem/Logs/Delete"
                });
            }
            return RedirectToAction("Index");
        }

    }
}
