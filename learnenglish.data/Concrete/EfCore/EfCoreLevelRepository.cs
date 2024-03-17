using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learnenglish.data.Abstract;
using learnenglish.entity;

namespace learnenglish.data.Concrete.EfCore
{
    public class EfCoreLevelRepository : EfCoreRepository<Level, LearnEnglishContext>, ILevelRepository
    {
        public string GetLevelById(int id)
        {
            using (var context = new LearnEnglishContext())
            {
                return context.Levels.Where(i=>i.Id==id).Select(o=>o.LevelName).FirstOrDefault();
            }
        }
    }
}