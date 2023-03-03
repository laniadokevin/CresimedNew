using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;
using Cresimed.Core.Entities;
using Cresimed.Core.Entities.Database;

namespace Cresimed.Data
{
    public class CresimedDBContext : DbContext
    {


        public CresimedDBContext(DbContextOptions<CresimedDBContext> options)
                : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Specialty> Specialties { get; set; }
        public virtual DbSet<Career> Careers { get; set; }
        public virtual DbSet<KeyWord> KeyWords { get; set; }
        public virtual DbSet<Faq> Faqs{ get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ExamDetail> ExamDetails { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Percentil> Percentils { get; set; }
        public virtual DbSet<QuestionStat> QuestionStats { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {

                entity.HasIndex(e => e.Username).IsUnique();
                
                entity.HasIndex(e => e.Email).IsUnique();

                entity.Property(e => e.FullName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
                entity.Property(e => e.UserAverage).HasColumnType("decimal(4,2)");

                entity.Property(e => e.Password)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.RoleID, e.UserID});

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role_UserID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role_RoleID");
            });

            modelBuilder.Entity<Role>(entity =>
            {

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });
     
            modelBuilder.Entity<Log>(entity =>

            {
                entity.HasKey(e => e.LogID);
                entity.ToTable("Log");


            });

            modelBuilder.Entity<Question>(entity =>

            {
                entity.HasKey(e => e.QuestionID);
                entity.ToTable("Question");


            });

            modelBuilder.Entity<Exam>(entity =>

            {
                entity.HasKey(e => e.ExamID);
                entity.ToTable("Exam");


            });

            modelBuilder.Entity<Answer>(entity =>

            {
                entity.HasKey(e => e.AnswerID);
                entity.ToTable("Answer");


            });

            modelBuilder.Entity<Career>(entity =>

            {
                entity.HasKey(e => e.CareerID);
                entity.ToTable("Career");


            });

            modelBuilder.Entity<Category>(entity =>

            {
                entity.HasKey(e => e.CategoryID);
                entity.ToTable("Category");


            });

            modelBuilder.Entity<Country>(entity =>

            {
                entity.HasKey(e => e.CountryID);
                entity.ToTable("Country");


            });

            modelBuilder.Entity<Specialty>(entity =>

            {
                entity.HasKey(e => e.SpecialtyID);
                entity.ToTable("Specialty");

                
            });

            modelBuilder.Entity<KeyWord>(entity =>

            {
                entity.HasKey(e => e.KeyWordID);
                entity.ToTable("KeyWord");


            });

            modelBuilder.Entity<Faq>(entity =>

            {
                entity.HasKey(e => e.FaqID);
                entity.ToTable("Faq");


            });

            modelBuilder.Entity<Contact>(entity =>

            {
                entity.HasKey(e => e.ContactID);
                entity.ToTable("Contact");


            });

            modelBuilder.Entity<ExamDetail>(entity =>

            {
                entity.HasKey(e => e.ExamDetailID);
                entity.ToTable("ExamDetail");


            });

            modelBuilder.Entity<Report>(entity =>

            {
                entity.HasKey(e => e.ReportID);
                entity.ToTable("Report");


            });

            modelBuilder.Entity<Percentil>(entity =>

            {
                entity.HasKey(e => e.PercentilID);
                entity.ToTable("Percentil");

            });

            modelBuilder.Entity<QuestionStat>(entity =>

            {
                entity.HasKey(e => e.QuestionStatID);
                entity.ToTable("QuestionStat");


            });

            modelBuilder.Entity<Subscription>(entity =>

            {
                entity.HasKey(e => e.SubscriptionID);
                entity.ToTable("Subscription");


            });

            modelBuilder.Entity<Discount>(entity =>

            {
                entity.HasKey(e => e.DiscountID);
                entity.ToTable("Discount");


            });
        }
    }
}