using BusinessLogic.Helpers;
using BusinessLogic.Model;
using DataAccess.Repositories;
using DataMapping.Entities;
using DataMapping.JSONData;
using DataMapping.Services;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Core
{
    public class UserProfilesLogic
    {


        public static UserProfile GetUserByUserName(string userName)
        {
            return UserProfilesRepository.GetUserByUserName(userName);

        }

        public static EmployeeUsersDetails GetCreateModel(int id)
        {
            EmployeeUsersDetails model = new EmployeeUsersDetails();
            webpages_Roles role = RolesRepositories.GetRoleById(id);
            if (role != null)
                model.RoleDisplayName = role.DisplayName;

            model.RoleId = id;
            model.year = 1950;
            model.Month = 1;
            model.day = 1;

            model.NumberOfDaysInMonth = DateTime.DaysInMonth(model.year, model.Month);
            model.ImgURL = "";
            model.JoinDate = DateTimeHelper.Today();
            return model;
        }
        public static void UpdateUserProfile(UserProfile userProfile)
        {
            UserProfilesRepository.UpdateUserProfile(userProfile);
        }
        public static UserProfile GetUserById(int userId)
        {
            return UserProfilesRepository.GetUserByUserId(userId);
        }
        public static UserData GetUserDataByUserId(int userId)
        {
            DateTime serverTime = DateTimeHelper.Today();
            return UserProfilesRepository.GetUserDataByUserId(userId, serverTime);
        }

        public static List<string> GetUserNamesNotInProjectByTerm(string term, int projectId)
        {
            return UserProfilesRepository.GetUserNamesInProjectByTerm(term, projectId);
        }
    }
}