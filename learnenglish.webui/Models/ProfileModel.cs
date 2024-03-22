using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learnenglish.webui.Models
{
    public class ProfileModel
    {
        public string Email { get; set; }
        public string LevelName { get; set; }
        public string UserName { get; set; }
        public string LastLessonName { get; set; }
        public List<string> LessonsNames { get; set; } // İlgili Seviyedeki derslerin adları
        public string ProfileUrl { get; set; }
    }
}