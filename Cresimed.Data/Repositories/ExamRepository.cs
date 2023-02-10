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
using Cresimed.Core.Entities.ViewModel.Campus;
using Cresimed.Core.Entities.Enum;
using Cresimed.Core.Entities.Const;
using Cresimed.Core.Entities.Enums;
using Cresimed.Core.Entities.Base;

namespace Cresimed.Data.Repositories
{
    public class ExamRepository : GenericRepository<Exam>, IExamRepository
    {
        private readonly CresimedDBContext _context;

        public ExamRepository(CresimedDBContext context) : base(context)
        {
            _context = context;
        
        }

        public Exam AnswerExam(List<ExamDetail> details)
        {
            var fullExam = _context.Exams.Include("ExamDetails").Include("ExamDetails.Question").Include("ExamDetails.Question.Answers").Where(x=>x.ExamID == details.FirstOrDefault().ExamID).FirstOrDefault();
            if (fullExam != null)
            {
                fullExam.DateEnd = DateTime.Now;
                foreach (var e in details)
                {
                    var respuesta = fullExam.ExamDetails.Where(x => x.QuestionID == e.QuestionID).FirstOrDefault();
                    respuesta.AnswerID = e.AnswerID;
                    respuesta.Time = e.Time;
                    if (e.AnswerID.HasValue)
                        respuesta.IsCorrect = respuesta.Question.Answers.Where(x => x.AnswerID == e.AnswerID).FirstOrDefault().IsCorrect;
                    _context.SaveChanges();    
                }

            }
            return null;
        }

        public PaginatedList<Exam> GetAllFiltered(int userID,string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            var exams = _context.Exams.Where(x => x.UserID == userID).AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                //exams = exams.Where(s => s..Contains(searchString));
            }
            switch (sortOrder)
            {

                case "ID":
                    exams = exams.OrderBy(s => s.ExamID);
                    break;
                case "ID_desc":
                    exams = exams.OrderByDescending(s => s.ExamID);
                    break;
                case "Exam":
                    exams = exams.OrderBy(s => s.DateStarted);
                    break;
                case "Exam_desc":
                    exams = exams.OrderByDescending(s => s.DateStarted);
                    break;
                default:
                    exams = exams.OrderByDescending(s => s.ExamID);
                    break;
            }

            int pageSize = 20;
            return PaginatedList<Exam>.Create(exams, pageNumber ?? 1, pageSize);

        }

        public int TotalExamsCount(int userID)
        {
            return _context.Exams.Where(x => x.UserID == userID).Count();
        }

        public Exam GetNewExam(CreateExamIndexViewModel filters)
        {
            
            try
            {
                var oldExams = _context.Exams.Include("ExamDetails").Where(x => x.UserID == filters.UserID).ToList();
                var oldQuest = new List<int>();
                var oldIncorrectQuest = new List<int>();

                foreach (var ed in oldExams)
                    foreach (var e in ed.ExamDetails)
                    {
                        oldQuest.Add(e.QuestionID);
                        if (e.IsCorrect != null)
                        {
                            if (!(bool)e.IsCorrect)
                                oldIncorrectQuest.Add(e.QuestionID);
                        }
                    }

                var questions = _context.Questions.Include("Answers").Include("QuestionStat").Include("KeyWords").AsQueryable();

                // Checkear Por Category
                if (filters.QuestionsCategory > 0)
                {
                    questions = questions.Where(x => filters.QuestionsCategory == x.CategoryID).AsQueryable();
                }

                // Como checkear que preguntas ya hizo
                if (filters.QuestionsType >= 0)
                {
                    switch (filters.QuestionsType)
                    {
                        case (int)QuestionsTypeEnum.NEW:
                            questions = questions.Where(x => !oldQuest.Contains(x.QuestionID)).AsQueryable();
                            break;
                        case (int)QuestionsTypeEnum.INCORRECT:
                            questions = questions.Where(x => !oldIncorrectQuest.Contains(x.QuestionID)).AsQueryable();
                            break;
                    }
                }
                
                // Checkear que especialidad quiere hacer
                if (filters.QuestionsSpecialty.Count() > 0)
                {
                    questions = questions.Where(x=> filters.QuestionsSpecialty.Contains(x.SpecialtyID)).AsQueryable();
                }

                var list = questions.OrderBy(x => Guid.NewGuid()).Take(filters.QuestionsAmount).ToList();
                
                if (list.Count < filters.QuestionsAmount) 
                {

                    questions = _context.Questions.Include("Answers").Include("KeyWords").AsQueryable();

                    // Checkear Por Category
                    if (filters.QuestionsCategory > 0)
                    {
                        questions = questions.Where(x => filters.QuestionsCategory == x.CategoryID).AsQueryable();
                    }

                    // Checkear que especialidad quiere hacer
                    if (filters.QuestionsSpecialty.Count() > 0)
                    {
                        questions = questions.Where(x => filters.QuestionsSpecialty.Contains(x.SpecialtyID)).AsQueryable();
                    }
                    var listAux = questions.OrderBy(x => Guid.NewGuid()).Take(filters.QuestionsAmount - list.Count).ToList();
                    foreach (var question in listAux)
                        list.Add(question);

                }

                return (InsertNewExam(list, filters));
            }
            catch (Exception ex)
            {
                return null;
            }
                   
            
        }

