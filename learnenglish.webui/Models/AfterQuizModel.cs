using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learnenglish.webui.Models
{
    public class AfterQuizModel
    {
        public int totalQuestion { get; set; }
        public int correctAnswer { get; set; }
        public int incorrectAnswer { get; set; }
        public string levelName { get; set; }
        public string? Message { get; set; }
        public int? IsUp { get; set; }
    }
}