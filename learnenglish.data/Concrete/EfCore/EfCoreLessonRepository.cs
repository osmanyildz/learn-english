using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using learnenglish.data.Abstract;
using learnenglish.entity;
using Microsoft.EntityFrameworkCore;

namespace learnenglish.data.Concrete.EfCore
{
    public class EfCoreLessonRepository : EfCoreRepository<Lesson, LearnEnglishContext>, ILessonRepository
    {
        public void CreateContent(Lesson lesson)
        {
                lesson.Level=GetLevel(lesson.LevelId);
                
            using (var context = new LearnEnglishContext())
            {
                context.Levels.Attach(lesson.Level);
                context.Lessons.Add(lesson);
                context.SaveChanges();
            }
        }

        public void DeleteLesson(Lesson lesson)
        {
            using(var context = new LearnEnglishContext()){
                context.Lessons.Remove(lesson);
                context.SaveChanges();
            }
        }

        public List<Lesson> GetAllLessons()
        {
            using (var context = new LearnEnglishContext())
            {
                return context.Lessons.Include(p=>p.Level).ToList();
            }
        }

        public Lesson GetLessonById(int id)
        {
            using (var context = new LearnEnglishContext())
            {
                return context.Lessons.Where(e => e.Id==id).FirstOrDefault();
            }
        }

        public List<Lesson> GetLessonsByLevelId(int levelId)
        {
            using (var context = new LearnEnglishContext())
            {
                return context.Lessons.Where(e=>e.LevelId==levelId).ToList();
            }
        }

        public Level GetLevel(int levelId){
            using (var context = new LearnEnglishContext())
            {
               return context.Levels.Find(levelId);
            }
        }

        public void UpdateLesson(Lesson lesson)
        {
            using(var context = new LearnEnglishContext()){
                var entity = context.Lessons.FirstOrDefault(a=>a.Id==lesson.Id);
                entity.LevelId=lesson.LevelId;
                entity.LessonTitle=lesson.LessonTitle;
                entity.LessonContent=lesson.LessonContent;
                context.Entry(entity).State=EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}