using DataMapping.Services;
using System;
using System.Collections.Generic;
using DataAccess.Repositories;
using DataMapping.Entities;
using BusinessLogic.Helpers;
using BusinessLogic.Model;

namespace BusinessLogic.Core
{
    public class SuggestionsLogic
    {
        public static List<SuggestionUserDetails> GetSuggestionsList(int page)
        {
            int takeCount = Config.PageItemCount();
            int skipCount = page * takeCount;
            return SuggestionsRepositories.GetSuggestionsList( skipCount, takeCount);
        }
        public static Suggestion GetSuggestionById(int id)
        {
            return SuggestionsRepositories.GetSuggestionById(id);
        }        
        public static CreateSuggestionModel GetCreateSuggestionModel()
        {
            CreateSuggestionModel model = new CreateSuggestionModel();
            model.Projects = ProjectsRepositories.GetAllProjects();
            model.Projects.Insert(0, (new Project { Name = "General", Id = 0 }));
            return model;
        }       
        public static CreateSuggestionModel GetEditSuggestionModel(int suggestionId)
        {
            CreateSuggestionModel model = new CreateSuggestionModel();
            model.Projects = ProjectsRepositories.GetAllProjects();
            model.Projects.Insert(0, (new Project { Name = "General", Id = 0 }));
            model.Suggestion = SuggestionsRepositories.GetSuggestionById(suggestionId);
            return model;
        }
        public static CreateSuggestionModel UpdateProjectsList(CreateSuggestionModel model)
        {
            model.Projects = ProjectsRepositories.GetAllProjects();
            model.Projects.Insert(0, (new Project { Name = "General", Id = 0 }));
            return model;
        }
        public static void InsertNewSuggestion(Suggestion suggestion)
        {
            
            suggestion.CreateDate = DateTimeHelper.Today();                
            SuggestionsRepositories.InsertNewSuggestion(suggestion);
        }
        public static void UpdateSuggestion(Suggestion suggestion)
        {
            DateTime myDate = DateTimeHelper.Today();
            
            SuggestionsRepositories.UpdateSuggestion(suggestion, myDate);
        }
        public static void DeleteSuggestion(int id)
        {
            SuggestionsRepositories.DeleteSuggestion(id);
        }
       
    }
}