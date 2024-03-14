using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learnenglish.webui.Models
{
    public class QuizShowModel
    {
        public int id { get; set; }
        public int score { get; set; } // doğru bildiği soru sayısı
        public string question { get; set; }
        public List<AnswerShowModel> answerShowModels { get; set; }
        public int soruSirasi { get; set; }
        //Options'lardan doğru olanı isCorrect alanı 1 ise bunu value'suna yaz. Yani name'i answer ve value'su @Model.IsCorrect olsun  
    }
}