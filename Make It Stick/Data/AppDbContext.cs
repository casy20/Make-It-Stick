using Make_It_Stick.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace Make_It_Stick.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TrainingProgram> TrainingPrograms { get; set; }
        public DbSet<ClassGroup> Classes { get; set; }
        public DbSet<ClassMember> ClassMembers { get; set; }
        public DbSet<QuestionSetClass> QuestionSetClasses { get; set; }
        public DbSet<QuestionSet> QuestionSets { get; set; }
        public DbSet<LessonMaterial> LessonMaterials { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<TestAttempt> TestAttempts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;port=3306;user=root;password=;database=makeItStick",
                ServerVersion.Parse("8.0.30")
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.Property(user => user.Name).HasColumnName("naam");
                entity.Property(user => user.Email).HasColumnName("email");
                entity.Property(user => user.Password).HasColumnName("wachtwoord");
                entity.Property(user => user.IsTeacher).HasColumnName("is_docent");
            });

            modelBuilder.Entity<TrainingProgram>(entity =>
            {
                entity.ToTable("Opleiding");
                entity.Property(program => program.Name).HasColumnName("naam");
            });

            modelBuilder.Entity<ClassGroup>(entity =>
            {
                entity.ToTable("Klas");
                entity.Property(classGroup => classGroup.Name).HasColumnName("naam");
                entity.Property(classGroup => classGroup.ClassCode).HasColumnName("klascode");
                entity.Property(classGroup => classGroup.TrainingProgramId).HasColumnName("opleiding_id");
                entity.Property(classGroup => classGroup.TeacherId).HasColumnName("docent_id");
            });

            modelBuilder.Entity<ClassMember>(entity =>
            {
                entity.ToTable("KlasLid");
                entity.HasKey(member => new { member.ClassId, member.UserId });
                entity.Property(member => member.ClassId).HasColumnName("klas_id");
                entity.Property(member => member.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<QuestionSetClass>(entity =>
            {
                entity.ToTable("VragensetKlas");
                entity.HasKey(link => new { link.QuestionSetId, link.ClassId });
                entity.Property(link => link.QuestionSetId).HasColumnName("vragenset_id");
                entity.Property(link => link.ClassId).HasColumnName("klas_id");
            });

            modelBuilder.Entity<QuestionSet>(entity =>
            {
                entity.ToTable("Vragenset");
                entity.Property(set => set.Title).HasColumnName("titel");
                entity.Property(set => set.TeacherId).HasColumnName("docent_id");
            });

            modelBuilder.Entity<LessonMaterial>(entity =>
            {
                entity.ToTable("Lesstof");
                entity.Property(material => material.TrainingProgramId).HasColumnName("opleiding_id");
                entity.Property(material => material.Title).HasColumnName("titel");
                entity.Property(material => material.Content).HasColumnName("inhoud");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Vraag");
                entity.Property(question => question.QuestionSetId).HasColumnName("vragenset_id");
                entity.Property(question => question.QuestionText).HasColumnName("vraagtekst");
                entity.Property(question => question.Explanation).HasColumnName("uitleg");
                entity.Property(question => question.Difficulty).HasColumnName("moeilijkheid");
            });

            modelBuilder.Entity<AnswerOption>(entity =>
            {
                entity.ToTable("Antwoordoptie");
                entity.Property(option => option.QuestionId).HasColumnName("vraag_id");
                entity.Property(option => option.Text).HasColumnName("tekst");
                entity.Property(option => option.IsCorrect).HasColumnName("is_goed");
            });

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("Antwoord");
                entity.Property(answer => answer.UserId).HasColumnName("user_id");
                entity.Property(answer => answer.QuestionId).HasColumnName("vraag_id");
                entity.Property(answer => answer.AnswerOptionId).HasColumnName("optie_id");
                entity.Property(answer => answer.TestAttemptId).HasColumnName("toetspoging_id");
                entity.Property(answer => answer.AnsweredAt).HasColumnName("tijdstip");
            });

            modelBuilder.Entity<TestAttempt>(entity =>
            {
                entity.ToTable("Toetspoging");
                entity.Property(attempt => attempt.UserId).HasColumnName("user_id");
                entity.Property(attempt => attempt.QuestionSetId).HasColumnName("vragenset_id");
                entity.Property(attempt => attempt.Date).HasColumnName("datum");
            });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Alice Teacher",
                    Email = "alice@example.com",
                    Password = "demo-password",
                    IsTeacher = true,
                },
                new User
                {
                    Id = 2,
                    Name = "Bob Student",
                    Email = "bob@example.com",
                    Password = "demo-password",
                    IsTeacher = false,
                }
            );

            modelBuilder.Entity<TrainingProgram>().HasData(
                new TrainingProgram
                {
                    Id = 1,
                    Name = "Web Development",
                }
            );

            modelBuilder.Entity<ClassGroup>().HasData(
                new ClassGroup
                {
                    Id = 1,
                    Name = "Class A",
                    ClassCode = "CLASS-A",
                    TrainingProgramId = 1,
                    TeacherId = 1,
                }
            );

            modelBuilder.Entity<ClassMember>().HasData(
                new ClassMember
                {
                    ClassId = 1,
                    UserId = 2,
                }
            );

            modelBuilder.Entity<QuestionSet>().HasData(
                new QuestionSet
                {
                    Id = 1,
                    Title = "Programming Basics",
                    TeacherId = 1,
                }
            );

            modelBuilder.Entity<QuestionSetClass>().HasData(
                new QuestionSetClass
                {
                    QuestionSetId = 1,
                    ClassId = 1,
                }
            );

            modelBuilder.Entity<LessonMaterial>().HasData(
                new LessonMaterial
                {
                    Id = 1,
                    TrainingProgramId = 1,
                    Title = "Introduction to C#",
                    Content = "Variables store values that a program can use.",
                }
            );

            modelBuilder.Entity<Question>().HasData(
                new Question
                {
                    Id = 1,
                    QuestionSetId = 1,
                    QuestionText = "Which keyword declares an integer in C#?",
                    Explanation = "The int keyword declares a 32-bit integer.",
                    Difficulty = "Easy",
                }
            );

            modelBuilder.Entity<AnswerOption>().HasData(
                new AnswerOption
                {
                    Id = 1,
                    QuestionId = 1,
                    Text = "int",
                    IsCorrect = true,
                },
                new AnswerOption
                {
                    Id = 2,
                    QuestionId = 1,
                    Text = "string",
                    IsCorrect = false,
                },
                new AnswerOption
                {
                    Id = 3,
                    QuestionId = 1,
                    Text = "bool",
                    IsCorrect = false,
                }
            );

            modelBuilder.Entity<TestAttempt>().HasData(
                new TestAttempt
                {
                    Id = 1,
                    UserId = 2,
                    QuestionSetId = 1,
                    Score = 1.00m,
                    Date = new DateTime(2026, 1, 15, 10, 0, 0, DateTimeKind.Utc),
                }
            );

            modelBuilder.Entity<Answer>().HasData(
                new Answer
                {
                    Id = 1,
                    UserId = 2,
                    QuestionId = 1,
                    AnswerOptionId = 1,
                    TestAttemptId = 1,
                    AnsweredAt = new DateTime(2026, 1, 15, 10, 0, 0, DateTimeKind.Utc),
                }
            );
        }
    }
}
