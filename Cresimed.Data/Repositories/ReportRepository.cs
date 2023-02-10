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
    public class ReportRepository : GenericRepository<Report>,IReportRepository
    {
        private readonly CresimedDBContext _context;

        public ReportRepository(CresimedDBContext context) : base(context)
        {
            _context = context;
        
        }

        public void DeleteReport(int id)
        {
            Report Report = _context.Reports.Where(x => x.ReportID == id).SingleOrDefault();
            _context.Remove(Report);
            _context.SaveChanges();
        }

        public List<Report> GetAll()
        {
            return _context.Reports.ToList();

        }

        public Report InsertReport(Report Report)
        {
            _context.Reports.Add(Report);
            _context.SaveChanges();

            return Report;

        }

        public Report GetById(int id)
        {
            var p = _context.Reports.Include("User").Include("Question").Where(x => x.ReportID == id).SingleOrDefault();

            return p;
        }

        public PaginatedList<Report> GetAllFiltered(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            var reports = _context.Reports.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                reports = reports.Where(s => s.Message.ToLower().Replace("à", "a")
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
                .Replace("ü", "u").Contains(searchString) || s.User.FullName.ToLower().Replace("à", "a")
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
                .Replace("ü", "u").Contains(searchString) || s.Question.QuestionText.ToLower().Replace("à", "a")
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
                    reports = reports.OrderBy(s => s.ReportID);
                    break;
                case "ID_desc":
                    reports = reports.OrderByDescending(s => s.ReportID);
                    break;
                default:
                    reports = reports.OrderByDescending(s => s.ReportID);
                    break;

            }

            int pageSize = 20;
            return PaginatedList<Report>.Create(reports, pageNumber ?? 1, pageSize);

        }

        public int TotalReportsCount()
        {
            return _context.Reports.Count();
        }

        public int TotalFilteredCount(string searchString)
        {
            var reports = _context.Reports.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                reports = reports.Where(s => s.Message.Contains(searchString));
            }
            return reports.Count();
        }
    }
}
