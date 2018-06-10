using DataAccess.Repositories;
using DataMapping.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using BusinessLogic.Model;
using BusinessLogic.Helpers;
using DataMapping.Services;
namespace BusinessLogic.Core
{
    public static class StoriesLogic
    {
        public static List<Story> GetStoriesInProject(int projectId)
        {
            return StoriesRepositories.GetAllStoriesList(projectId);
        }
        public static List<StoriesDetails> GetStoriesList(int projectId)
        {
            return StoriesRepositories.GetStoriesList(projectId);
        }
        public static List<StoriesDetails> GetStoriesListAndItemCount(int projectId, int sprintId)
        {
            if (projectId == 0) return StoriesRepositories.GetSprintStoriesListAndItemCount(sprintId);
            return StoriesRepositories.GetStoriesListAndItemCount(projectId, sprintId);
        }
        public static bool InsertStorySprint(int projectId, int sprintId, string storyName, int userId)
        {
            return StoriesRepositories.InsertStorySprint(projectId, sprintId, storyName, userId);
        }
        public static List<StoriesDetails> GetFilteredStoriesList(StoriesFilter storiesFilter)
        {
            List<StoriesDetails> storiesList = new List<StoriesDetails>();
            bool selectAnyFilterOption = false;
            if (storiesFilter.sprintId != 0)
            {
                selectAnyFilterOption = true;
                storiesList = storiesFilter.AllStories.Where(t => t.Sprints.Any(s => s.Id == storiesFilter.sprintId)).ToList();
            }
            if (!selectAnyFilterOption)
            {
                storiesList = storiesFilter.AllStories;
            }
            if (!string.IsNullOrEmpty(storiesFilter.SearchText))
            {
                storiesList = storiesList.Where(s => s.Name.ToLower().Contains(storiesFilter.SearchText.ToLower())).ToList();
            }
            if (storiesFilter.OrderedByName)
            {
                storiesList = storiesList.OrderBy(s => s.Name).ToList();
            }


            return storiesList;
        }
        public static List<string> AutocompleteStories(int projectId, int sprintId)
        {
            return StoriesRepositories.AutocompleteStories(projectId, sprintId);
        }
        public static List<string> AutocompleteStoriesForAdd(int projectId)
        {
            return StoriesRepositories.AutocompleteStoriesForAdd(projectId);
        }
        public static Story GetStoryById(int id)
        {
            return StoriesRepositories.GetStoryById(id);
        }
        public static StoriesIndexModel GetStoriesIndexModel(StoriesFilter storiesFilter, int projectId)
        {

            Project project = new Project();
            StoriesIndexModel model = new StoriesIndexModel
            {
                projectId = projectId,                
                OrderedByName = storiesFilter.OrderedByName,
                Reviewed = storiesFilter.Reviewed,
                SearchText = storiesFilter.SearchText
            };
            if (projectId != 0)
            {
                project = ProjectsRepositories.GetProjectById(projectId);
                model.projectName = project.Name;

            }
            return model;
        }
        public static StoriesDetails InsertNewStory(Story story)
        {
            return StoriesRepositories.InsertNewStory(story);
        }
        public static void UpdateStory(Story story)
        {
            DateTime myDate = DateTimeHelper.Today();
            StoriesRepositories.UpdateStory(story, myDate);
        }
        public static void DeleteStoriesList(List<StoriesDetails> stories)
        {
            List<int> storiesIds = new List<int>();
            foreach (var item in stories)
            {
                storiesIds.Add(item.Id);
            }
            StoriesRepositories.DeleteAllStory(storiesIds);
        }
        public static void FinishCodeReview(int id)
        {
            StoriesRepositories.FinishCodeReview(id);
        }
        public static void DeleteStory(int id)
        {
            StoriesRepositories.DeleteStory(id);
        }
    }
}