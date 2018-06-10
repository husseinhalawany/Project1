using DataMapping.Entities;
using System.Collections.Generic;
using System.Linq;
using DataMapping.Enums;
using System;
using DataMapping.JSONData;

namespace DataAccess.Repositories
{
    public class UserProfilesRepository
    {
        public static List<UserProfile> GetNotAdminUsers()
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = (from user in db.UserProfiles
                       where user.webpages_Roles.Any(r => r.RoleName != "Admin")

                         select (user))
                        .ToList();
                return q;
            }
        }
        public static List<string> GetAllUsersNamesStartingWithTerm(string term)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.UserProfiles
                        .Where(x => x.UserName.StartsWith(term) &&
                            x.LockedUser == false)
                        .Select(x => x.UserName)
                        .ToList();
            }
        }
        public static UserProfile GetUserByUserName(string userName)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.UserProfiles.FirstOrDefault(
                    x => x.UserName == userName &&
                    x.LockedUser == false);
            }
        }
        public static UserProfile GetUserByUserId(int id)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.UserProfiles.FirstOrDefault(u => u.UserId == id);
            }
        }
        public static UserData GetUserDataByUserId(int id, DateTime serverTime)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = (from user in db.UserProfiles.Where(u => u.UserId == id)
                        
                        join employee in db.Employees 
                            on user.UserId equals employee.UserId
                        join attendance in db.Attendances
                                .Where(a => a.SignInDate.Value.Date == serverTime.Date &&
                                    a.IsDeleted == false)
               on user.UserId equals attendance.EmpUserId
                    where user.webpages_Roles.Any(r => r.RoleName != "Admin")


                         select new UserData
                        {
                            Id = user.UserId,
                            UserName = user.UserName,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            Address = user.Address,
                            Phone1 = user.Phone1,
                            Phone2 = user.Phone2,
                            ProfilePictureUrl = user.ProfilePictureUrl,
                            Status = user.Status.GetValueOrDefault(),
                            CurrentSalary = employee.CurrentSalary,
                            PreviousSalary = employee.PreviousSalary,
                            JoinDate = employee.JoinDate,
                            IsSignedIn = AttendancesRepositories.IsUserSignedIn(id, serverTime),
                            SignInDate = attendance.SignInDate,
                            SignOutDate = attendance.SignOutDate,
                            IsSentStandUpMeeting = StandUpMeetingRepository.IsUserSentStandUpMeeting(id, serverTime),

                        })
                        .FirstOrDefault();
                return q;
            }
        }
        public static List<string> GetUserNamesInProjectByTerm(string term, int projectId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                List<UserProfile> userInProject = (from user in db.UserProfiles
                                                   join projectUser in db.ProjectUsers
                                                   on user.UserId equals projectUser.UserId
                                                   where projectUser.ProjectId == projectId
                                                   select user).ToList();
                List<UserProfile> users = (from user in db.UserProfiles

                                           where user.webpages_Roles.Any(r => r.RoleName != "Admin")
                                           && user.LockedUser == false
                                           && (
                                           user.UserName.ToLower().Contains(term.ToLower()) 
                                           || user.FirstName.ToLower().Contains(term.ToLower()) 
                                           || user.LastName.ToLower().Contains(term.ToLower())
                                           )
                                           select user).ToList();
                List<UserProfile> userNotInProject = users.Except(userInProject).ToList();
                List<string> usernameNotInProject = userNotInProject.Select(q=>q.UserName).ToList();
                return usernameNotInProject;
            }
        }
        public static void UpdateUserProfile(UserProfile userProfile)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = db.UserProfiles.FirstOrDefault(u => u.UserId == userProfile.UserId);
                if (q != null)
                {
                    q.FirstName = userProfile.FirstName;
                    q.LastName = userProfile.LastName;
                    q.Email = userProfile.Email;
                    q.Address = userProfile.Address;
                    q.Phone2 = userProfile.Phone2;
                    q.Phone1 = userProfile.Phone1;
                    q.BirthDate = userProfile.BirthDate;
                    q.ProfilePictureUrl = userProfile.ProfilePictureUrl;
                    db.SaveChanges();
                }
            }

        }
        public static string GetUserPassword(string userName)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = (from user in db.UserProfiles
                         join Membership in db.webpages_Membership on user.UserId equals Membership.UserId
                         where (user.UserName == userName)
                         select Membership.Password)
                         .FirstOrDefault();
                return q;
            }
        }
    }
}