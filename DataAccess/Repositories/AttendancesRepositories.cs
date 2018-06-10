using DataMapping.Entities;
using System;
using System.Collections.Generic;
using DataMapping.Services;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
namespace DataAccess.Repositories
{
    public class AttendancesRepositories
    {
        public static Attendance GetTodayAttendance(int userId, DateTime today)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
              
                return db.Attendances
                        .FirstOrDefault(a => a.EmpUserId == userId &&
                             a.SignInDate.Value == today &&
                             a.IsDeleted == false);
            }
        }
        public static List<UserSign> GetAllUserNotSignOut(DateTime today, int skipCount, int takeCount)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = (from Attendance in db.Attendances
                         join user in db.UserProfiles
                             on Attendance.EmpUserId equals user.UserId
                         where (Attendance.SignOutDate == null &&
                            Attendance.SignInDate.Value < today &&
                             user.LockedUser == false &&
                             Attendance.IsDeleted == false)

                         select new UserSign
                         {
                             AttendanceId = Attendance.Id,
                             UserId = Attendance.EmpUserId,
                             Name = user.FirstName + " " + user.LastName,
                             SignInDate = Attendance.SignInDate,
                             Image = user.ProfilePictureUrl

                         }).OrderBy(x => x.AttendanceId)
                        .Skip(skipCount)
                        .Take(takeCount)
                        .ToList();
                return q;
            }
        }
        public static List<UserSign> GetAllUserNotSignOut(DateTime today)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = (from Attendance in db.Attendances
                         join user in db.UserProfiles
                         on Attendance.EmpUserId equals user.UserId
                         where (Attendance.SignOutDate == null
                         && Attendance.SignInDate.Value == today
                         && user.LockedUser == false && Attendance.IsDeleted == false)

                         select new UserSign
                         {
                             AttendanceId = Attendance.Id,
                             UserId = Attendance.EmpUserId,
                             Name = user.FirstName + " " + user.LastName,
                             SignInDate = Attendance.SignInDate,
                             Image = user.ProfilePictureUrl

                         })
                        .ToList();
                return q;
            }
        }
        public static List<UserSign> GetAllOnlineUseres(DateTime today)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = (from Attendance in db.Attendances
                         join user in db.UserProfiles
                             on Attendance.EmpUserId equals user.UserId
                         where (Attendance.SignOutDate == null &&
                             Attendance.SignInDate.Value== today &&
                             user.LockedUser == false &&
                             Attendance.IsDeleted == false)

                         select new UserSign
                         {
                             AttendanceId = Attendance.Id,
                             UserId = Attendance.EmpUserId,
                             Name = user.FirstName + " " + user.LastName,
                             SignInDate = Attendance.SignInDate,
                             ImgUrl = user.ProfilePictureUrl,
                         })
                        .ToList();
                return q;
            }
        }

        public static void InsertNewAttendance(Attendance item, DateTime myDate)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {

                var q = (from user in db.UserProfiles
                         join attendance in db.Attendances
                             on user.UserId equals attendance.EmpUserId

                         where (attendance.SignOutDate == null &&
                                 attendance.SignInDate.Value == myDate &&
                                 user.LockedUser == false &&
                                 attendance.IsDeleted == false &&
                                 attendance.EmpUserId == item.EmpUserId)

                         select new UserSign
                         {
                             UserId = attendance.EmpUserId
                         })
                        .ToList();

                if (q.Count == 0)
                {
                    db.Attendances.Add(item);
                    db.SaveChanges();
                }
            }
        }
        public static void UpdateAttendce(Attendance item)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = db.Attendances.SingleOrDefault(a => a.Id == item.Id);
                if (q != null)
                {
                    q.SignOutDate = item.SignOutDate;
                    q.TotalHours = (item.SignOutDate - item.SignInDate).Value.TotalHours;
                    db.SaveChanges();
                }
            }
        }
        public static void UpdateAttendce(int id)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = db.Attendances.SingleOrDefault(a => a.Id == id);
                if (q != null)
                {
                    q.SignOutDate = q.SignInDate.Value.AddHours(8);
                    q.TotalHours = 8;
                    db.SaveChanges();
                }
            }
        }
        public static UserProfile SignOutToUser(int id)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = db.Attendances.SingleOrDefault(a => a.Id == id);
                if (q != null)
                {
                    q.SignOutDate = q.SignInDate.Value.AddHours(8);
                    q.TotalHours = 8;
                    db.SaveChanges();
                }
                return db.UserProfiles.FirstOrDefault(x => x.UserId == q.EmpUserId);
            }
        }
       
     
      
        public static bool IsUserSignedIn(int userId, DateTime todayServerTime)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                Attendance attendance = db.Attendances
                    .FirstOrDefault(x => x.EmpUserId == userId &&
                        x.SignInDate.Value == todayServerTime);

                return (attendance != null);
            }
        }
        public static List<UserProfile> SignOutToAllUsers(DateTime today)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var attendancesNotSignedOut = db.Attendances
                    .Where(a => a.SignInDate.Value < today &&
                        a.SignOutDate == null && a.IsDeleted == false)
                    .ToList();

                for (int i = 0; i < attendancesNotSignedOut.Count(); i++)
                {
                    attendancesNotSignedOut[i].SignOutDate = attendancesNotSignedOut[i].SignInDate.Value.AddHours(8);
                }

                db.SaveChanges();
                List<UserProfile> users = new List<UserProfile>();
                foreach (var item in attendancesNotSignedOut)
                {
                    var user = db.UserProfiles.FirstOrDefault(x => x.UserId == item.EmpUserId);
                    if (!(users.Contains(user)))
                    {
                        users.Add(user);
                    }
                }
                return users;
            }
        }
        public static List<Attendance> GetAllUserAttendencesBetweenTowDates(int userId, DateTime startDate, DateTime endDate)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.Attendances
                    .Where(a => a.EmpUserId == userId &&
                        a.SignInDate > startDate &&
                        a.SignInDate < endDate)
                    .ToList();
            }
        }
        public static double GetTotalUserWorkingHoursInMonth(int userId, DateTime startDate, DateTime endDate)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.Attendances
                    .AsEnumerable()
                    .Where(a => a.EmpUserId == userId &&
                        a.SignOutDate != null &&
                        a.SignInDate > startDate &&
                        a.SignInDate < endDate)
                    .Sum(a => a.TotalHours);
            }
        }
    }
}
