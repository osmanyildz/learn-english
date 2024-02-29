using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learnenglish.entity;
using Microsoft.EntityFrameworkCore;

namespace learnenglish.data.Concrete.EfCore
{
    public class LearnEnglishContext:DbContext
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source = learnenglishDb");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}