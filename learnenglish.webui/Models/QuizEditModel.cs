using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learnenglish.webui.Models
{
    public class QuizEditModel
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public List<AnswerEditModel> Answers { get; set; }
        public QuizEditModel()
        {
            Answers = new List<AnswerEditModel>
            {
                new AnswerEditModel(),
                new AnswerEditModel(),
                new AnswerEditModel(),
                new AnswerEditModel()
            };
        }
    }
}