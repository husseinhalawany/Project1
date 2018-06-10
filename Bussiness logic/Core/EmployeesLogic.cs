using BusinessLogic.Model;
using DataAccess.Repositories;
using DataMapping.Entities;
using DataMapping.Services;
using BusinessLogic.Helpers;
using System;
using System.Collections.Generic;
using DataMapping.JSONData;

namespace BusinessLogic.Core
{
    public class EmployeesLogic
    {
        public static List<EmployeeUsersDetails> GetEmployeesByRoleId(int roleId, int page)
        {
            int takeCount = Config.PageItemCount();
            int skipCount = page * takeCount;
            return EmployeesRepositories.GetEmployeesByRoleId(roleId, skipCount, takeCount);
        }
        public static List<EmployeeUsersDetails> GetEmployeesExceptAdmin(int page)
        {
            int takeCount = Config.PageItemCount();
            int skipCount = page * takeCount;
            return EmployeesRepositories.GetEmployeesExceptAdmin(skipCount, takeCount);
        }
        public static List<UserHistoryDetails> GetUsersHistory(int page)
        {
            int takeCount = Config.PageItemCount();
            int skipCount = page * takeCount;
            var userHistoryList = EmployeesRepositories.GetUsersHistory(skipCount, takeCount);
            foreach (var userHistory in userHistoryList)
            {
                int days, years, months, weeks;
                DateTimeHelper.GetDifference(userHistory.JoinDate, DateTimeHelper.Today(), out years, out months, out weeks, out days);
                userHistory.Years = years;
                userHistory.Months = months;
                userHistory.Weeks = weeks;
                userHistory.Days = days;
            }
            return userHistoryList;
        }
        public static List<UserBirthdayDetails> GetMonthlyBirthday()
        {
            var userHistoryList = EmployeesRepositories.GetMonthlyBirthday(DateTimeHelper.Today().Month);
            return userHistoryList;
        }
        public static List<UserData> GetEmployeesExceptAdmin()
        {
            return EmployeesRepositories.GetEmployeesExceptAdmin();
        }
        public static List<UserData> GetAllAdminEmployees()
        {
            return EmployeesRepositories.GetAllAdminEmoloyees();
        }
        public static EditProfileModel GetEditProfileModel(string userName)
        {
            EmployeeUsersDetails employee = EmployeesRepositories.GetEmployeeByUserName(userName);
            EditProfileModel model = new EditProfileModel
            {
                UserId = employee.UserId,
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Address = employee.Address,
                Phone1 = employee.Phone1,
                Phone2 = employee.Phone2,
                CurrentSalary = employee.CurrentSalary,
                PreviousSalary = employee.PreviousSalary,
                JoinDate = employee.JoinDate,
                ImgURL = employee.ImgURL,
                BirthDate = employee.BirthDate,
                day = employee.day,
                Month = employee.Month,
                year = employee.year,
                NumberOfDaysInMonth = employee.NumberOfDaysInMonth,
            };
            return model;
        }
        public static ChangePasswordDetails GetChangePasswordModel(int userId)
        {
            return EmployeesRepositories.GetEmployeePasswordDetails(userId);
        }
        public static EditProfileModel GetEditProfileModel(int userId)
        {
            EditProfileModel model = new EditProfileModel();
            EmployeeUsersDetails employee = EmployeesRepositories.GetEmployeeUserDetailsById(userId);
            model.UserId = employee.UserId;
            model.EmployeeId = employee.EmployeeId;
            model.FirstName = employee.FirstName;
            model.LastName = employee.LastName;
            model.Email = employee.Email;
            model.Address = employee.Address;
            model.Phone1 = employee.Phone1;
            model.Phone2 = employee.Phone2;
            model.RoleId = employee.RoleId;
            model.RoleName = employee.RoleDisplayName;
            model.CurrentSalary = employee.CurrentSalary;
            model.PreviousSalary = employee.PreviousSalary;
            model.LastIncrementPercentage = employee.LastIncrementPercentage;
            model.JoinDate = employee.JoinDate;
            model.ImgURL = employee.ImgURL;
            model.BirthDate = employee.BirthDate;
            model.day = employee.BirthDate.Day;
            model.Month = employee.BirthDate.Month;
            model.year = employee.BirthDate.Year;
            model.Image = employee.Image;

            model.NumberOfDaysInMonth = DateTime.DaysInMonth(model.year, model.Month);

            return model;
        }
        public static void InsertNewEmployee(EmployeeUsersDetails employeeUser)
        {
            int userId = UserProfilesRepository.GetUserByUserName(employeeUser.UserName).UserId;
            Employee employee = new Employee();
            employee.CurrentSalary = employeeUser.CurrentSalary;
            employee.JoinDate = employeeUser.JoinDate;
            employee.UserId = userId;
            employee.CreateDate = DateTimeHelper.Today();
            if (employeeUser.RoleId == 0)
            {
                webpages_Roles role = RolesRepositories.GetEmployeeRole();
                employeeUser.RoleId = role.RoleId;
            }
           
            EmployeesRepositories.InsertNewEmployee(employee);
            RolesRepositories.CreateUser(userId, employeeUser.RoleId);
        }
        public static void UpdateEmployee(EditProfileModel employee)
        {
            DateTime todayDate = DateTimeHelper.Today();
            EmployeeUsersDetails model = new EmployeeUsersDetails
            {
                UserId = employee.UserId,
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Address = employee.Address,
                Phone1 = employee.Phone1,
                Phone2 = employee.Phone2,
                CurrentSalary = employee.CurrentSalary,
                PreviousSalary = employee.PreviousSalary,
                JoinDate = employee.JoinDate,
                ImgURL = employee.ImgURL,
                BirthDate = employee.BirthDate,
                day = employee.day,
                Month = employee.Month,
                year = employee.year,
                NumberOfDaysInMonth = employee.NumberOfDaysInMonth,
            };
            EmployeesRepositories.UpdateEmployee(model, todayDate);
        }
        public static void UpdateEmployee(UserData employee)
        {
            DateTime todayDate = DateTimeHelper.Today();
            EmployeeUsersDetails model = new EmployeeUsersDetails
            {
                UserId = employee.Id,
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Address = employee.Address,
                Phone1 = employee.Phone1,
                Phone2 = employee.Phone2,
                CurrentSalary = employee.CurrentSalary,
                PreviousSalary = employee.PreviousSalary,
                JoinDate = employee.JoinDate,
                ImgURL = employee.ProfilePictureUrl,
                BirthDate = employee.BirthDate,
            };
            EmployeesRepositories.UpdateEmployee(model, todayDate);
        }
        public static void DeleteEmployee(int userId)
        {
            EmployeesRepositories.DeleteEmployee(userId);
        }
        public static void LockEmployee(int userId)
        {
            EmployeesRepositories.LockEmployee(userId);
        }
        public static OnlineUserModel GetOnlineUsers()
        {

            DateTime todayDate = DateTimeHelper.Today();
            OnlineUserModel model = new OnlineUserModel();

            model.OnlineUsers = EmployeesRepositories.GetUsersSignIn(todayDate);
            model.NotComeUser = EmployeesRepositories.GetNotComeUser(todayDate);
            model.LeaveUser = EmployeesRepositories.GetUserLeaved(todayDate);
            model.Date = DateTimeHelper.Today();
            return model;
        }

    }
}