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

namespace Cresimed.Data.Repositories
{
    public class FaqRepository : GenericRepository<Faq>,IFaqRepository
    {
        private readonly CresimedDBContext _context;

        public FaqRepository(CresimedDBContext context) : base(context)
        {
            _context = context;
        
        }

        public void DeleteFaq(int id)
        {
            Faq Faq = _context.Faqs.Where(x => x.FaqID == id).SingleOrDefault();
            _context.Remove(Faq);
            _context.SaveChanges();
        }

        public List<Faq> GetAll()
        {
            return _context.Faqs.ToList();

        }

        public Faq InsertFaq(Faq Faq)
        {
            _context.Faqs.Add(Faq);
            _context.SaveChanges();

            return Faq;

        }

        public Faq UpdateFaq(Faq Faq)
        {
            var p = _context.Faqs.Where(x => x.FaqID == Faq.FaqID).SingleOrDefault();
            if (p != null)
            {

                p.Subject = Faq.Subject;
                p.Question = Faq.Question;
                p.Answer = Faq.Answer;

                _context.SaveChanges();
            }
            return p;
        }
        public Faq GetById(int id)
        {
            var p = _context.Faqs.Where(x => x.FaqID == id).SingleOrDefault();

            return p;
        }

        public PaginatedList<Faq> GetAllFiltered(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            var careers = _context.Faqs.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                careers = careers.Where(s => s.Question.ToLower().Replace("à", "a")
                .Replace("â", "a")
                .Replace("ä", "a")
                .Replace("ç", "c")
                .Replace("é", "e")
                .Replace("è", "e")
                .Replace("ê", "e")
                .Replace("ë", "e")
                .Replace("î", "i")
                .Replace("ï", "i")
                .Replace("ô", "o")
                .Replace("ù", "u")
                .Replace("û", "u")
                .Replace("ü", "u").Contains(searchString) || s.Subject.ToLower().Replace("à", "a")
                .Replace("â", "a")
                .Replace("ä", "a")
                .Replace("ç", "c")
                .Replace("é", "e")
                .Replace("è", "e")
                .Replace("ê", "e")
                .Replace("ë", "e")
                .Replace("î", "i")
                .Replace("ï", "i")
                .Replace("ô", "o")
                .Replace("ù", "u")
                .Replace("û", "u")
                .Replace("ü", "u").Contains(searchString) || s.Answer.ToLower().Replace("à", "a")
                .Replace("â", "a")
                .Replace("ä", "a")
                .Replace("ç", "c")
                .Replace("é", "e")
                .Replace("è", "e")
                .Replace("ê", "e")
                .Replace("ë", "e")
                .Replace("î", "i")
                .Replace("ï", "i")
                .Replace("ô", "o")
                .Replace("ù", "u")
                .Replace("û", "u")
                .Replace("ü", "u").Contains(searchString));
            }
            switch (sortOrder)
            {

                case "ID":
                    careers = careers.OrderBy(s => s.FaqID);
                    break;
                case "ID_desc":
                    careers = careers.OrderByDescending(s => s.FaqID);
                    break;
                case "Subject":
                    careers = careers.OrderBy(s => s.Subject);
                    break;
                case "Subject_desc":
                    careers = careers.OrderByDescending(s => s.Subject);
                    break;
                case "Question":
                    careers = careers.OrderBy(s => s.Question);
                    break;
                case "Question_desc":
                    careers = careers.OrderByDescending(s => s.Question);
                    break;
                case "Answer":
                    careers = careers.OrderBy(s => s.Answer);
                    break;
                case "Answer_desc":
                    careers = careers.OrderByDescending(s => s.Answer);
                    break;
                default:
                    careers = careers.OrderByDescending(s => s.FaqID);
                    break;

            }

            int pageSize = 20;
            return PaginatedList<Faq>.Create(careers, pageNumber ?? 1, pageSize);

        }

        public int TotalFaqsCount()
        {
            return _context.Faqs.Count();
        }

        public int TotalFilteredCount(string searchString)
        {
            var careers = _context.Faqs.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                careers = careers.Where(s => s.Question.Contains(searchString));
            }
            return careers.Count();
        }
    }
}
