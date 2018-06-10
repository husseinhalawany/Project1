using DataMapping.Entities;
using DataMapping.Interfaces;

namespace BusinessLogic.Model
{
    public class LogIndexModel : IResult
    {
        public Log Log { get; set; }
        public int LogCount { get; set; }
        public int CountStoryLog { get; set; }
        public string ErrorMessage { get; set; }
        public bool Succeeded { get; set; }
    }
}
