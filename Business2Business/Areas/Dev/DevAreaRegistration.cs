﻿using System.Web.Mvc;

namespace ManagementProject.Areas.Dev
{
    public class DevAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Dev";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Dev_default",
                "Dev/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces:new[]{"MangamentProject.Areas.Dev.Controllers"}
            );
        }
    }
}
