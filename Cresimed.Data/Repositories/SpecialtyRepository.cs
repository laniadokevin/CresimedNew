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
    public class SpecialtyRepository : GenericRepository<Specialty>,ISpecialtyRepository
    {
        private readonly CresimedDBContext _context;

        public SpecialtyRepository(CresimedDBContext context) : base(context)
        {
            _context = context;
        
        }

        public void DeleteSpecialty(int id)
        {
            Specialty specialty = _context.Specialties.Where(x => x.SpecialtyID == id).SingleOrDefault();
            _context.Remove(specialty);
            _context.SaveChanges();
        }

        public List<Specialty> GetAll()
        {
            var e = _context.Specialties.Include("Questions").Include("Country").Include("Career").ToList();
            return e;
        }

        public PaginatedList<Specialty> GetAllFiltered(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            var specialties = _context.Specialties.Include("Questions").Include("Country").Include("Career").AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                specialties = specialties.Where(s => s.Name.ToLower().Replace("à", "a")
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
                    specialties= specialties.OrderBy(s => s.SpecialtyID);
                    break;
                case "ID_desc":
                    specialties = specialties.OrderByDescending(s => s.SpecialtyID);
                    break;
                case "Specialty":
                    specialties = specialties.OrderBy(s => s.Name);
                    break;
                case "Specialty_desc":
                    specialties = specialties.OrderByDescending(s => s.Name);
                    break;
                case "Careers":
                    specialties = specialties.OrderBy(s => s.Career.Name);
                    break;
                case "Careers_desc":
                    specialties = specialties.OrderByDescending(s => s.Career.Name);
                    break;
                case "Countries":
                    specialties = specialties.OrderBy(s => s.Country.Name);
                    break;
                case "Countries_desc":
                    specialties = specialties.OrderByDescending(s => s.Country.Name);
                    break;
                case "Questions":
                    specialties = specialties.OrderBy(s => s.Questions.Count);
                    break;
                case "Questions_desc":
                    specialties = specialties.OrderByDescending(s => s.Questions.Count);
                    break;
                default:
                    specialties = specialties.OrderByDescending(s => s.SpecialtyID);
                    break;

            }

            int pageSize = 20;
            return PaginatedList<Specialty>.Create(specialties, pageNumber ?? 1, pageSize);

        }

        public Specialty GetById(int id)
        {
            var p =  _context.Specialties.Where(x => x.SpecialtyID == id).SingleOrDefault();

            return p;
        }

        public Specialty GetOrSave(Specialty specialty)
        {
            var specialtyDb = _context.Specialties.Where(x => x.Name == specialty.Name).SingleOrDefault();

            if (specialtyDb == null)
            {

                _context.Specialties.Add(specialty);
                _context.SaveChanges();

                return specialty;
            }
            else
                return specialtyDb;
        }

        public int TotalSpecialtiesCount()
        {
            return _context.Specialties.Count();
        }
        public int TotalFilteredCount(string searchString)
        {
            var specialties = _context.Specialties.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                specialties = specialties.Where(s => s.Name.Contains(searchString));
            }
            return specialties.Count();

        }

        public Specialty UpdateSpecialty(Specialty specialty)
        {
            var p = _context.Specialties.Where(x => x.SpecialtyID == specialty.SpecialtyID).SingleOrDefault();
            if (p != null)
            {
                p.Name = specialty.Name;
                p.CareerID = specialty.CareerID;
                p.CountryID = specialty.CountryID;
                
                _context.SaveChanges();
            }
            return p;
        }

    }
}
