using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learnenglish.webui.Models
{
    public class QuizEditModel
    {
        public int quizId { get; set; }
        public string question { get; set; }
        public List<AnswerEditModel> answerEditModels { get; set; }
        public int levelId { get; set; }
    }
}