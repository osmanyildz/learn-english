using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learnenglish.entity;

namespace learnenglish.data.Abstract
{
    public interface ILessonRepository:IRepository<Lesson>
    {
        void CreateContent(Lesson lesson);
        List<Lesson> GetLessonsByLevelId(int levelId);
        Lesson GetLessonById(int id);
        List<Lesson> GetAllLessons();
        void UpdateLesson(Lesson lesson);
        void DeleteLesson(Lesson lesson);
        int GetLessonQuantity();
    }
}