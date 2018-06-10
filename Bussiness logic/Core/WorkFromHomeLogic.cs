using BusinessLogic.Helpers;
using DataAccess.Repositories;
using DataMapping.Services;
using System;
using System.Collections.Generic;
using BusinessLogic.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MangamentProject.Core;
using DataMapping.Entities;
using BusinessLogic.Model;
using DataMapping.Enums;

namespace BusinessLogic.Core
{
    public class WorkFromHomeLogic
    {
        public static List<RequestFromHomeDayDetails> GetEmployeeWorkFromHomeDaysList(int page, int statusId, int userId)
        {
            int takeCount = Config.PageItemCount();
            int skipCount = page * takeCount;
            VacationYear vacationYear = VacationYearsLogic.GetCurrentVacationYear();
            int vacationYearId = vacationYear.Id;
            DateTime today = DateTimeHelper.Today();
            return WorkFromHomeReopsitories.GetEmployeeWorkFromHomeDaysList(userId, statusId, vacationYearId, today, skipCount, takeCount);
        }
        public static List<RequestFromHomeDayDetails> GetEmployeeWorkFromHomeDaysList(int page, int statusId)
        {
            int takeCount = Config.PageItemCount();
            int skipCount = page * takeCount;
            VacationYear vacationYear = VacationYearsLogic.GetCurrentVacationYear();
            int vacationYearId = vacationYear.Id;
            return WorkFromHomeReopsitories.GetEmployeeWorkFromHomeDaysList(statusId, vacationYearId,DateTimeHelper.Today(), skipCount, takeCount);
        }
        public static WorkFromHomeModel CreateWorkFomHomeModel(bool isAdmin, int userId)
        {
            WorkFromHomeModel model = new WorkFromHomeModel()
            {
                WorkFromHomeDay = new WorkFromHomeDay(),
                RemainingDaysPerMonth = (int)WorkFromHomeDaysCount.MaxDaysPerMonth
            };
            model.WorkFromHomeDay.UserProfile = UserProfilesLogic.GetUserById(userId);
            model.WorkFromHomeDay.EmployeeUserId = userId;
            model.WorkFromHomeDay.Date = DateTimeHelper.Today();
            model.WorkFromHomeDay.StatusId = isAdmin
                    ? (int)EVacationStatus.Approved
                    : (int)EVacationStatus.Pending;
            int totalTakenDaysPerYear = WorkFromHomeReopsitories.GetEmployeeWorkFromHomeDaysPerYear(userId, DateTimeHelper.Today());
            int vacationMaxDays = (int)WorkFromHomeDaysCount.MAxDaysPerYEars;
            model.RemainingDaysPerYear = vacationMaxDays - totalTakenDaysPerYear;
            if (model.RemainingDaysPerMonth < 1)
            {
                model.ErrorMessage = "Can not Take or Request more than " + vacationMaxDays + " Days Per Year.";
                model.Succeeded = false;
                return model;
            }
            int totalTakenDaysPerMonth = WorkFromHomeReopsitories.GetUserWorkFromHomeDaysPerMonth(userId, DateTimeHelper.Today());
            model.RemainingDaysPerMonth = (int)WorkFromHomeDaysCount.MaxDaysPerMonth - totalTakenDaysPerMonth;
            if (model.RemainingDaysPerMonth < 1)
            {
                model.ErrorMessage = "Can not Take or Request more than " + (int)WorkFromHomeDaysCount.MaxDaysPerMonth + " Days Per Month.";
                model.Succeeded = false;
                return model;
            }

            model.Succeeded = true;
            return model;
        }
        public static WorkFromHomeModel CheckWrokFromHomeRequestModel(WorkFromHomeModel model)
        {
            if (model.WorkFromHomeDay.Date.Date.CompareTo(DateTimeHelper.Today().Date) < 0)
            {
                model.ErrorMessage = " Date Must Not be Less than Today.";
                model.Succeeded = false;

            }
            else if (WorkFromHomeReopsitories.IsWorkFromHomeDayTakenBefore(model.WorkFromHomeDay.Date, model.WorkFromHomeDay.EmployeeUserId))
            {
                model.ErrorMessage = "You Request Work From Home at " + model.WorkFromHomeDay.Date.ToString("dd MMM yyyy") + ", Date  before.";
                model.Succeeded = false;

            }
            else
            {
                model.Succeeded = true;
            }
            return model;
        }
        public static void InsertNewWrokFromHomeRequest(WorkFromHomeModel model)
        {
            VacationYear vacationYear = VacationYearsLogic.GetCurrentVacationYear();
            model.WorkFromHomeDay.VacationYearId = vacationYear.Id;
            WorkFromHomeReopsitories.InsertNewEmployeeWorkFromHome(model.WorkFromHomeDay);
        }
        public static void ChangeStatus(int Id, EVacationStatus status)
        {
            WorkFromHomeReopsitories.ChangeStatus(Id, status);
        }
    }
}
