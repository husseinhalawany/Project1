using DataMapping.Entities;
using DataMapping.Enums;
using DataMapping.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class WorkFromHomeReopsitories
    {
        public static List<RequestFromHomeDayDetails> GetEmployeeWorkFromHomeDaysList(int userId, int statusId,int vacationYearId,DateTime today, int skipCount, int takeCount)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = from workfromhome in db.WorkFromHomeDays
                        join
                        vacationyear in db.VacationYears on workfromhome.VacationYearId equals vacationyear.Id
                        join
                        status in db.VacationStatus on workfromhome.StatusId equals status.Id
                        join
                        user in db.UserProfiles on workfromhome.EmployeeUserId equals user.UserId
                        where (user.UserId == userId && status.Id == statusId
                        &&vacationyear.Id== vacationYearId && workfromhome.Date.Date >= today.Date)
                        select new RequestFromHomeDayDetails
                        {
                            Id = workfromhome.Id,
                            EmployeeUserId = workfromhome.EmployeeUserId,
                            EmployeeUserName = user.UserName,
                            StatusId = workfromhome.StatusId,
                            StatusName = status.Name,
                            VacationYearId=vacationyear.Id,
                            VacationYearName=vacationyear.YearName,
                            Date = workfromhome.Date,
                        };
                return q.OrderByDescending(x => x.Date).Skip(skipCount).Take(takeCount).ToList();
            }
        }
        public static List<RequestFromHomeDayDetails> GetEmployeeWorkFromHomeDaysList(int statusId, int vacationYearId,DateTime today, int skipCount, int takeCount)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = from workfromhome in db.WorkFromHomeDays
                        join
                        vacationyear in db.VacationYears on workfromhome.VacationYearId equals vacationyear.Id
                        join
                        status in db.VacationStatus on workfromhome.StatusId equals status.Id
                        join
                        user in db.UserProfiles on workfromhome.EmployeeUserId equals user.UserId
                        where (status.Id == statusId && vacationyear.Id == vacationYearId && workfromhome.Date.Date >= today.Date)
                        select new RequestFromHomeDayDetails
                        {
                            Id = workfromhome.Id,
                            EmployeeUserId = workfromhome.EmployeeUserId,
                            EmployeeUserName = user.UserName,
                            StatusId = workfromhome.StatusId,
                            StatusName = status.Name,
                            VacationYearId = vacationyear.Id,
                            VacationYearName = vacationyear.YearName,
                            Date = workfromhome.Date,
                        };
                return q.OrderByDescending(x => x.Date).Skip(skipCount).Take(takeCount).ToList();
            }
        }
        public static int GetEmployeeWorkFromHomeDaysPerYear(int? userId, DateTime dateTime)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {

                List<WorkFromHomeDay> emp = (from workfromhome in db.WorkFromHomeDays
                                              join vacationStatus in db.VacationStatus on workfromhome.StatusId equals vacationStatus.Id
                                              where workfromhome.EmployeeUserId == userId
                                              && workfromhome.Date.Year == dateTime.Year
                                              &&
                                              (vacationStatus.Id == (int)EVacationStatus.Pending ||
                                              vacationStatus.Id == (int)EVacationStatus.Approved ||
                                              vacationStatus.Id == (int)EVacationStatus.Finished)
                                              select workfromhome
                         ).ToList();
                return emp.Count();


            }
        }
        public static int GetUserWorkFromHomeDaysPerMonth(int? userId, DateTime dateTime)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {

                List<WorkFromHomeDay> emp = (from workfromhome in db.WorkFromHomeDays
                                             join vacationStatus in db.VacationStatus on workfromhome.StatusId equals vacationStatus.Id
                                             where workfromhome.EmployeeUserId == userId
                                             && workfromhome.Date.Year == dateTime.Year
                                             && workfromhome.Date.Month == dateTime.Month
                                             &&
                                             (vacationStatus.Id == (int)EVacationStatus.Pending ||
                                             vacationStatus.Id == (int)EVacationStatus.Approved ||
                                             vacationStatus.Id == (int)EVacationStatus.Finished)
                                             select workfromhome
                         ).ToList();
                return emp.Count();


            }
        }
        public static void InsertNewEmployeeWorkFromHome(WorkFromHomeDay workFromHomeDay)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                db.WorkFromHomeDays.Add(workFromHomeDay);
                db.SaveChanges();
            }
        }
        public static void ChangeStatus(int Id, EVacationStatus status)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = db.WorkFromHomeDays.FirstOrDefault(e => e.Id == Id);
                if (q != null) q.StatusId = (int)status;
                db.SaveChanges();
            }
        }
        public static bool IsWorkFromHomeDayTakenBefore(DateTime dateTime, int? userId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var item = db.WorkFromHomeDays.FirstOrDefault(
                    v => v.EmployeeUserId == userId
                    && v.Date.Date == dateTime.Date
                    && (v.StatusId == (int)EVacationStatus.Pending
                    || v.StatusId == (int)EVacationStatus.Approved
                    || v.StatusId == (int)EVacationStatus.Finished)
                    );
                return item != null ? true : false;

            }
        }
    }
}
