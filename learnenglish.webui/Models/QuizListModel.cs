using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learnenglish.webui.Models
{
    public class QuizListModel
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string LevelName { get; set; }
    }
}