        public Exam InsertNewExam(List<Question> questions, CreateExamIndexViewModel filt)
        {
            Exam exam = new Exam();
            exam.UserID = filt.UserID;
            exam.Tutor = filt.Tutor ;
            exam.Timer = filt.Timer ;
            exam.TimeAmount = filt.TimeAmount ;
            exam.QuestionsAmount = filt.QuestionsAmount ;
            exam.QuestionsType = filt.QuestionsType ;
            exam.QuestionsCategory = filt.QuestionsCategory ;
            exam.QuestionsSpecialty = string.Join(",", filt.QuestionsSpecialty);
            exam.DateStarted = DateTime.Now;

            _context.Exams.Add(exam);
            _context.SaveChanges();
            foreach (var e in questions)
            { 
                ExamDetail examDetail = new ExamDetail();
                examDetail.QuestionID = e.QuestionID;
                examDetail.Question = e;
                examDetail.ExamID = exam.ExamID;
                exam.ExamDetails.Add(examDetail);
                _context.ExamDetails.Add(examDetail);
            }
            _context.SaveChanges();
            return exam;
        }

        public Exam GetById(int id)
        {
            var exam = new Exam();
            exam = _context.Exams.Include("ExamDetails").Include("ExamDetails.Question").Include("ExamDetails.Question.Answers").Where(x => x.ExamID == id).FirstOrDefault();
            return exam;
        }

        public List<Exam> GetAllExams(int userID)
        {
            var exams = _context.Exams
                .Include("ExamDetails")
                .Include("ExamDetails.Question")
                .Where(x => x.UserID == userID && x.ExamDetails.Any(a => a.IsCorrect.HasValue ? a.IsCorrect.Value : false))
                .SelectMany(x => x.ExamDetails)
                .ToList();

            var tot = exams;

            var cor = exams.Where(z => z.IsCorrect == true).ToList();

            var inc = exams.Where(z => z.IsCorrect == false).ToList();


            return null;
        }

        public List<Exam> GetLast5Exams(int userID)
        {
            var exams = _context.Exams
                .Where(x => x.UserID == userID)
                .OrderByDescending(x => x.ExamID)
                .Take(5)
                .ToList();
        

            return exams;
        }

        public ExamStatsViewModel GetAllBySpecialty(int userID)
        {
            ExamStatsViewModel examStats = new ExamStatsViewModel();

            var exams = _context.Exams
                .Where(x => x.UserID == userID)
                .Include("ExamDetails")
                .SelectMany(x => x.ExamDetails)
                .ToList();

            var tot = exams.Where(x => x.IsCorrect != null).Count();

            var allCors = exams.Where(z => z.IsCorrect == true).Count();

            var allIncs = exams.Where(z => z.IsCorrect == false).Count();
            /*
            var corBySpec = exams.Where(z => z.IsCorrect == true).GroupBy(x => x.Question.SpecialtyID).ToList();

            var incorBySpec = exams.Where(z => z.IsCorrect == false).GroupBy(x => x.Question.SpecialtyID).ToList();

            foreach (var e in corBySpec)
            {

                CorrectBySpecialty aux = new CorrectBySpecialty();

                aux.Name = e.First().Question.Specialty.Name;
                aux.Corrects = e.Count();

                examStats.CountBySpecialties.Add(aux);

            }

            foreach (var e in incorBySpec)
            {
                var ex = examStats.CountBySpecialties.Where(x => x.Name == e.First().Question.Specialty.Name).FirstOrDefault();

                if (ex != null)
                {
                    ex.Incorrects = e.Count();
                }
                else
                {

                    CorrectBySpecialty aux = new CorrectBySpecialty();

                    aux.Name = e.First().Question.Specialty.Name;
                    aux.Incorrects = e.Count();

                    examStats.CountBySpecialties.Add(aux);

                }

            }
            */
            examStats.TotalCorrect = allCors;
            examStats.TotalIncorrect = allIncs;
            examStats.TotalCount = tot;

            return examStats;
        }
    }
}
