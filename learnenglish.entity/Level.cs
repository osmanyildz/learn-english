using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace learnenglish.entity
{
    public class Level
    {
        public int Id { get; set; }
        public string LevelName { get; set; }
        public List<Lesson> Lessons { get; set; } // nav prop
        public List<Quiz> Quizzes { get; set; }

    }
}