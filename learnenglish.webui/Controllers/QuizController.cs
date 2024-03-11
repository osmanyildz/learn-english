using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learnenglish.data.Abstract;
using learnenglish.webui.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace learnenglish.webui.Controllers
{
    public class QuizController:Controller
    {
        private IQuizRepository _quizRepository;
        public QuizController(IQuizRepository quizRepository)
        {
            _quizRepository=quizRepository;
        }
        public List<string> Sorular(){
            var sorular = new List<string>();
            sorular.Add("deneme sorusu 1");
            sorular.Add("deneme sorusu 2");
            sorular.Add("deneme sorusu 3");
            return sorular;
        }

        [HttpGet]
        public IActionResult ShowQuiz(){
            // var soru = new ShowModel();
            // var sorular = Sorular();
            // soru.contents=sorular[0];
            // soru.soruSirasi=0;
            // return View(soru);
            // var soru = new QuizShowModel();
            // soru.soruSirasi=1;
            // soru.question=sorular[0];
            // soru.id=0;
            // var answerModels = new List<AnswerShowModel>();
            // answerModels.Add(new AnswerShowModel(){
            //     isCorrect=0,
            //     option="1-- Bu birinci şık için olan option"
            // });
            // answerModels.Add(new AnswerShowModel(){
            //     isCorrect=1,
            //     option="2-- Bu ikinci şık için olan option--Doğru"
            // });
            //  answerModels.Add(new AnswerShowModel(){
            //     isCorrect=1,
            //     option="3-- Bu üçüncü şık için olan option"
            // });
            //  answerModels.Add(new AnswerShowModel(){
            //     isCorrect=1,
            //     option="4-- Bu dördüncü şık için olan option"
            // });
            var questions = _quizRepository.GetQuizzesByLevelId(3); //asp.net user'daki seviye kolonundan bu bilgiyi al ve metoda gönder
            var question = questions[0];
            var model = new QuizShowModel();
            model.soruSirasi=1;
            model.id=question.Id;
            model.question=question.QuizContent;
            var answerList = new List<AnswerShowModel>();
            foreach (var item in question.Answers)
            {
                var answer = new AnswerShowModel(){
                    isCorrect=item.IsCorrect,
                    option=item.Option
                };
                answerList.Add(answer);
            }
            model.answerShowModels=answerList;
            return View(model);
        }
        
        [HttpPost]
        public IActionResult ShowQuiz(int answer, int soruSirasi){ // soruSayisi kullanıcıya döndürülecek olan soru 
            // System.Console.WriteLine(answer);
            // var sorular = Sorular(); 
            //Data işlemleri gerekli çağırmalar burada yapılacak
            // var model = new ShowModel();
            // soruSirasi++;
            // if(soruSirasi>=sorular.Count){
            //     return Redirect("/home/index");
            // }
            // model.contents=sorular[soruSirasi];
            // model.soruSirasi=soruSirasi;
            // System.Console.WriteLine("inci soru: " + soruSirasi);
            System.Console.WriteLine(answer);
            System.Console.WriteLine(soruSirasi);
            return Redirect("/Home/Index");
        }
        [HttpGet]
        public IActionResult BeforeQuiz(){
            return View();
        }
    }
}