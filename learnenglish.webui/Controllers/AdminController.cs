using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using learnenglish.data.Abstract;
using learnenglish.entity;
using learnenglish.webui.Identity;
using learnenglish.webui.Models;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace learnenglish.webui.Controllers
{
    public class AdminController:Controller
    {
        private UserManager<User> _userManager;
        private IQuizRepository _quizRepository;
        private IAnswerRepository _answerRepository;
        private ILessonRepository _lessonRepository;
        public AdminController(IQuizRepository quizRepository, IAnswerRepository answerRepository, ILessonRepository lessonRepository,UserManager<User> userManager)
        {
            _quizRepository=quizRepository;
            _answerRepository=answerRepository;
            _lessonRepository=lessonRepository;
            _userManager=userManager;
        }

        [HttpGet]
        public IActionResult CreateContent(){
            return View();
        }
        [HttpPost]
        public IActionResult CreateContent(ContentCreateModel model){
            
            var entity = new Lesson(){
                LessonTitle=model.LessonTitle,
                LessonContent=model.LessonContent,
                LevelId=model.LevelId
            };
            _lessonRepository.CreateContent(entity);
            return RedirectToAction("LessonList");
        }
    [HttpGet]
    public IActionResult CreateQuiz(){
        return View();
    }
     [HttpPost]
    public IActionResult CreateQuiz(QuizCreateModel model){

        var quiz = new Quiz(){
            QuizContent = model.Question,
            levelId=model.levelId,
        };

        var liste = new List<string>();
        liste.Add(model.option1);
        liste.Add(model.option2);
        liste.Add(model.option3);
        liste.Add(model.option4);
        var answers = new List<Answer>();
        
        var i=1;

          foreach (var item in liste)
        {
            if(model.trueOptionId==i){
                var entity = new Answer(){
                    Option = item, 
                    IsCorrect=1
                };
            answers.Add(entity);
            }else{
                var entity = new Answer(){
                    Option = item, 
                    IsCorrect=0
                };
            answers.Add(entity);    
            }
            i++;
        }
        _quizRepository.CreateQuiz(quiz,answers);
        return RedirectToAction("QuizList");
    }
    [HttpGet]
        public IActionResult LessonList(){
            var lessons = _lessonRepository.GetAllLessons();
            var model = new List<LessonTitleModel>();
            foreach (var item in lessons)
            {
                model.Add(new LessonTitleModel(){
                    Id=item.Id,
                    LevelName=item.Level.LevelName,
                    Title=item.LessonTitle
                });
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult LessonEdit(int id){
            var lesson = _lessonRepository.GetLessonById(id);

            var model = new LessonEditModel(){
                Id = lesson.Id,
                Title=lesson.LessonTitle,
                LessonContent=lesson.LessonContent,
                // levelId=lesson.LevelId
            };
            ViewBag.levelId = lesson.LevelId;
            return View(model);
        }
        [HttpPost]
        public IActionResult LessonEdit(LessonEditModel lessonEditModel, int levelId){
            // System.Console.WriteLine("Buraya geldi");
            // System.Console.WriteLine(lessonEditModel.Id);
            // System.Console.WriteLine(lessonEditModel.Title);
            // System.Console.WriteLine("------------------"+lessonEditModel.Id);
            // System.Console.WriteLine(levelId);
            var entity = new Lesson(){
                Id=lessonEditModel.Id,
                LessonTitle=lessonEditModel.Title,
                LevelId=levelId,
                LessonContent=lessonEditModel.LessonContent
            };
            _lessonRepository.UpdateLesson(entity);
            return RedirectToAction("LessonList");
        }
        public IActionResult LessonDelete(int id){
            var lesson = _lessonRepository.GetLessonById(id);
            _lessonRepository.DeleteLesson(lesson);
            return RedirectToAction("LessonList");
        }
        [HttpGet]
        public IActionResult QuizList(){
            var quizzes = _quizRepository.GetAllQuizzes();
            var models = new List<QuizListModel>();
            foreach (var item in quizzes)
            {
                models.Add(new QuizListModel(){
                    Id=item.Id,
                    Question=item.QuizContent,
                    LevelName=item.Level.LevelName
                });
            }
            // foreach (var item in quizzes)
            // {
            //     System.Console.WriteLine(item.Id);
            //     System.Console.WriteLine(item.QuizContent);
            //     System.Console.WriteLine(item.levelId);
            //     foreach (var answer in item.Answers)
            //     {
            //         System.Console.WriteLine(answer.Option);
            //     }
            // }
            return View(models);
        }
    [HttpGet]
        public IActionResult QuizEdit(int id){
            var quiz = _quizRepository.GetQuizById(id);
            var model = new QuizEditModel(){
                question=quiz.QuizContent,
                quizId=quiz.Id,
                levelId=quiz.levelId
            };
            model.answerEditModels = new List<AnswerEditModel>();
            foreach (var item in quiz.Answers){
            model.answerEditModels.Add(new AnswerEditModel(){
                optionId=item.Id,
                option=item.Option,
                isCorrect=item.IsCorrect
            });
            }
            return View(model);
        }
        
        [HttpPost]
        public IActionResult QuizEdit(QuizEditModel model, int isCorrect){

            // System.Console.WriteLine(model.quizId);
            // System.Console.WriteLine(model.question);
            // System.Console.WriteLine(model.levelId);
            System.Console.WriteLine("**************************************");
            System.Console.WriteLine(model.levelId);
            foreach (var item in model.answerEditModels)
            {
                // System.Console.WriteLine(item.option);
                // System.Console.WriteLine(item.optionId);
                // System.Console.WriteLine(item.optionId);
                // System.Console.WriteLine("IsCorrect Burada : "+ isCorrect);
            }
            System.Console.WriteLine(isCorrect);
            var quiz = new Quiz(){
                Id=model.quizId,
                QuizContent=model.question,
                levelId=model.levelId
            };
            var answers = new List<Answer>();
            foreach (var item in model.answerEditModels)
            {
                var answer = new Answer(){
                    Id=item.optionId,
                    Option=item.option,
                    QuizId=model.quizId,
                };
                if(item.optionId==isCorrect){
                    answer.IsCorrect=1;
                }else{
                    answer.IsCorrect=0;
                }
                
                answers.Add(answer);
            }
            _quizRepository.UpdateQuiz(quiz,answers);
            return RedirectToAction("QuizList");
        }
        public IActionResult QuizDelete(int id){

            _quizRepository.QuizDelete(id);
            return RedirectToAction("QuizList");

        }
        public IActionResult Statistics(){
            var usersA1 =  _userManager.Users.Where(u=>u.LevelId==2).ToList();
            var usersA2 =  _userManager.Users.Where(u=>u.LevelId==3).ToList();
            var usersB1 =  _userManager.Users.Where(u=>u.LevelId==4).ToList();
            var usersB2 =  _userManager.Users.Where(u=>u.LevelId==5).ToList();
            var usersC1 =  _userManager.Users.Where(u=>u.LevelId==6).ToList();
            var usersC2 =  _userManager.Users.Where(u=>u.LevelId==7).ToList();
            
            var lessonA1 = _lessonRepository.GetAllLessons().Where(l=>l.LevelId==2).ToList();
            var lessonA2 = _lessonRepository.GetAllLessons().Where(l=>l.LevelId==3).ToList();
            var lessonB1 = _lessonRepository.GetAllLessons().Where(l=>l.LevelId==4).ToList();
            var lessonB2 = _lessonRepository.GetAllLessons().Where(l=>l.LevelId==5).ToList();
            var lessonC1 = _lessonRepository.GetAllLessons().Where(l=>l.LevelId==6).ToList();
            var lessonC2 = _lessonRepository.GetAllLessons().Where(l=>l.LevelId==7).ToList();
            
            var quizBeginner = _quizRepository.GetAllQuizzes().Where(q=>q.levelId==1).ToList();
            var quizA1 = _quizRepository.GetAllQuizzes().Where(q=>q.levelId==2).ToList();
            var quizA2 = _quizRepository.GetAllQuizzes().Where(q=>q.levelId==3).ToList();
            var quizB1 = _quizRepository.GetAllQuizzes().Where(q=>q.levelId==4).ToList();
            var quizB2 = _quizRepository.GetAllQuizzes().Where(q=>q.levelId==5).ToList();
            var quizC1 = _quizRepository.GetAllQuizzes().Where(q=>q.levelId==6).ToList();
            var quizC2 = _quizRepository.GetAllQuizzes().Where(q=>q.levelId==7).ToList();

            var activeUsers = _userManager.Users.Where(u=>u.EmailConfirmed==true).ToList();
            var InactiveUsers = _userManager.Users.Where(u=>u.EmailConfirmed==false).ToList();

            System.Console.WriteLine(lessonA1.Count);
            var model = new StatisticsModel(){
                A1PersonQuantity=usersA1.Count,
                A2PersonQuantity=usersA2.Count,
                B1PersonQuantity=usersB1.Count,
                B2PersonQuantity=usersB2.Count,
                C1PersonQuantity=usersC1.Count,
                C2PersonQuantity=usersC2.Count,
                A1LessonQuantity=lessonA1.Count,
                A2LessonQuantity=lessonA2.Count,
                B1LessonQuantity=lessonB1.Count,
                B2LessonQuantity=lessonB2.Count,
                C1LessonQuantity=lessonC1.Count,
                C2LessonQuantity=lessonC2.Count,
                BeginnerQuizQuantity=quizBeginner.Count,
                A1QuizQuantity=quizA1.Count,
                A2QuizQuantity=quizA2.Count,
                B1QuizQuantity=quizB1.Count,
                B2QuizQuantity=quizB2.Count,
                C1QuizQuantity=quizC1.Count,
                C2QuizQuantity=quizC2.Count,
                ActiveUserQuantity=activeUsers.Count,
                InactiveUserQuantity=InactiveUsers.Count
            };
            return View(model);
        }
        public IActionResult Showing(){
            return View();
        }
    }
}
