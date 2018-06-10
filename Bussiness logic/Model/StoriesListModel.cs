using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMapping.Entities;
using DataMapping.Services;

namespace BusinessLogic.Model
{
    public class StoriesListModel
    {
        public int StoriesCount { get; set; }
        public List<StoriesDetails> Stories { get; set; }
    }
}