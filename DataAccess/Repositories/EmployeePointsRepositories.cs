using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMapping.Entities;
using DataMapping.Services;

namespace DataAccess.Repositories
{
    public class EmployeePointsRepositories
    {
        public static void InsertNewEmployeePoint(EmployeePoint employeePoint)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                db.EmployeePoints.Add(employeePoint);
                db.SaveChanges();
            }
        }
        public static void UpdateEmployeePoint(EmployeePoint employeePoint)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                EmployeePoint item = db.EmployeePoints.FirstOrDefault(q => q.Id == employeePoint.Id);
                if (item != null)
                {
                    item.ActionRateId = employeePoint.ActionRateId;
                    item.Date = employeePoint.Date;
                    item.UserId = employeePoint.UserId;
                    item.Rate = employeePoint.Rate;
                    db.SaveChanges();
                }
            }
        }
        public static List<EmployeePointDetails> BestEmployeesOfMonthList(DateTime dateTime)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return (
                     from ePoint in db.EmployeePoints
                        .Where(a => a.Date.Year == dateTime.Year &&
                        a.Date.Month == dateTime.Month)

                     group ePoint by ePoint.UserId
                    into empPoint
                     join user in db.UserProfiles
                         on empPoint.FirstOrDefault().UserId equals user.UserId
                     where user.LockedUser == false && user.webpages_Roles.FirstOrDefault().RoleName != "Admin"

                     select new EmployeePointDetails()
                    {
                        UserId = user.UserId,
                        UserName = user.UserName,
                        TotalPoints = empPoint.Sum(q => q.Rate),
                        ProfilePicURL = user.ProfilePictureUrl
                    }
                    )
                    .OrderByDescending(a => a.TotalPoints)
                    .ToList();
            }
        }
        public static EmployeePointDetails BestEmployeeOfMonth(DateTime dateTime)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return (
                     from ePoint in
                         db.EmployeePoints
                             .Where(a => a.Date.Year == dateTime.Year &&
                                 a.Date.Month == dateTime.Month)

                     group ePoint by ePoint.UserId
                     into empPoint
                     join user in db.UserProfiles
                         on empPoint.FirstOrDefault().UserId equals user.UserId
                     where user.LockedUser == false && user.webpages_Roles.FirstOrDefault().RoleName != "Admin"

                     select new EmployeePointDetails()
                    {
                        UserId = user.UserId,
                        UserName = user.UserName,
                        TotalPoints = empPoint.Sum(q => q.Rate),
                        ProfilePicURL = user.ProfilePictureUrl
                    }
                    )
                    .OrderByDescending(a => a.TotalPoints)
                    .FirstOrDefault();
            }
        }
        public static List<EmployeePointDetails> BestEmployeesOfQuarterList(DateTime startDate, DateTime endDate)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return (
                     from ePoint in db.EmployeePoints
                       .Where(a => a.Date > startDate && a.Date < endDate)

                     group ePoint by ePoint.UserId
                   into empPoint
                     join user in db.UserProfiles
                         on empPoint.FirstOrDefault().UserId equals user.UserId
                     where user.LockedUser == false && user.webpages_Roles.FirstOrDefault().RoleName != "Admin"
                     select new EmployeePointDetails()
                    {
                        UserId = user.UserId,
                        UserName = user.UserName,
                        TotalPoints = empPoint.Sum(q => q.Rate),
                        ProfilePicURL = user.ProfilePictureUrl
                    }
                    )
                    .OrderByDescending(a => a.TotalPoints)
                    .ToList();
            }
        }
        public static EmployeePointDetails BestEmployeeOfQuarter(DateTime startDate, DateTime endDate)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                 return (
                     from ePoint in db.EmployeePoints
                         .Where(a => a.Date > startDate && a.Date < endDate)

                     group ePoint by ePoint.UserId
                     into empPoint
                     join user in db.UserProfiles
                         on empPoint.FirstOrDefault().UserId equals user.UserId
                     where user.LockedUser == false && user.webpages_Roles.FirstOrDefault().RoleName != "Admin"


                     select new EmployeePointDetails()
                    {
                        UserId = user.UserId,
                        UserName = user.UserName,
                        TotalPoints = empPoint.Sum(q => q.Rate),
                        ProfilePicURL = user.ProfilePictureUrl
                    }
                    )
                    .OrderByDescending(a => a.TotalPoints)
                    .FirstOrDefault();
            }
        }
    }
}
