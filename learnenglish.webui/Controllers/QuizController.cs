using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learnenglish.data.Abstract;
using learnenglish.webui.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity;
using learnenglish.webui.Identity;


namespace learnenglish.webui.Controllers
{
    public class QuizController:Controller
    {
        private IQuizRepository _quizRepository;
        private UserManager<User> _userManager;
        public QuizController(IQuizRepository quizRepository, UserManager<User> userManager)
        {
            _quizRepository=quizRepository;
            _userManager=userManager;
        }
        public List<string> Sorular(){
            var sorular = new List<string>();
            sorular.Add("deneme sorusu 1");
            sorular.Add("deneme sorusu 2");
            sorular.Add("deneme sorusu 3");
            return sorular;
        }

        [HttpGet]
        public async Task<IActionResult> ShowQuiz(){
            var user = await _userManager.GetUserAsync(User);
            if(user!=null){

            var questions = _quizRepository.GetQuizzesByLevelId(user.LevelId); //asp.net user'daki seviye kolonundan bu bilgiyi al ve metoda gönder
            var question = questions[0];
            var model = new QuizShowModel();
            model.soruSirasi=0;
            model.id=question.Id;
            model.question=question.QuizContent;
            model.score=0;
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
            }else{
                return Redirect("/Home/Index");
            }
        }
        
        [HttpPost]
        public IActionResult ShowQuiz(int answer, int soruSirasi, int score){ // soruSayisi kullanıcıya döndürülecek olan soru 
            if(answer==1){
                score++;
                
            }
            var questions = _quizRepository.GetQuizzesByLevelId(3); //asp.net user'daki seviye kolonundan bu bilgiyi al ve metoda gönder
            soruSirasi++;
            if(soruSirasi!=questions.Count){
            var question = questions[soruSirasi];
            var model = new QuizShowModel();
            System.Console.WriteLine(soruSirasi);
            model.soruSirasi=soruSirasi;
            model.id=question.Id;
            model.score=score;
            model.question=question.QuizContent;
            var answerList = new List<AnswerShowModel>();
            foreach (var item in question.Answers)
            {
                var answerModel = new AnswerShowModel(){
                    isCorrect=item.IsCorrect,
                    option=item.Option
                };
                answerList.Add(answerModel);
            }
            model.answerShowModels=answerList;
            return View(model);
            }else{
//Score'u burada en son AfterQuiz'e gönder alttaki Redirect'e AfterQuiz yönlendirmesini yap
                return Redirect("/Quiz/AfterQuiz");
            }
        }
        [HttpGet]
        public IActionResult BeforeQuiz(){
            return View();
        }

        [HttpGet]
        public IActionResult AfterQuiz(){
            return View();
        }
    }
}