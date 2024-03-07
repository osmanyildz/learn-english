using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learnenglish.webui.Models
{
    public class ContentCreateModel
    {
        public string LessonTitle { get; set; }
        public int LevelId { get; set; }
        public string LessonContent { get; set; }
    }
}