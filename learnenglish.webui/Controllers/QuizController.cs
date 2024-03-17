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
using System.Diagnostics;


namespace learnenglish.webui.Controllers
{
    public class QuizController:Controller
    {
        private IQuizRepository _quizRepository;
        private UserManager<User> _userManager;
        private ILevelRepository _levelRepository;
        public QuizController(IQuizRepository quizRepository, UserManager<User> userManager,ILevelRepository levelRepository)
        {
            _quizRepository=quizRepository;
            _userManager=userManager;
            _levelRepository=levelRepository;
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
        public async Task<IActionResult> ShowQuiz(int answer, int soruSirasi, int score){ // soruSayisi kullanıcıya döndürülecek olan soru 
        System.Console.WriteLine("Cevap doğru mu?: "+answer);
            if(answer==1){
                score++;
            }
            var user = await _userManager.GetUserAsync(User);
            var levelId = user.LevelId;
            var questions = _quizRepository.GetQuizzesByLevelId(levelId); //asp.net user'daki seviye kolonundan bu bilgiyi al ve metoda gönder
            soruSirasi++;
            if(soruSirasi!=questions.Count && soruSirasi!=20){ //20 soru ile de sınırladım
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
                System.Console.WriteLine("Toplam score: "+score);
                return RedirectToAction("AfterQuiz",new{
                    toplamScore=score,
                    soruSirasi=soruSirasi
                });
            }
        }
        [HttpGet]
        public IActionResult BeforeQuiz(){
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AfterQuiz(int toplamScore,int soruSirasi){

            //ToplamScore'a göre bir seviye belirle ve kullanıcının database kolonuna(levelId) atama burada yap 
            var user = await _userManager.GetUserAsync(User);
            var currentLevelId= user.LevelId;
            int levelId;
            string levelName;
            if(currentLevelId==1){ //Kullanıcı İlk defa sınava giriyorsa
            if(toplamScore<4){
                levelName="A1";
                levelId=2;
            }else if(toplamScore<7){
                levelName="A2";
                levelId=3;
            }else if(toplamScore<10){
                levelName="B1";
                levelId=4;

            }else if(toplamScore<13){
                levelName="B2";
                levelId=5;

            }else if(toplamScore<16){
                levelName="C1";
                levelId=6;

            }else{
                levelName="C2";
                levelId=7;
            }
            user.LevelId=levelId;
            user.IsBeginner=0;
            await _userManager.UpdateAsync(user);
            var model = new AfterQuizModel(){
                totalQuestion=soruSirasi,
                correctAnswer=toplamScore,
                incorrectAnswer=soruSirasi-toplamScore,
                levelName=levelName,
                Message="Tebrikler! Sınav aşamasını başarıyle geçtiniz. "+levelName+" seviyesinden artık eğitimlere başlayabilirsiniz!"
            };
            return View(model);
            }else{ //Kullanıcı ilk defa sınava girmiyorsa %50 üst başarılı ise bir üst seviyeye eğer değilse aynı seviyede kalacak
            if(toplamScore>(soruSirasi/2)){
                currentLevelId +=1;
                user.LevelId=currentLevelId;
                await _userManager.UpdateAsync(user);
                var model = new AfterQuizModel(){
                    totalQuestion=soruSirasi,
                    Message="Tebrikler! Bir sonraki dil seviyesine geçtiniz.",
                    IsUp=1,
                    incorrectAnswer=soruSirasi-toplamScore,
                    correctAnswer=toplamScore,
                    levelName=_levelRepository.GetLevelById(currentLevelId)
                };
                return View(model);
            }else{
                var model=new AfterQuizModel(){
                    totalQuestion=soruSirasi,
                    Message="Maalesef bu quizdeki sonuca göre bir sonraki eğitim seviyesine geçemediniz.",
                    IsUp=0,
                    incorrectAnswer=soruSirasi-toplamScore,
                    correctAnswer=toplamScore,
                    levelName=_levelRepository.GetLevelById(currentLevelId)
                };
                return View(model);
            }
            }
        }
    }
}