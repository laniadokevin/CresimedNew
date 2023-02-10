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
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Cresimed.Data.Repositories
{
    public class QuestionStatRepository : GenericRepository<QuestionStat>, IQuestionStatRepository
    {
        private readonly CresimedDBContext _context;

        public QuestionStatRepository(CresimedDBContext context) : base(context)
        {
            _context = context;

        }
        public QuestionStat UpdateQuestionStat(List<ExamDetail> examDetails)
        {
            var exam = _context
                .Exams
                .Include("ExamDetails")
                .Include("ExamDetails.Question")
                .Where(x => x.ExamID == examDetails.FirstOrDefault().ExamID)
                .FirstOrDefault();

            var specialties = exam.ExamDetails.GroupBy(x => x.Question.QuestionID);

            foreach (var e in specialties)
            {
                var p = _context
                    .QuestionStats
                    .Where(x => x.QuestionID == e.Key)
                    .SingleOrDefault();

                if (p == null)
                {
                    var tot = e.Count();
                    var corr = e.Where(s => s.IsCorrect == true).Count();
                    var incorr = tot - corr;

                    var questionStat = new QuestionStat();

                    questionStat.QuestionID = e.Key;
                    questionStat.CorrectsAmount = corr;
                    questionStat.IncorrectsAmount = incorr;
                    questionStat.TotalAmount = tot;
                    questionStat.Percentage = (int)((double)corr / (double)tot * 100);

                    _context.QuestionStats.Add(questionStat);
                    _context.SaveChanges();


                }
                else
                {
                    var tot = e.Count();
                    var corr = e.Where(s => s.IsCorrect == true).Count();
                    var incorr = tot - corr;

                    p.CorrectsAmount = p.CorrectsAmount + corr;
                    p.IncorrectsAmount = p.IncorrectsAmount + incorr;
                    p.TotalAmount = p.TotalAmount + tot;
                    p.Percentage = (int)((double)p.CorrectsAmount / (double)p.TotalAmount * 100);


                    _context.SaveChanges();


                }
            }

            return null;
        }
    }
}
