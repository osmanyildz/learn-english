using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learnenglish.data.Abstract;
using learnenglish.entity;
using learnenglish.webui.Identity;
using learnenglish.webui.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace learnenglish.webui.Controllers
{
    public class LessonController:Controller
    {
        private ILessonRepository _lessonRepository;
        private UserManager<User> _userManager;
        public LessonController(ILessonRepository lessonRepository, UserManager<User> userManager)
        {
            _lessonRepository=lessonRepository;
            _userManager=userManager;
           
        }
       
        [HttpGet]
        public async Task<IActionResult> LessonContent(int id=0){
            
            var user = await _userManager.GetUserAsync(User);
            var levelId=user.LevelId;
            if(levelId==1){
                return RedirectToAction("LevelInformation");
            }
            var titleModel = new List<LessonTitleModel>();
            if(user!=null){

            var lessons = _lessonRepository.GetLessonsByLevelId(user.LevelId);
            var i=0;
            System.Console.WriteLine(lessons.ElementAt(0).LessonContent);

            foreach (var item in lessons)
            {
                titleModel.Add(new LessonTitleModel(){
                    Id = item.Id,
                    Title = item.LessonTitle,
                });
              if(id == 0 && i==0 && user.LastLessonId==0){
                ViewBag.LessonContent = new LessonContentModel(){
                    Id=item.Id,
                    Content=item.LessonContent
                };
                i++;
              }else if(id == 0 && user.LastLessonId !=0){
                if(user.LastLessonId==item.Id){
                    ViewBag.LessonContent = new LessonContentModel(){
                        Id=item.Id,
                        Content=item.LessonContent
                    };
                    user.LastLessonId=item.Id;
                    await _userManager.UpdateAsync(user);
                }
              }
            }
            
//Burada kalınan son dersin id'sine göre bir kontrol yap ilk başta navbardaki eğitim içeriğine tıkladığında onun kontrolünü yap ona göre veri tabanından ders içeriğini getir.
        if(id!=0){
        var lesson = _lessonRepository.GetLessonById(id);
         ViewBag.LessonContent = new LessonContentModel(){
            Id=lesson.Id,
            Content=lesson.LessonContent
        };
        user.LastLessonId=lesson.Id;
        await _userManager.UpdateAsync(user);
        }
            return View(titleModel);
            }else{
                return Redirect("/Index/Home");
            }
        }
        
      
        [HttpGet]
        public IActionResult LevelInformation(){
            return View();
        }

    }
}