using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learnenglish.webui.Models
{
    public class AnswerEditModel
    {
        public int optionId { get; set; }
        public string option { get; set; }
        public int isCorrect { get; set; }
    }
}