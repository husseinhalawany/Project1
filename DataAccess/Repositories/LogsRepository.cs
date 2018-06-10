using System.Collections.Generic;
using System.Linq;
using DataMapping.Entities;
using System.Data.Entity;
using DataMapping.Services;
using DataMapping.JSONData;
using System;

namespace DataAccess.Repositories
{
    public class LogsRepository
    {
        public static void InsertLog(Log log,DateTime myDate)
        {

            
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                log.DateTime = DateTime.Now ;
               
                db.Logs.Add(log);
                db.SaveChanges();
            }
        }
        public static Log GetFirstOrDefaultLog()
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                Log logData = db.Logs.OrderBy(x => x.StoryName).FirstOrDefault();
                return logData;
            }
        }
        public static void DeleteLogsByStoryName(string storyName)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                db.Logs.RemoveRange(db.Logs.Where(a => a.StoryName == storyName));
                db.SaveChanges();
            }
        }
        public static int GetLogCountByStoryName(string storyName)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.Logs.Where(x => x.StoryName == storyName).Count();
            }
        }
        public static int GetLogCount()
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.Logs.Count();
            }
        }
        public static void DeleteLogById(int logId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                Log log = db.Logs.FirstOrDefault(a => a.Id == logId);
                if (log != null)
                {
                    db.Logs.Remove(log);
                    db.SaveChanges();
                }
            }
        }

    }
}