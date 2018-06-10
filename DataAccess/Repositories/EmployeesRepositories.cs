using DataMapping.Entities;
using DataMapping.JSONData;
using DataMapping.Services;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DataAccess.Repositories
{
    public class EmployeesRepositories
    {
        public static List<EmployeeUsersDetails> GetEmployeesByRoleId(int roleId, int skipCount, int takeCount)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = (from emp in db.Employees
                         join user in db.UserProfiles
                            on emp.UserId equals user.UserId
                         where user.webpages_Roles.Any(r => r.RoleName == "Admin")

                         select new EmployeeUsersDetails()
                         {
                             EmployeeId = emp.Id,
                             UserId = user.UserId,
                             isLocked = user.LockedUser,
                             UserName = user.UserName,
                             FirstName = user.FirstName,
                             LastName = user.LastName,
                             Email = user.Email,
                             Phone1 = user.Phone1,
                             Phone2 = user.Phone2,
                             Address = user.Address,
                             RoleId = roleId,
                             CurrentSalary = emp.CurrentSalary,
                             PreviousSalary = emp.PreviousSalary,
                             LastIncrementPercentage = emp.LastIncrementPercentage,
                             JoinDate = emp.JoinDate,
                             ImgURL = user.ProfilePictureUrl,
                             BirthDate = user.BirthDate,
                             Image = user.ProfilePictureUrl
                         }).OrderBy(x => x.EmployeeId)
                        .Skip(skipCount)
                        .Take(takeCount)
                        .ToList();
                return q;
            }
        }
        public static List<EmployeeUsersDetails> GetEmployeesExceptAdmin(int skipCount, int takeCount)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = (from emp in db.Employees
                         join user in db.UserProfiles/*.Where(r => r.LockedUser == false)*/
                            on emp.UserId equals user.UserId
                         where user.webpages_Roles.Any(r => r.RoleName != "Admin")

                         select new EmployeeUsersDetails()
                         {
                             EmployeeId = emp.Id,
                             UserId = user.UserId,
                             isLocked = user.LockedUser,
                             UserName = user.UserName,
                             FirstName = user.FirstName,
                             LastName = user.LastName,
                             Email = user.Email,
                             Phone1 = user.Phone1,
                             Phone2 = user.Phone2,
                             Address = user.Address,
                             CurrentSalary = emp.CurrentSalary,
                             PreviousSalary = emp.PreviousSalary,
                             LastIncrementPercentage = emp.LastIncrementPercentage,
                             JoinDate = emp.JoinDate,
                             ImgURL = user.ProfilePictureUrl,
                             BirthDate = user.BirthDate,
                             Image = user.ProfilePictureUrl
                         }).OrderBy(x=>x.EmployeeId)
                        .Skip(skipCount) 
                        .Take(takeCount)
                        .ToList();
                return q;
            }
        }
        public static List<UserData> GetEmployeesExceptAdmin()
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = (from emp in db.Employees
                         join user in db.UserProfiles.Where(r => r.LockedUser == false)
                        on emp.UserId equals user.UserId
                         where user.webpages_Roles.Any(r => r.RoleName != "Admin")
                         select new UserData()
                         {
                             EmployeeId = emp.Id,
                             Id = user.UserId,
                             UserName = user.UserName,
                             FirstName = user.FirstName,
                             LastName = user.LastName,
                             Email = user.Email,
                             Phone1 = user.Phone1,
                             Phone2 = user.Phone2,
                             Address = user.Address,
                             CurrentSalary = emp.CurrentSalary,
                             PreviousSalary = emp.PreviousSalary,
                             JoinDate = emp.JoinDate,
                             ProfilePictureUrl = user.ProfilePictureUrl,
                             BirthDate = user.BirthDate,
                         })
                        .ToList();
                return q;
            }
        }
        public static List<UserHistoryDetails> GetUsersHistory(int skipCount, int takeCount)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var usersList = (from emp in db.Employees
                                 join user in db.UserProfiles.Where(r => r.LockedUser == false)
                                     on emp.UserId equals user.UserId
                                 where user.webpages_Roles.Any(r => r.RoleName != "Admin")

                                 select new UserHistoryDetails()
                                 {
                                     EmployeeId = emp.Id,
                                     UserId = user.UserId,
                                     UserName = user.UserName,
                                     FirstName = user.FirstName,
                                     LastName = user.LastName,
                                     CurrentSalary = emp.CurrentSalary,
                                     PreviousSalary = emp.PreviousSalary,
                                     LastIncrementPercentage = emp.LastIncrementPercentage,
                                     JoinDate = emp.JoinDate
                                 })
                                 .OrderBy(u => u.JoinDate)
                                 .Skip(skipCount)
                                 .Take(takeCount)
                                 .ToList();
                return usersList;
            }
        }
        public static List<UserBirthdayDetails> GetMonthlyBirthday(int month)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var usersList = (from user in db.UserProfiles.Where(r => r.LockedUser == false)
                                 where user.BirthDate.Month == month
                          && user.webpages_Roles.Any(r => r.RoleName != "Admin")


                                 select new UserBirthdayDetails()
                                 {
                                     UserId = user.UserId,
                                     UserName = user.UserName,
                                     BirthDate = user.BirthDate,
                                     ProfilePictureUrl = user.ProfilePictureUrl
                                 })
                                 .OrderBy(u => u.BirthDate)
                                 .ToList();
                return usersList;
            }
        }
        public static List<UserData> GetAllAdminEmoloyees()
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = (from emp in db.Employees
                         join user in db.UserProfiles.Where(r => r.LockedUser == false)
                         on emp.UserId equals user.UserId
                         where user.webpages_Roles.Any(r => r.RoleName != "Admin")
                         select new UserData()
                         {
                             EmployeeId = emp.Id,
                             Id = user.UserId,
                             UserName = user.UserName,
                             FirstName = user.FirstName,
                             LastName = user.LastName,
                             Email = user.Email,
                             Phone1 = user.Phone1,
                             Phone2 = user.Phone2,
                             Address = user.Address,
                             CurrentSalary = emp.CurrentSalary,
                             PreviousSalary = emp.PreviousSalary,
                             JoinDate = emp.JoinDate,
                             ProfilePictureUrl = user.ProfilePictureUrl,
                             BirthDate = user.BirthDate,
                         })
                        .ToList();
                return q;
            }
        }
        public static EmployeeUsersDetails GetEmployeeByUserName(string userName)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = (from emp in db.Employees
                         join user in db.UserProfiles
                           on emp.UserId equals user.UserId
                         from roles in db.webpages_Roles
                         where (user.UserName == userName && user.LockedUser == false)

                         select new EmployeeUsersDetails()
                         {
                             EmployeeId = emp.Id,
                             UserId = user.UserId,
                             UserName = user.UserName,
                             FirstName = user.FirstName,
                             LastName = user.LastName,
                             Email = user.Email,
                             Phone1 = user.Phone1,
                             Phone2 = user.Phone2,
                             Address = user.Address,
                             CurrentSalary = emp.CurrentSalary,
                             PreviousSalary = emp.PreviousSalary,
                             LastIncrementPercentage = emp.LastIncrementPercentage,
                             JoinDate = emp.JoinDate,
                             ImgURL = user.ProfilePictureUrl,
                             BirthDate = user.BirthDate,
                             RoleId = roles.RoleId,
                             RoleDisplayName = roles.DisplayName
                         })
                        .FirstOrDefault();
                return q;
            }
        }
        public static EmployeeUsersDetails GetEmployeeUserDetailsById(int id)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = (from emp in db.Employees
                         join user in db.UserProfiles.Where(x => x.LockedUser == false && x.UserId == id)
                            on emp.UserId equals user.UserId
                         from roles in db.webpages_Roles

                         select new EmployeeUsersDetails
                         {
                             EmployeeId = emp.Id,
                             FirstName = user.FirstName,
                             LastName = user.LastName,
                             UserName = user.UserName,
                             CurrentSalary = emp.CurrentSalary,
                             PreviousSalary = emp.PreviousSalary,
                             LastIncrementPercentage = emp.LastIncrementPercentage,
                             JoinDate = emp.JoinDate,
                             CreateDate = emp.CreateDate,
                             RoleId = roles.RoleId,
                             UserId = user.UserId,
                             RoleDisplayName = roles.DisplayName,
                             Phone1 = user.Phone1,
                             Phone2 = user.Phone2,
                             Email = user.Email,
                             BirthDate = user.BirthDate,
                             Address = user.Address,
                             ImgURL = user.ProfilePictureUrl,
                             Image = user.ProfilePictureUrl
                         }).FirstOrDefault();
                return q;
            }
        }
        public static ChangePasswordDetails GetEmployeePasswordDetails(int userId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = (from user in db.UserProfiles
                            .Where(a => a.UserId == userId &&
                            a.LockedUser == false)
                         select new ChangePasswordDetails()
                         {
                             Name = user.FirstName + " " + user.LastName,
                             UserName = user.UserName,
                         })
                        .FirstOrDefault();
                return q;
            }
        }
        public static void InsertNewEmployee(Employee employee)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                db.Employees.Add(employee);
                db.SaveChanges();
            }
        }
        public static void UpdateEmployee(EmployeeUsersDetails employeeUsersDetails, DateTime today)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                Employee employee = db.Employees
                    .FirstOrDefault(a => a.Id == employeeUsersDetails.EmployeeId);
                UserProfile userProfile = db.UserProfiles.FirstOrDefault(user => user.UserId == employeeUsersDetails.UserId);

                if (employee != null)
                {
                    employee.UpdateDate = today;
                    employee.PreviousSalary = employee.CurrentSalary;
                    employee.CurrentSalary = employeeUsersDetails.CurrentSalary;
                    if (employee.PreviousSalary != 0)
                    {
                        double incrementPercantages = ((employee.CurrentSalary - employee.PreviousSalary) / employee.PreviousSalary) * 100;
                        employee.LastIncrementPercentage = Convert.ToInt32(incrementPercantages);
                    }
                    else
                        employee.LastIncrementPercentage = 0;

                    employee.JoinDate = employee.JoinDate;
                    if (userProfile != null)
                    {
                        userProfile.ProfilePictureUrl = employeeUsersDetails.ImgURL;
                        userProfile.Email = employeeUsersDetails.Email;
                        userProfile.FirstName = employeeUsersDetails.FirstName;
                        userProfile.LastName = employeeUsersDetails.LastName;
                        userProfile.Phone1 = employeeUsersDetails.Phone1;
                        userProfile.Phone2 = employeeUsersDetails.Phone2;
                        userProfile.Address = employeeUsersDetails.Address;
                        userProfile.BirthDate = employeeUsersDetails.BirthDate;
                    }

                    db.SaveChanges();
                }
            }
        }
        public static void DeleteEmployee(int userId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                UserProfile userProfile = db.UserProfiles.SingleOrDefault(r => r.UserId == userId);
                if (userProfile != null)
                {
                    userProfile.LockedUser = true;
                    db.SaveChanges();
                }
            }
        }
        public static void LockEmployee(int userId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                UserProfile userProfile = db.UserProfiles.SingleOrDefault(r => r.UserId == userId);
                if (userProfile != null)
                {
                    userProfile.LockedUser = !userProfile.LockedUser;
                    db.SaveChanges();
                }
            }
        }
        public static List<SignedEmployee> GetUsersSignIn(DateTime today)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = (from user in db.UserProfiles
                         join attendance in db.Attendances
                         on user.UserId equals attendance.EmpUserId
                         where (attendance.SignOutDate == null
                          && attendance.SignInDate.Value == today
                          && user.LockedUser == false && attendance.IsDeleted == false
                          && user.webpages_Roles.FirstOrDefault().RoleName != "Admin")

                         select new SignedEmployee
                         {
                             Name = user.FirstName + " " + user.LastName,
                             SignInDate = attendance.SignInDate,
                             SignOutDate = attendance.SignOutDate,
                             Image = user.ProfilePictureUrl
                         })
                        .Distinct()
                        .ToList();
                return q;
            }
        }
        public static List<SignedEmployee> GetNotComeUser(DateTime today)
        {

            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = (from user in db.UserProfiles.Where(u => u.FirstName != null)


                         where (!db.Attendances.Any(a => a.EmpUserId == user.UserId
                          && a.SignInDate.Value == today
                          && user.LockedUser == false && a.IsDeleted == false)
                          && user.webpages_Roles.Any(r => r.RoleName != "Admin"))

                         select new SignedEmployee
                         {
                             Name = user.FirstName + " " + user.LastName,
                             Image = user.ProfilePictureUrl
                         })
                        .Distinct()
                        .ToList();
                return q;
            }
        }
        public static List<SignedEmployee> GetUserLeaved(DateTime today)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = (from user in db.UserProfiles
                         join attendance in db.Attendances
                         on user.UserId equals attendance.EmpUserId
                         where (attendance.SignOutDate.Value == today
                          && attendance.SignInDate.Value == today
                          && user.LockedUser == false && attendance.IsDeleted == false
                          && user.webpages_Roles.Any(r => r.RoleName != "Admin"))

                         select new SignedEmployee
                         {
                             Name = user.FirstName + " " + user.LastName,
                             SignOutDate = attendance.SignOutDate,
                             SignInDate = attendance.SignInDate,
                             Image = user.ProfilePictureUrl
                         })
                         .ToList();
                return q;
            }

        }
    }
}