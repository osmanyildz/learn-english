using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learnenglish.data.Abstract;
using learnenglish.entity;
using learnenglish.webui.Models;
using Microsoft.AspNetCore.Mvc;

namespace learnenglish.webui.Controllers
{
    public class LessonController:Controller
    {
        private ILessonRepository _lessonRepository;
        public LessonController(ILessonRepository lessonRepository)
        {
            _lessonRepository=lessonRepository;
        }
        [HttpGet]
        public IActionResult LessonContent(int id){

            var titleModel = new List<LessonTitleModel>();
            var lessons = _lessonRepository.GetLessonsByLevelId(2);

            foreach (var item in lessons)
            {
                titleModel.Add(new LessonTitleModel(){
                    Id = item.Id,
                    Title = item.LessonTitle,
                });
              if(id == 0){
                ViewBag.LessonContent = new LessonContentModel(){
                    Id=item.Id,
                    Content=item.LessonContent
                };
              }
            }
//Burada kalınan son dersin id'sine göre bir kontrol yap ilk başta navbardaki eğitim içeriğine tıkladığında onun kontrolünü yap ona göre veri tabanından ders içeriğini getir.
        if(id!=0){
        var lesson = _lessonRepository.GetLessonById(id);
         ViewBag.LessonContent = new LessonContentModel(){
            Id=lesson.Id,
            Content=lesson.LessonContent
        };
        }
            return View(titleModel);
        }
       



    }
}