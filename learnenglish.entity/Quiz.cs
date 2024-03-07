using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learnenglish.entity
{
    public class Quiz
    {
        public int Id { get; set; }
        public string QuizContent { get; set; }
        public List<Answer> Answers { get; set; }
        public int levelId { get; set; } 
        public Level Level { get; set; }

    }
}