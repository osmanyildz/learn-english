using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace learnenglish.webui.Identity
{
    public class User:IdentityUser
    {
        public int LevelId { get; set; }
        public int LastLessonId { get; set; }
        public int IsBeginner { get; set; } //Başlangıç sınavını geçmediyse(yani yeni kayıt olup seviye belirleme sınavına girmediyse) burası 0, eğer girdiyse burası 1 olacak. 
        public string ProfileUrl { get; set; }
    }
}