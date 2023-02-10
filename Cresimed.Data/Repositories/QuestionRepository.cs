using System;
using System.Collections.Generic;
using System.Text;
using Cresimed.Core.Interfaces;
using Cresimed.Core.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq;
using Cresimed.Core.Entities.Database;
using Cresimed.Core.Entities.Base;

namespace Cresimed.Data.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        private readonly CresimedDBContext _context;

        public QuestionRepository(CresimedDBContext context) : base(context)
        {
            _context = context;

        }

        public Question InsertQuestion(Question question)
        {
            question.UniqueIdentity = GetLastID() + 1;
            if (question.QuestionImage == null)
                question.QuestionImage = "";
            if (question.ExplanationImage == null)
                question.ExplanationImage = "";

            _context.Questions.Add(question);
            _context.SaveChanges();

            return question;
        }


        public void DeleteQuestion(int id)
        {
            Question Question = _context.Questions.Where(x => x.QuestionID == id).SingleOrDefault();
            _context.Remove(Question);
            _context.SaveChanges();
        }

        public List<Question> GetAll()
        {
            return _context.Questions.Include("Career").Include("Category").Include("Country").Include("Specialty").ToList();

        }

        public Question UpdateQuestion(Question Question)
        {
            var p = _context.Questions.Where(x => x.QuestionID == Question.QuestionID).SingleOrDefault();
            if (p != null)
            {
                p.QuestionText = Question.QuestionText;
                
                if (Question.QuestionImage != null)
                    p.QuestionImage = Question.QuestionImage;
                
                p.CountryID = Question.CountryID;
                p.CareerID = Question.CareerID;
                p.SpecialtyID = Question.SpecialtyID;
                p.CategoryID = Question.CategoryID;
                p.Explanation = Question.Explanation;
             
                if (Question.ExplanationImage != null)
                    p.ExplanationImage = Question.ExplanationImage;

                _context.SaveChanges();
            }

            return p;
        }
        public Question GetById(int id)
        {
            var p = _context.Questions.Where(x => x.QuestionID == id).Include("Answers").Include("KeyWords").SingleOrDefault();

            return p;
        }

        public PaginatedList<Question> GetAllFiltered(string sortOrder, string currentFilter, string searchString, int? pageNumber, int CategoryID, int SpecialtyID)
        {
            var questions = _context.Questions.Include("Career").Include("Category").Include("Country").Include("Specialty").AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                questions = questions.Where(s => s.QuestionText.Contains(searchString));
            }
            if ((CategoryID) > 0)
            {
                questions = questions.Where(s => s.CategoryID == CategoryID);
            }
            if ((SpecialtyID) > 0)
            {
                questions = questions.Where(s => s.SpecialtyID == SpecialtyID);
            }
            switch (sortOrder)
            {

                case "ID":
                    questions = questions.OrderBy(s => s.QuestionID);
                    break;
                case "ID_desc":
                    questions = questions.OrderByDescending(s => s.QuestionID);
                    break;
                case "Categories":
                    questions = questions.OrderBy(s => s.Category.Name);
                    break;
                case "Categories_desc":
                    questions = questions.OrderByDescending(s => s.Category.Name);
                    break;
                case "Specialties":
                    questions = questions.OrderBy(s => s.Specialty.Name);
                    break;
                case "Specialties_desc":
                    questions = questions.OrderByDescending(s => s.Specialty.Name);
                    break;
                case "Careers":
                    questions = questions.OrderBy(s => s.Career.Name);
                    break;
                case "Careers_desc":
                    questions = questions.OrderByDescending(s => s.Career.Name);
                    break;
                case "Countries":
                    questions = questions.OrderBy(s => s.Country.Name);
                    break;
                case "Countries_desc":
                    questions = questions.OrderByDescending(s => s.Country.Name);
                    break;
                default:
                    questions = questions.OrderByDescending(s => s.QuestionID);
                    break;
            }

            int pageSize = 40;
            return PaginatedList<Question>.Create(questions, pageNumber ?? 1, pageSize);

        }

        public int GetLastID()
        {
            return _context.Questions.OrderByDescending(x => x.QuestionID).FirstOrDefault().QuestionID;
        }

        public int TotalQuestionsCount()
        {
            return _context.Questions.Include("Career").Include("Category").Include("Country").Include("Specialty").Count();
        }

        public int TotalFilteredCount ( string searchString, int CategoryID, int SpecialtyID)
        {
            var questions = _context.Questions.Include("Career").Include("Category").Include("Country").Include("Specialty").AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                questions = questions.Where(s => s.QuestionText.Contains(searchString));
            }
            if ((CategoryID) > 0)
            {
                questions = questions.Where(s => s.CategoryID == CategoryID);
            }
            if ((SpecialtyID) > 0)
            {
                questions = questions.Where(s => s.SpecialtyID == SpecialtyID);
            }

            return (questions.Count());

        }
    }
}