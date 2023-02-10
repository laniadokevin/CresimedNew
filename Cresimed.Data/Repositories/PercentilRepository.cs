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
using Cresimed.Core.Entities.ViewModel.Campus;

namespace Cresimed.Data.Repositories
{
    public class PercentilRepository : GenericRepository<Percentil>,IPercentilRepository
    {
        private readonly CresimedDBContext _context;
        private readonly IUserRepository _userRepository;

        public PercentilRepository(CresimedDBContext context, IUserRepository userRepository) : base(context)
        {
            _context = context;
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public Percentil UpdatePercentil(List<ExamDetail> examDetails)
        {
            var exam = _context
                .Exams
                .Include("ExamDetails")
                .Include("ExamDetails.Question")
                .Where(x => x.ExamID == examDetails.FirstOrDefault().ExamID)
                .FirstOrDefault();

            var userId = exam.UserID;
            var specialties = exam.ExamDetails.GroupBy(x=>x.Question.SpecialtyID);

            foreach (var e in specialties)
            { 
                var p = _context
                    .Percentils
                    .Where(x => x.UserID == userId 
                        && x.SpecialtyID == e.Key)
                    .SingleOrDefault();

                if (p == null)
                {
                    var tot = e.Count();
                    var corr = e.Where(s => s.IsCorrect == true).Count();
                    var incorr = tot - corr;

                    var percentil = new Percentil();

                    percentil.UserID = userId;
                    percentil.SpecialtyID = e.Key;
                    percentil.CorrectsAmount = corr;
                    percentil.IncorrectsAmount = incorr;
                    percentil.QuestionsAmount = tot;
                    percentil.Value = (int)((double)corr / (double)tot * 100);

                    _context.Percentils.Add(percentil);
                    _context.SaveChanges();


                }
                else 
                {
                    var tot = e.Count();
                    var corr = e.Where(s => s.IsCorrect == true).Count();
                    var incorr = tot - corr;

                    p.CorrectsAmount = p.CorrectsAmount + corr;
                    p.IncorrectsAmount = p.IncorrectsAmount + incorr;
                    p.QuestionsAmount = p.QuestionsAmount + tot;
                    p.Value = (int)((double)p.CorrectsAmount / (double)p.QuestionsAmount * 100);


                    _context.SaveChanges();


                }
            }


            var percentils = GetListPercentilsForUser(userId);

            if (percentils.Count > 0)
            {
                var sum = percentils.Sum(x => x.Value);
                var cantSpecialties = percentils.Count();

                decimal userAverage = Decimal.Round((decimal)sum / (decimal)cantSpecialties,2, MidpointRounding.AwayFromZero) ;

                _userRepository.UpdateUserAverage(userId, userAverage);

            }


            return null;
        }

        public PercentilsViewModel GetPercentilsForUser(int userID)
        {

            PercentilsViewModel view = new PercentilsViewModel();

            var percentils = GetListPercentilsForUser(userID);

            if (percentils.Count() > 0)
            {
                view.Percentils = percentils;
            }

            return view;

        }

        public List<Percentil> GetListPercentilsForUser(int userID)
        {
            PercentilsViewModel view = new PercentilsViewModel();

            var percentils = _context.Percentils
                .Include("Specialty")
                .Where(x => x.UserID == userID)
                .OrderByDescending(x => x.Value)
                .ToList();

            return percentils;
        }
    }
}
