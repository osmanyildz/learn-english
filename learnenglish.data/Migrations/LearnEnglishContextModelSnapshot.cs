﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using learnenglish.data.Concrete.EfCore;

#nullable disable

namespace learnenglish.data.Migrations
{
    [DbContext(typeof(LearnEnglishContext))]
    partial class LearnEnglishContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.12");

            modelBuilder.Entity("learnenglish.entity.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("IsCorrect")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Option")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("QuizId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("QuizId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("learnenglish.entity.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("LessonContent")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LessonTitle")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("LevelId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("LevelId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("learnenglish.entity.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("LevelName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("learnenglish.entity.Quiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("QuizContent")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("levelId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("levelId");

                    b.ToTable("Quizzes");
                });

            modelBuilder.Entity("learnenglish.entity.Answer", b =>
                {
                    b.HasOne("learnenglish.entity.Quiz", "Quiz")
                        .WithMany("Answers")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Quiz");
                });

            modelBuilder.Entity("learnenglish.entity.Lesson", b =>
                {
                    b.HasOne("learnenglish.entity.Level", "Level")
                        .WithMany("Lessons")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Level");
                });

            modelBuilder.Entity("learnenglish.entity.Quiz", b =>
                {
                    b.HasOne("learnenglish.entity.Level", "Level")
                        .WithMany("Quizzes")
                        .HasForeignKey("levelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Level");
                });

            modelBuilder.Entity("learnenglish.entity.Level", b =>
                {
                    b.Navigation("Lessons");

                    b.Navigation("Quizzes");
                });

            modelBuilder.Entity("learnenglish.entity.Quiz", b =>
                {
                    b.Navigation("Answers");
                });
#pragma warning restore 612, 618
        }
    }
}