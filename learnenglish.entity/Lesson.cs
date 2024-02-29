using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learnenglish.entity
{
    public class Lesson
    {
        public int Id { get; set; }
        public string LessonTitle { get; set; }
        public string LessonContent { get; set; }
        public int LevelId { get; set; } // FK
        public Level Level { get; set; } // Nav prop
    }
}