using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learnenglish.webui.Models
{
    public class LessonEditModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string LessonContent { get; set; }
        // public int levelId { get; set; }
    }
}