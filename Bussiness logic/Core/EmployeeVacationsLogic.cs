using System;
using System.Collections.Generic;
using DataMapping.Entities;
using DataAccess.Repositories;
using BusinessLogic.Helpers;
using BusinessLogic.Model;
using DataMapping.Services;
using System.Web.Script.Serialization;
using DataMapping.Enums;

namespace BusinessLogic.Core
{
    public class EmployeeVacationsLogic
    {
        public static List<EmplyeeVacationDetails> GetEmployeeVacationsList(int page, int statusId, int userId)
        {
            int takeCount = Config.PageItemCount();
            int skipCount = page * takeCount;
            return EmployeeVacationsRepositories.GetAllEmployeeVacationsList(userId, statusId, skipCount, takeCount, DateTimeHelper.Today());
        }
        public static List<EmplyeeVacationDetails> GetUsersVacationsList(int page, int statusId)
        {
            int takeCount = Config.PageItemCount();
            int skipCount = page * takeCount;
            DateTime toDay = DateTimeHelper.Today();
            return EmployeeVacationsRepositories.GetAllUsersVacationsList(statusId, toDay, skipCount, takeCount);
        }
        public static List<EmplyeeVacationDetails> GetUsersVacationsList(int page, int statusId, int userId)
        {
            int takeCount = Config.PageItemCount();
            int skipCount = page * takeCount;
            DateTime toDay = DateTimeHelper.Today();
            return EmployeeVacationsRepositories.GetAllUsersVacationsList(userId, statusId, toDay, skipCount, takeCount);
        }
        public static EmployeeVacationsIndexModel GetEmployeeVacationsIndexModel(int? statusId, int? userId)
        {
            EmployeeVacationsIndexModel model = new EmployeeVacationsIndexModel();
            model.VacationStatusList = EmployeeVacationsRepositories.GetAllVacationStatus();
            model.EmployeeUsersList = UserProfilesRepository.GetNotAdminUsers();
            model.StatusId = statusId ?? 0;
            model.EmployeeUserId = userId ?? 0;
            return model;
        }
        public static CreateEmployeeVacationModel CreateEmployeeVacationRequest(int userId)
        {
            CreateEmployeeVacationModel model = new CreateEmployeeVacationModel();
            model.EmployeeVacation = new EmployeeVacation()
            {
                EmployeeUserId = userId,
                StartDate = DateTimeHelper.Today(),
                EndDate = DateTimeHelper.Today()
            };
            model.VacationTypesList = VacationTypesRepositories.GetVacationTypesExceptUnpaid();
            return model;
        }
        public static void CreateEmployeeVacationRequest(CreateEmployeeVacationModel model)
        {
            model.Succeeded = false;
            if (model.EmployeeVacation.StartDate.Date.CompareTo(DateTimeHelper.Today().Date) < 0 && !model.IsAdmin)
            {
                model.ErrorMessage = "Start Date Must Not be Less than Today";
                return;
            }
            if (model.EmployeeVacation.EndDate.Date.CompareTo(DateTimeHelper.Today().Date) < 0 && !model.IsAdmin)
            {
                model.ErrorMessage = "End Date Must be Greater than Today";
                return;
            }
            if (model.EmployeeVacation.EndDate.CompareTo(model.EmployeeVacation.StartDate) < 0)
            {
                model.ErrorMessage = "End Date Must be Later than Start Date";
                return;
            }
            if (model.EmployeeVacation.StartDate.Year != model.EmployeeVacation.EndDate.Year)
            {
                model.ErrorMessage = "End Date Must be in same Start Date Year.";
                return;
            }

            model.EmployeeVacation.StatusId = model.IsAdmin ? (int)EVacationStatus.Approved : (int)EVacationStatus.Pending;
            model.VacationTypesList = VacationTypesRepositories.GetVacationTypesExceptUnpaid();
            model.EmployeeVacation.VacationDays = GetRequestedVacationDays(model.EmployeeVacation.StartDate, model.EmployeeVacation.EndDate);
            if (model.EmployeeVacation.VacationDays < 1)
            {
                model.ErrorMessage = "Requested Vacation Period is already Vacation in our system ;).";
                return;
            }
            if (!IsRequestedVacationDaysValid(model))
            {
                model.ErrorMessage = "Requested Vacation Days Must be less than Remaining Days Of This Vacation.";
                return;
            }
            if (IsVacationDaysTakenBefore(model))
            {
                model.ErrorMessage = "There are days or part of them was requested before";
                return;
            }

            EmployeeVacationsRepositories.InsertNewEmployeeVacation(model.EmployeeVacation);
            //EmailHelper.VacationSendEmail(model, SessionData.UserName);
            model.Succeeded = true;
        }
        public static bool IsRequestedVacationDaysValid(CreateEmployeeVacationModel model)
        {
            int vacationMaxDays = VacationTypesRepositories.GetVacationTypeById(model.EmployeeVacation.VacationTypeId).VacationLength;
            int userVacationDays = EmployeeVacationsRepositories.GetUserVacationDays(model.EmployeeVacation.EmployeeUserId, model.EmployeeVacation.VacationTypeId, model.EmployeeVacation.StartDate.Year);
            int remainingDaysOfVacation = vacationMaxDays - userVacationDays;
            return model.EmployeeVacation.VacationDays <= remainingDaysOfVacation;
        }
        public static int GetRequestedVacationDays(DateTime startDate, DateTime endDate)
        {
            int requestedVacationDays = 0;
            DateTime currentDate = startDate;
            while (currentDate <= endDate)
            {
                if (!(currentDate.DayOfWeek == DayOfWeek.Friday || currentDate.DayOfWeek == DayOfWeek.Saturday))
                {
                    requestedVacationDays++;
                }
                currentDate = currentDate.AddDays(1);
            }
            if (requestedVacationDays > 0)
            {
                requestedVacationDays -= OccasionVacationsRepositories.GetRequestOccesionVacation(startDate, endDate).Count;
            }
            return requestedVacationDays;
        }
        public static bool IsVacationDaysTakenBefore(CreateEmployeeVacationModel model)
        {
            DateTimeRange requestedDays = new DateTimeRange(model.EmployeeVacation.StartDate, model.EmployeeVacation.EndDate);
            try
            {
                List<EmployeeVacation> result = EmployeeVacationsRepositories.GetAllEmployeeVacationsByVacationTypeId(model.EmployeeVacation.VacationTypeId, model.EmployeeVacation.EmployeeUserId,DateTimeHelper.Today());
                foreach (EmployeeVacation vacation in result)
                {
                    DateTimeRange lastTakenDaysOfVacation = new DateTimeRange(vacation.StartDate, vacation.EndDate);
                    if (requestedDays.Intersects(lastTakenDaysOfVacation))
                        return true;
                }
                return false;
            }
            catch (Exception e)
            {

                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "BusinessLogic/EmployeeVacationLogic/IsVacationDaysExistInPreviousRequestedVacation",
                    Parameters = "requestedDays=" + new JavaScriptSerializer().Serialize(requestedDays) +
                    "userId=" + model.EmployeeVacation.EmployeeUserId + "& vacationTypeId= " + model.EmployeeVacation.VacationTypeId
                });
                return true;
            }
        }
        public static int GetUserVacationDays(int userId, int vacationTypeId, int year)
        {
            return EmployeeVacationsRepositories.GetUserVacationDays(userId, vacationTypeId, year);
        }
        public static int GetVacationStatusDaysByUserId(int userId, int statusId)
        {
            return EmployeeVacationsRepositories.GetVacationStatusDaysByUserId(userId, statusId, DateTimeHelper.Today());
        }
        public static void ChangeStatus(int employeeVacationId, EVacationStatus status)
        {
            EmployeeVacationsRepositories.ChangeStatus(employeeVacationId, status);
        }
        public static List<EmplyeeVacationDetails> GetAllCompletedVacations(int page, EVacationStatus completeStatus)
        {
            int takeCount = Config.PageItemCount();
            int skipCount = page * takeCount;
            return EmployeeVacationsRepositories.GetAllCompletedVacations(skipCount, takeCount, completeStatus, DateTimeHelper.Today());
        }
        public static List<EmplyeeVacationDetails> GetCompletedVacations(int page, int userId, EVacationStatus completeStatus)
        {
            int takeCount = Config.PageItemCount();
            int skipCount = page * takeCount;
            return EmployeeVacationsRepositories.GetCompletedVacations(skipCount, takeCount, userId, completeStatus,  DateTimeHelper.Today());
        }
        public static List<EmplyeeVacationDetails> GetEmployeeVacationTypeDetails(int statusId, int userId)
        {
            return EmployeeVacationsRepositories.GetEmployeeVacationTypeDetails(userId, statusId, DateTimeHelper.Today());
        }
        public static CreateWorkFromHomeRequest GetCreateWorkFromHomeRequest(bool isAdmin, int userId)
        {
            CreateWorkFromHomeRequest model = new CreateWorkFromHomeRequest()
            {
                EmployeeVacation = new EmployeeVacation(),
                RemainingDaysPerMonth = (int)WorkFromHomeDaysCount.MaxDaysPerMonth
            };  
            model.EmployeeVacation.EmployeeUserId = userId;
            model.EmployeeVacation.StartDate = DateTimeHelper.Today();
            VacationType vacationType = VacationTypesRepositories.GetWorkFromHomeVacationType();
            model.EmployeeVacation.VacationTypeId = vacationType.Id;
            model.EmployeeVacation.VacationDays = 1;
            model.EmployeeVacation.StatusId = isAdmin
                    ? (int)EVacationStatus.Approved
                    : (int)EVacationStatus.Pending;

            int totalTakenDaysPerYear = EmployeeVacationsRepositories.GetUserWorkFromHomeDaysPerYear(userId, vacationType.Id, DateTimeHelper.Today());
            int vacationMaxDays = vacationType.VacationLength;
            model.RemainingDaysPerYear = vacationMaxDays - totalTakenDaysPerYear;
            if (model.RemainingDaysPerMonth < 1)
            {
                model.ErrorMessage = "Can not Take or Request more than " + vacationMaxDays + " Days Per Year.";
                return model;
            }
            int totalTakenDaysPerMonth = EmployeeVacationsRepositories.GetUserWorkFromHomeDaysPerMonth(userId, vacationType.Id, DateTimeHelper.Today());
            model.RemainingDaysPerMonth = (int)WorkFromHomeDaysCount.MaxDaysPerMonth - totalTakenDaysPerMonth;
            if (model.RemainingDaysPerMonth < 1)
            {
                model.ErrorMessage = "Can not Take or Request more than " + (int)WorkFromHomeDaysCount.MaxDaysPerMonth + " Days Per Month.";
                return model;
            }

            model.Succeeded = true;
            return model;
        }
        public static CreateWorkFromHomeRequest InsertNewWrokFromHomeRequest(CreateWorkFromHomeRequest model)
        {
            model.Succeeded = false;
            model.EmployeeVacation.EndDate = model.EmployeeVacation.StartDate;
            if (model.EmployeeVacation.StartDate.Date.CompareTo(DateTimeHelper.Today().Date) < 0)
            {
                model.ErrorMessage = "Start Date Must Not be Less than Today.";
                return model;
            }
            VacationType vacationType = VacationTypesRepositories.GetWorkFromHomeVacationType();
            int totalTakenDaysPerYear = EmployeeVacationsRepositories.GetUserWorkFromHomeDaysPerYear(model.EmployeeVacation.EmployeeUserId, vacationType.Id, DateTimeHelper.Today());
            int vacationMaxDays = vacationType.VacationLength;
            model.RemainingDaysPerYear = vacationMaxDays - totalTakenDaysPerYear;
            if (model.RemainingDaysPerMonth < 1)
            {
                model.ErrorMessage = "Can not Take or Request more than " + vacationMaxDays + " Days Per Year.";
                return model;
            }
            int totalTakenDaysPerMonth = EmployeeVacationsRepositories.GetUserWorkFromHomeDaysPerMonth(model.EmployeeVacation.EmployeeUserId, vacationType.Id, DateTimeHelper.Today());
            model.RemainingDaysPerMonth = (int)WorkFromHomeDaysCount.MaxDaysPerMonth - totalTakenDaysPerMonth;
            if (model.RemainingDaysPerMonth < 1)
            {
                model.ErrorMessage = "Can not Take or Request more than " + (int)WorkFromHomeDaysCount.MaxDaysPerMonth + " Days Per Month.";
                return model;
            }
            if (EmployeeVacationsRepositories.IsWorkFromHomeDayTakenBefore(model.EmployeeVacation.StartDate, model.EmployeeVacation.EmployeeUserId, vacationType.Id))
            {
                model.ErrorMessage = "You Request Work From Home at " + model.EmployeeVacation.StartDate.ToString("dd MMM yyyy") + ", Date  before.";
                return model;
            }
            EmployeeVacationsRepositories.InsertNewEmployeeVacation(model.EmployeeVacation);
            model.ErrorMessage = "Your Request Submit Successfully at " + model.EmployeeVacation.StartDate.ToString("dd MMM yyyy") + " Date.";
            model.Succeeded = true;
            return model;
        }
    }
}