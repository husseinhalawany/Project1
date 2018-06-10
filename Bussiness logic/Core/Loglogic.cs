using System;
using BusinessLogic.Model;
using DataAccess.Repositories;
using DataMapping.Entities;
using BusinessLogic.Helpers;

namespace BusinessLogic.Core
{
    public class LogsLogic
    {
        public static string URL, FileName;      

        public static void InsertLog(Log log)
        {
            DateTime myDate = DateTimeHelper.Today();
            LogsRepository.InsertLog(log, myDate);
        }
        public static void DeleteLogById(int logId)
        {
            LogsRepository.DeleteLogById(logId);
        }
        public static LogIndexModel GetLogIndexModel()
        {
            LogIndexModel model = new LogIndexModel()
            {
                LogCount = LogsRepository.GetLogCount()
            };
            if (model.LogCount > 0)
            {
                model.Log = LogsRepository.GetFirstOrDefaultLog();
                model.CountStoryLog = LogsRepository.GetLogCountByStoryName(model.Log.StoryName);
            }
            model.Succeeded = true;
            return model;
        }
        public static void DeleteLogsByStoryName(string storyName)
        {
            if (storyName == null)
                storyName = "";
            LogsRepository.DeleteLogsByStoryName(storyName);
        }
    }
}
