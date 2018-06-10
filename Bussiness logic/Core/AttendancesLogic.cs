using System;
using System.Collections.Generic;
using DataAccess.Repositories;
using DataMapping.Entities;
using DataMapping.Services;
using BusinessLogic.Helpers;

namespace BusinessLogic.Core
{
    public class AttendancesLogic
    {
        public static List<UserSign> GetAllUsersNotSignOut(int page)
        {
            int takeCount = Config.PageItemCount();
            int skipCount = page * takeCount;
            DateTime todayDate = DateTimeHelper.Today();
            return AttendancesRepositories.GetAllUserNotSignOut(todayDate, skipCount, takeCount);
        }
        public static List<UserSign> GetAllUsersNotSignOut()
        {
            DateTime todayDate = DateTimeHelper.Today();
            return AttendancesRepositories.GetAllUserNotSignOut(todayDate);
        }
        public static List<UserSign> GetAllOnlineUseres()
        {
            DateTime todayDate = DateTimeHelper.Today();
            return AttendancesRepositories.GetAllOnlineUseres(todayDate);
        }
        public static Attendance GetLastSignByUserId(int userId)
        {
            DateTime todayDate = DateTimeHelper.Today();
            return AttendancesRepositories.GetTodayAttendance(userId, todayDate);
        }
        public static void InsertNewAttendance(Attendance attend)
        {
            DateTime todayDate = DateTimeHelper.Today();
            AttendancesRepositories.InsertNewAttendance(attend, todayDate);



        }
        public static void UpdateAttendance(Attendance attendance)
        {
            attendance.SignOutDate = DateTimeHelper.Today().AddHours(2);
            AttendancesRepositories.UpdateAttendce(attendance);
        }
        public static void UpdateAttendance(int id)
        {
            AttendancesRepositories.UpdateAttendce(id);
        }

        public static void SignOutToAllUsers()
        {
            var users = AttendancesRepositories.SignOutToAllUsers(DateTimeHelper.Today());
            foreach (var user in users)
            {
                if (string.IsNullOrEmpty(user.Email))
                {
                    EmailHelper.MangerSignOutEmailHeader(user.UserName);
                }
                else
                {
                    EmailHelper.MissingSignOutSendEmail(user.Email, user.UserName);
                }
            }
        }
        public static List<Attendance> GetAllUserAttendencesBetweenTwoDates(int userId, DateTime startDate, DateTime endDate)
        {
            return AttendancesRepositories.GetAllUserAttendencesBetweenTowDates(userId, startDate, endDate);
        }
        public static double GetTotalUserWorkingHoursInMonth(int userId, DateTime startDate, DateTime endDate)
        {
            return AttendancesRepositories.GetTotalUserWorkingHoursInMonth(userId, startDate, endDate);
        }
    }
}
