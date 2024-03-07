using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learnenglish.webui.Models
{
    public class AnswerEditModel
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public int IsCorrect { get; set; }
    
    }
}