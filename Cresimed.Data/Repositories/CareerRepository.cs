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
    public class CareerRepository : GenericRepository<Career>,ICareerRepository
    {
        private readonly CresimedDBContext _context;

        public CareerRepository(CresimedDBContext context) : base(context)
        {
            _context = context;
        
        }

        public void DeleteCareer(int id)
        {
            Career career = _context.Careers.Where(x => x.CareerID == id).SingleOrDefault();
            _context.Remove(career);
            _context.SaveChanges();
        }

        public List<Career> GetAll()
        {
            return _context.Careers.Include("Questions").Include("Country").Include("Specialties").ToList();

        }

        public Career GetOrSave(Career career)
        {
            var careerDb = _context.Careers.Where(x => x.Name == career.Name).SingleOrDefault();
         
            if (careerDb == null)
            {
                _context.Careers.Add(career);
                _context.SaveChanges();
                
                return career;
            }
            else
                return careerDb;

        }
        public Career UpdateCareer(Career career)
        {
            var p = _context.Careers.Where(x => x.CareerID == career.CareerID).SingleOrDefault();
            if (p != null)
            {
                p.Name = career.Name;
                p.CountryID = career.CountryID;

                _context.SaveChanges();
            }
            return p;
        }
        public Career GetById(int id)
        {
            var p = _context.Careers.Where(x => x.CareerID == id).SingleOrDefault();

            return p;
        }

        public PaginatedList<Career> GetAllFiltered(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        { 
            var careers = _context.Careers.Include("Questions").Include("Country").Include("Specialties").AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                careers = careers.Where(s => s.Name.ToLower().Replace("à", "a")
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
                    careers = careers.OrderBy(s => s.CareerID);
                    break;
                case "ID_desc":
                    careers = careers.OrderByDescending(s => s.CareerID);
                    break;
                case "Career":
                    careers = careers.OrderBy(s => s.Name);
                    break;
                case "Career_desc":
                    careers = careers.OrderByDescending(s => s.Name);
                    break;
                case "Countries":
                    careers = careers.OrderBy(s => s.Country.Name);
                    break;
                case "Countries_desc":
                    careers = careers.OrderByDescending(s => s.Country.Name);
                    break;
                case "Specialties":
                    careers = careers.OrderBy(s => s.Specialties.Count);
                    break;
                case "Specialties_desc":
                    careers = careers.OrderByDescending(s => s.Specialties.Count);
                    break;
                case "Questions":
                    careers = careers.OrderBy(s => s.Questions.Count);
                    break;
                case "Questions_desc":
                    careers = careers.OrderByDescending(s => s.Questions.Count);
                    break;
                default:
                    careers = careers.OrderByDescending(s => s.CareerID);
                    break;
            }

            int pageSize = 20;
            return PaginatedList<Career>.Create(careers, pageNumber ?? 1, pageSize);

        }

        public int TotalCareersCount()
        {
            return _context.Careers.Count();
        }

        public int TotalFilteredCount(string searchString)
        {
            var careers = _context.Careers.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                careers = careers.Where(s => s.Name.Contains(searchString));
            }
            return careers.Count();

        }
    }
}
