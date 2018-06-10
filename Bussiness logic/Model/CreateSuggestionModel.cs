using DataMapping.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class CreateSuggestionModel
    {
        public List<Project> Projects { get; set; }
        public Suggestion Suggestion { get; set; }
    }
}
