using DataMapping.Entities;
using DataMapping.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class SuggestionsRepositories
    {
        public static List<SuggestionUserDetails> GetSuggestionsList(int skipCount, int takeCount)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = from suggest in db.Suggestions
                        join user in db.UserProfiles
                        on suggest.SuggestByUserId equals user.UserId
                        where suggest.IsDeleted == false

                        select new SuggestionUserDetails()
                        {
                            Id = suggest.Id,
                            Name = user.LastName,
                            Title = suggest.Title,
                            Descreption = suggest.Description,
                            ImgUrl = user.ProfilePictureUrl

                        };
                return q.OrderByDescending(x => x.Id).Skip(skipCount).Take(takeCount).ToList();
            }
        }
        public static Suggestion GetSuggestionById(int id)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.Suggestions.SingleOrDefault(a => a.Id == id);
            }
        }
        public static void InsertNewSuggestion(Suggestion suggestion)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                db.Suggestions.Add(suggestion);
                db.SaveChanges();
            }
        }
        public static void UpdateSuggestion(Suggestion suggestion, DateTime today)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = db.Suggestions.SingleOrDefault(a => a.Id == suggestion.Id);
                if (q != null)
                {
                    q.Title = suggestion.Title;
                    q.Description = suggestion.Description;
                    db.SaveChanges();
                }
            }
        }
        public static void DeleteSuggestion(int id)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = db.Suggestions.SingleOrDefault(a => a.Id == id);
                if (q != null)
                {
                    q.IsDeleted = true;
                    db.SaveChanges();
                }
            }
        }
    }
}