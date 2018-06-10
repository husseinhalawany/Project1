using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagementProject.Controllers
{
    [Authorize]
    public class DateController : Controller
    {
        public ActionResult DaysList(int days)
        {
            return PartialView("DaysListPartial", days);
        }

    }
}
