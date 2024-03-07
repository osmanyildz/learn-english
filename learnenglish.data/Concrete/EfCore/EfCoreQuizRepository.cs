using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learnenglish.data.Abstract;
using learnenglish.entity;
using Microsoft.EntityFrameworkCore;

namespace learnenglish.data.Concrete.EfCore
{
    public class EfCoreQuizRepository : EfCoreRepository<Quiz, LearnEnglishContext>, IQuizRepository
    {
        // public void CreateQuiz(Quiz quiz, List<Answer> answers)
        // {
        //     using (var context = new LearnEnglishContext())
        //     {
        //         context.Quizzes.Add(quiz);
        //         context.SaveChanges();

        //         foreach (var item in answers)
        //         {
        //             context.Answers.Add(new Answer(){
        //                 QuizId=quiz.Id,
        //                 Option=item.Option,
        //                 IsCorrect=item.IsCorrect
        //             });
        //             context.SaveChanges();
        //         }
        //     }
        // }
        public void CreateQuiz(Quiz quiz, List<Answer> answers)
        {
            // System.Console.WriteLine(quiz.QuizContent);
            // System.Console.WriteLine(quiz.levelId);
            // System.Console.WriteLine(GetLevel(quiz.levelId).LevelName);
            var entity = new Quiz();
            entity.Level = GetLevel(quiz.levelId);
            entity.QuizContent=quiz.QuizContent;
            // entity.levelId=quiz.levelId;
            using (var context = new LearnEnglishContext())
            {
                context.Quizzes.Add(entity);
                context.Levels.Attach(entity.Level);
                context.SaveChanges();
                
                foreach (var item in answers)
                {
                    item.Quiz=entity;
                    context.Answers.Add(item);
                    context.SaveChanges();
                }
            }

        }

        public List<Quiz> GetAllQuizzes()
        {
            using (var context = new LearnEnglishContext())
            {
              return context.Quizzes.Include(a=>a.Answers).Include(l=>l.Level).ToList();
            }
        }

        public Level GetLevel(int levelId){
            using(var context = new LearnEnglishContext()){
                // return context.Levels.Where(level => level.Id == levelId ).FirstOrDefault();
                return context.Levels.Find(levelId);
            }
        }

       

        public Quiz GetQuizById(int id)
        {
            using (var context = new LearnEnglishContext())
            {
                return context.Quizzes.Where(q=>q.Id==id).Include(a=>a.Answers).Include(l=>l.Level).FirstOrDefault();
            }
        }

        public void QuizDelete(int id)
        {
            using (var context = new LearnEnglishContext())
            {
                var entity = context.Quizzes.Where(a=>a.Id==id).FirstOrDefault();
                context.Quizzes.Remove(entity);
                context.SaveChanges();
            }
        }
    }
}

// Level için entity oluştur ve nav propunu quiz entitysine at nav proplarla iş yap. Atamaları ilişkilerde nav prop üzerinden atamayı dene. 