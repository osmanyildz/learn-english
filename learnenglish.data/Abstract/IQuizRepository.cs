using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learnenglish.entity;

namespace learnenglish.data.Abstract
{
    public interface IQuizRepository:IRepository<Quiz>
    {
        void CreateQuiz(Quiz quiz, List<Answer> answers);
        List<Quiz> GetAllQuizzes();
        Quiz GetQuizById(int id);
        void QuizDelete(int id);
        void UpdateQuiz(Quiz quiz, List<Answer> answers);
        List<Quiz> GetQuizzesByLevelId(int levelId);
    }
}