using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learnenglish.entity
{
    public class Answer
    {
        public int Id { get; set; }
        public string Option { get; set; }
        public Quiz Quiz { get; set; }
        public int QuizId { get; set; }
        public int IsCorrect { get; set; }
    }
}