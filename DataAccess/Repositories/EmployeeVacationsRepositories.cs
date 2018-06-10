using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMapping.Entities;
using DataMapping.Services;
using DataMapping.Enums;

namespace DataAccess.Repositories
{
    public class EmployeeVacationsRepositories
    {
        public static List<VacationStatu> GetAllVacationStatus()
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.VacationStatus.ToList();
            }
        }
        public static List<EmployeeVacation> GetAllEmployeeVacationsByVacationTypeId(int? vacationTypeId, int? employeeUserId, DateTime toDay)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.EmployeeVacations.Where(
                    v => v.EmployeeUserId == employeeUserId
                    && v.VacationTypeId == vacationTypeId
                    && v.StartDate.Year == toDay.Year).ToList();
            }
        }
        public static List<EmplyeeVacationDetails> GetAllEmployeeVacationsList(int userId, int statusId, int skipCount, int takeCount, DateTime toDay)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = from vacation in db.EmployeeVacations
                        join
                        status in db.VacationStatus on vacation.StatusId equals status.Id
                        join
                        vacationType in db.VacationTypes on vacation.VacationTypeId equals vacationType.Id
                        join
                        user in db.UserProfiles on vacation.EmployeeUserId equals user.UserId
                        where (user.UserId == userId && status.Id == statusId
                        && vacation.StartDate.Year == toDay.Year)
                        select new EmplyeeVacationDetails
                        {
                            Id = vacation.Id,
                            EmployeeUserId = vacation.EmployeeUserId,
                            EmployeeUserName = user.UserName,
                            StatusId = vacation.StatusId,
                            StatusName = status.Name,
                            VacationTypeId = vacation.VacationTypeId,
                            VacationTypeName = vacationType.Name,
                            StartDate = vacation.StartDate,
                            EndDate = vacation.EndDate,
                            VacationDays = vacation.VacationDays,
                        };
                return q.OrderByDescending(x => x.StartDate).Skip(skipCount).Take(takeCount).ToList();
            }
        }
        public static List<EmplyeeVacationDetails> GetAllUsersVacationsList(int statusId, DateTime toDay, int skipCount, int takeCount)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = from vacation in db.EmployeeVacations
                        join
                        status in db.VacationStatus on vacation.StatusId equals status.Id
                        join
                        vacationType in db.VacationTypes on vacation.VacationTypeId equals vacationType.Id
                        join
                        user in db.UserProfiles on vacation.EmployeeUserId equals user.UserId
                        where (status.Id == statusId
                        //&& vacation.EndDate.Date >= toDay.Date
                        && vacation.StartDate.Year == toDay.Year)
                        select new EmplyeeVacationDetails
                        {
                            Id = vacation.Id,
                            EmployeeUserId = vacation.EmployeeUserId,
                            EmployeeUserName = user.UserName,
                            StatusId = vacation.StatusId,
                            StatusName = status.Name,
                            VacationTypeId = vacation.VacationTypeId,
                            VacationTypeName = vacationType.Name,
                            StartDate = vacation.StartDate,
                            EndDate = vacation.EndDate,
                            VacationDays = vacation.VacationDays,
                        };
                return q.OrderByDescending(x => x.StartDate).Skip(skipCount).Take(takeCount).ToList();
            }
        }
        public static List<EmplyeeVacationDetails> GetAllUsersVacationsList(int userId, int statusId, DateTime toDay, int skipCount, int takeCount)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = from vacation in db.EmployeeVacations
                        join
                        status in db.VacationStatus on vacation.StatusId equals status.Id
                        join
                        vacationType in db.VacationTypes on vacation.VacationTypeId equals vacationType.Id
                        join
                        user in db.UserProfiles on vacation.EmployeeUserId equals user.UserId
                        where (user.UserId == userId && status.Id == statusId
                               && vacation.EndDate.Date >= toDay.Date
                               && vacation.StartDate.Year == toDay.Year)
                        select new EmplyeeVacationDetails
                        {
                            Id = vacation.Id,
                            EmployeeUserId = vacation.EmployeeUserId,
                            EmployeeUserName = user.UserName,
                            StatusId = vacation.StatusId,
                            StatusName = status.Name,
                            VacationTypeId = vacation.VacationTypeId,
                            VacationTypeName = vacationType.Name,
                            StartDate = vacation.StartDate,
                            EndDate = vacation.EndDate,
                            VacationDays = vacation.VacationDays,
                        };
                return q.OrderByDescending(x => x.StartDate).Skip(skipCount).Take(takeCount).ToList();
            }
        }
        public static List<EmplyeeVacationDetails> GetAllCompletedVacations(int skipCount, int takeCount, EVacationStatus completeStatus, DateTime datetime)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = (from vacation in db.EmployeeVacations
                         join status in db.VacationStatus
                             on vacation.StatusId equals status.Id
                         join vacationType in db.VacationTypes
                             on vacation.VacationTypeId equals vacationType.Id
                         join user in db.UserProfiles
                             on vacation.EmployeeUserId equals user.UserId
                         where vacation.StartDate.Year == datetime.Year &&
                             vacation.StatusId == (int)completeStatus
                         select new EmplyeeVacationDetails
                         {
                             Id = vacation.Id,
                             EmployeeUserId = vacation.EmployeeUserId,
                             EmployeeUserName = user.UserName,
                             StatusId = vacation.StatusId,
                             StatusName = status.Name,
                             VacationTypeId = vacation.VacationTypeId,
                             VacationTypeName = vacationType.Name,
                             StartDate = vacation.StartDate,
                             EndDate = vacation.EndDate,
                             VacationDays = vacation.VacationDays
                         })
                        .OrderByDescending(x => x.StartDate)
                        .Skip(skipCount)
                        .Take(takeCount)
                        .ToList();
                return q;
            }
        }
        public static List<EmplyeeVacationDetails> GetCompletedVacations(int skipCount, int takeCount, int userId, EVacationStatus completeStatus, DateTime datetime)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = (from vacation in db.EmployeeVacations
                         join status in db.VacationStatus
                             on vacation.StatusId equals status.Id
                         join vacationType in db.VacationTypes
                             on vacation.VacationTypeId equals vacationType.Id
                         join user in db.UserProfiles
                             on vacation.EmployeeUserId equals user.UserId
                         where vacation.StartDate.Year == datetime.Year &&
                             vacation.EmployeeUserId == userId &&
                             vacation.StatusId == (int)completeStatus
                         select new EmplyeeVacationDetails
                         {
                             Id = vacation.Id,
                             EmployeeUserId = vacation.EmployeeUserId,
                             EmployeeUserName = user.UserName,
                             StatusId = vacation.StatusId,
                             StatusName = status.Name,
                             VacationTypeId = vacation.VacationTypeId,
                             VacationTypeName = vacationType.Name,
                             StartDate = vacation.StartDate,
                             EndDate = vacation.EndDate,
                             VacationDays = vacation.VacationDays
                         })
                        .OrderByDescending(x => x.StartDate)
                        .Skip(skipCount)
                        .Take(takeCount)
                        .ToList();
                return q;
            }
        }
        public static int GetUserVacationDays(int? userId, int? vacationTypeId, int year)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {

                List<EmployeeVacation> emp = (from empVaction in db.EmployeeVacations
                                              where empVaction.EmployeeUserId == userId && empVaction.VacationTypeId == vacationTypeId
                                              && empVaction.StartDate.Year == year
                                              && empVaction.EndDate.Year == year
                                              &&
                                              (empVaction.StatusId == (int)EVacationStatus.Approved ||
                                              empVaction.StatusId == (int)EVacationStatus.Finished)
                                              select empVaction
                                             ).ToList();
                return emp.Sum(e => e.VacationDays);


            }
        }
        public static int GetUserWorkFromHomeDaysPerYear(int? userId, int vacationTypeId, DateTime dateTime)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {

                List<EmployeeVacation> emp = (from vacationType in db.VacationTypes
                                              join empVaction in db.EmployeeVacations
                                              on vacationType.Id equals empVaction.VacationTypeId
                                              join vacationStatus in db.VacationStatus on empVaction.StatusId equals vacationStatus.Id
                                              where empVaction.EmployeeUserId == userId && empVaction.VacationTypeId == vacationTypeId
                                              && vacationType.IsDeleted == false
                                              && empVaction.StartDate.Year == dateTime.Year
                                              && empVaction.EndDate.Year == dateTime.Year
                                              &&
                                              (vacationStatus.Name == EVacationStatus.Pending.ToString() ||
                                              vacationStatus.Name == EVacationStatus.Approved.ToString() ||
                                              vacationStatus.Name == EVacationStatus.Finished.ToString())
                                              select empVaction
                         ).ToList();
                return emp.Sum(e => e.VacationDays);


            }
        }
        public static int GetUserWorkFromHomeDaysPerMonth(int? userId, int vacationTypeId, DateTime dateTime)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                List<EmployeeVacation> emp = (from vacationType in db.VacationTypes
                                              join empVaction in db.EmployeeVacations
                                              on vacationType.Id equals empVaction.VacationTypeId
                                              join vacationStatus in db.VacationStatus on empVaction.StatusId equals vacationStatus.Id
                                              where empVaction.EmployeeUserId == userId && empVaction.VacationTypeId == vacationTypeId
                                              && vacationType.IsDeleted == false
                                              && empVaction.StartDate.Month == dateTime.Month
                                              && empVaction.StartDate.Year == dateTime.Year
                                              &&
                                              (vacationStatus.Name == EVacationStatus.Pending.ToString() ||
                                              vacationStatus.Name == EVacationStatus.Approved.ToString() ||
                                              vacationStatus.Name == EVacationStatus.Finished.ToString())
                                              select empVaction
                         ).ToList();
                return emp.Sum(e => e.VacationDays);


            }
        }
        public static int GetVacationStatusDaysByUserId(int userId, int statusId, DateTime today)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.EmployeeVacations
                    .Where(
                    v => v.EmployeeUserId == userId
                    && v.StatusId == statusId
                    && v.StartDate.Year == today.Year)
                    .Sum(v => v.VacationDays);
            }
        }
        public static void InsertNewEmployeeVacation(EmployeeVacation employeeVacation)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                db.EmployeeVacations.Add(employeeVacation);
                db.SaveChanges();
            }
        }
        public static void ChangeStatus(int employeeVacationId, EVacationStatus status)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = db.EmployeeVacations.FirstOrDefault(e => e.Id == employeeVacationId);
                if (q != null) q.StatusId = (int)status;
                db.SaveChanges();
            }
        }
        public static List<EmplyeeVacationDetails> GetEmployeeVacationTypeDetails(int userId, int statusId, DateTime today)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                List<EmplyeeVacationDetails> emplyeeVacationDetails = (
                    from eVac in db.EmployeeVacations.Where(q => q.EmployeeUserId == userId && q.StatusId == statusId && q.StartDate.Year == today.Year)
                    group eVac by eVac.VacationTypeId into empVac
                    join vacationType in db.VacationTypes
                    on empVac.FirstOrDefault().VacationTypeId equals vacationType.Id

                    select new EmplyeeVacationDetails()
                    {
                        VacationTypeId = vacationType.Id,
                        VacationTypeName = vacationType.Name,
                        VacationDays = empVac.Sum(q => q.VacationDays)
                    }
                ).ToList();

                return emplyeeVacationDetails;
            }
        }
        public static bool IsWorkFromHomeDayTakenBefore(DateTime dateTime, int? userId, int vacationTypeId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var item = db.EmployeeVacations.FirstOrDefault(
                    v => v.EmployeeUserId == userId
                    && v.VacationTypeId == vacationTypeId
                    && v.StartDate.Date == dateTime.Date
                    && (v.StatusId == (int)EVacationStatus.Pending
                    || v.StatusId == (int)EVacationStatus.Approved
                    || v.StatusId == (int)EVacationStatus.Finished)
                    );
                return item != null ? true : false;

            }
        }
    }
}