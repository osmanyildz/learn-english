using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learnenglish.webui.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace learnenglish.webui.Controllers
{
    public class QuizController:Controller
    {

        public List<string> Sorular(){
            var sorular = new List<string>();
            sorular.Add("deneme sorusu 1");
            sorular.Add("deneme sorusu 2");
            sorular.Add("deneme sorusu 3");
            return sorular;
        }

        [HttpGet]
        public IActionResult ShowQuiz(){
            var soru = new ShowModel();
            var sorular = Sorular();
            soru.contents=sorular[0];
            soru.soruSirasi=0;
            return View(soru);
        }
        [HttpPost]
        public IActionResult ShowQuiz(int answer, int soruSirasi){ // soruSayisi kullanıcıya döndürülecek olan soru 
            System.Console.WriteLine(answer);
            var sorular = Sorular(); 
            //Data işlemleri gerekli çağırmalar burada yapılacak
            var model = new ShowModel();
            soruSirasi++;
            if(soruSirasi>=sorular.Count){
                return Redirect("/home/index");
            }
            model.contents=sorular[soruSirasi];
            model.soruSirasi=soruSirasi;
            System.Console.WriteLine("inci soru: " + soruSirasi);
            return View(model);
        }
        [HttpGet]
        public IActionResult BeforeQuiz(){
            return View();
        }
    }
}