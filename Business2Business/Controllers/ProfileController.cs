using System;
using System.Web.Mvc;
using BusinessLogic.Core;
using DataMapping.Services;
using BusinessLogic.Model;
using System.Web.Security;
using WebMatrix.WebData;
using DataMapping.Entities;
using System.Web.Script.Serialization;
using BusinessLogic.Helpers;
namespace ManagementProject.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        public ActionResult EditProfile()
        {

            UserProfile model = new UserProfile();
            try
            {
                model = UserProfilesLogic.GetUserByUserName(SessionData.UserName);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagmentProject/Profile/EditProfile"
                });
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult EditProfile(UserProfile model, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int day = Int16.Parse(collection["day"]);
                    int month = Int16.Parse(collection["month"]);
                    int year = Int16.Parse(collection["year"]);

                    model.BirthDate = new DateTime(year, month, day);
                    model.ProfilePictureUrl = SessionData.ImgUrl;                    
                    UserProfilesLogic.UpdateUserProfile(model);
                    SessionData.profileImageUrl = model.ProfilePictureUrl;
                    SessionData.ImgUrl = null;
                    return RedirectToAction("Index","Home");
                }
                model = UserProfilesLogic.GetUserByUserName(SessionData.UserName);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagmentProject/Profile/EditProfile(Post)",
                    Parameters = "UserProfile=" + new JavaScriptSerializer().Serialize(model) + "&Collection=" + new JavaScriptSerializer().Serialize(collection)
                });
            }
            return View(model);
        }
        public ActionResult ChangePassword()
        {

            ChangePasswordDetails password = new ChangePasswordDetails();
            try
            {
                password = EmployeesLogic.GetChangePasswordModel(SessionData.UserId);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagmentProject/Profile/ChangePassword"
                });
                return RedirectToAction("GeneralError", "Error", new { ErrorMessage = Error.ServerNotRespond });
            }
            return PartialView(password);
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordDetails changePasswordModel)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(changePasswordModel.UserName, changePasswordModel.OldPassword))
                {
                    WebSecurity.ChangePassword(changePasswordModel.UserName, changePasswordModel.OldPassword, changePasswordModel.NewPassword);
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("OldPassword", "wrong password");
            }
            return View("ChangePassword", changePasswordModel);
        }
    }
}