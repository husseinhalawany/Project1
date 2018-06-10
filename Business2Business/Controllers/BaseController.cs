using DataMapping.Services;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessLogic.Model;
using BusinessLogic.Core;
using DataMapping.Entities;
using System;
using System.Web.Security;
using DataMapping.Enums;

namespace ManagementProject.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        #region User Session
        private UserDataSession _SessionData;
        public UserDataSession SessionData
        {
            get
            {
                if (Session["UserSession"] == null)
                {
                    InitUserSession();
                }
                return (UserDataSession)Session["UserSession"];
            }
            set
            {
                _SessionData = value;
                Session["UserSession"] = _SessionData;
            }
        }
        private void InitUserSession()
        {
            try
            {
                _SessionData = new UserDataSession()
                {
                    UserName = User.Identity.Name,
                };

                DataMapping.Entities.UserProfile user = UserProfilesLogic.GetUserByUserName(User.Identity.Name);
                _SessionData.UserId = user.UserId;
                _SessionData.profileImageUrl = user.ProfilePictureUrl;
                 if (Roles.IsUserInRole("Admin"))
                {
                    _SessionData.UserRole = UserRoles.Admin;
                }
                else if (Roles.IsUserInRole("Employee"))
                {
                    _SessionData.UserRole = UserRoles.Employee;
                }
                

                Session["UserSession"] = _SessionData;
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Base/InitUserSession"
                });
            }

        }
        #endregion

        #region StoriesFilter
        private StoriesFilter _StoriesFilterSession;

        public StoriesFilter StoriesFilterSession
        {
            get
            {
                if (Session["SessionFilterStory"] == null)
                {
                    _StoriesFilterSession = new StoriesFilter()
                    {
                        Iterations = new List<Sprint>(),
                        AllStories = new List<StoriesDetails>(),
                        SprintItems=new List<Item>()
                    };
                }
                return (StoriesFilter)Session["SessionFilterStory"];

            }
            set
            {
                _StoriesFilterSession = value;
                Session["SessionFilterStory"] = _StoriesFilterSession;
            }
        }
        #endregion

    }
}