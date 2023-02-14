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
    public class CountryRepository : GenericRepository<Country>,ICountryRepository
    {
        private readonly CresimedDBContext _context;

        public CountryRepository(CresimedDBContext context) : base(context)
        {
            _context = context;
        
        }


        public void DeleteCountry(int id)
        {
            Country Country = _context.Countries.Where(x => x.CountryID == id).SingleOrDefault();
            _context.Remove(Country);
            _context.SaveChanges();
        }

        public List<Country> GetAll()
        {
            return _context.Countries.Include("Questions").Include("Careers").Include("Specialties").AsSplitQuery().ToList();

        }
        public PaginatedList<Country> GetAllFiltered(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {

            var countries = _context.Countries.Include("Questions").Include("Careers").Include("Specialties").AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                countries = countries.Where(s => s.Name.ToLower().Replace("à", "a")
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
                    countries = countries.OrderBy(s => s.CountryID);
                    break;
                case "ID_desc":
                    countries = countries.OrderByDescending(s => s.CountryID);
                    break;
                case "Country":
                    countries = countries.OrderBy(s => s.Name);
                    break;
                case "Country_desc":
                    countries = countries.OrderByDescending(s => s.Name);
                    break;
                case "Careers":
                    countries = countries.OrderBy(s => s.Careers.Count);
                    break;
                case "Careers_desc":
                    countries = countries.OrderByDescending(s => s.Careers.Count);
                    break;
                case "Specialties":
                    countries = countries.OrderBy(s => s.Specialties.Count);
                    break;
                case "Specialties_desc":
                    countries = countries.OrderByDescending(s => s.Specialties.Count);
                    break;
                case "Questions":
                    countries = countries.OrderBy(s => s.Questions.Count);
                    break;
                case "Questions_desc":
                    countries = countries.OrderByDescending(s => s.Questions.Count);
                    break;
                default:
                    countries = countries.OrderByDescending(s => s.CountryID);
                    break;

            }

            int pageSize = 20;
            return PaginatedList<Country>.Create(countries, pageNumber ?? 1, pageSize);

        }


        public Country GetOrSave(Country Country)
        {
            var CountryDb = _context.Countries.Where(x => x.Name == Country.Name).SingleOrDefault();

            if (CountryDb == null)
            {
                _context.Countries.Add(Country);
                _context.SaveChanges();

                return Country;
            }
            else
                return CountryDb;

        }
        public Country UpdateCountry(Country Country)
        {
            var p = _context.Countries.Where(x => x.CountryID == Country.CountryID).SingleOrDefault();
            if (p != null)
            {
                p.Name = Country.Name;
                p.CountryID = Country.CountryID;

                _context.SaveChanges();
            }
            return p;
        }
        public Country GetById(int id)
        {
            var p = _context.Countries.Where(x => x.CountryID == id).SingleOrDefault();

            return p;
        }

        public int TotalCountriesCount()
        {
            return _context.Countries.Count();
        }

        public int TotalFilteredCount(string searchString)
        {

            var countries = _context.Countries.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                countries = countries.Where(s => s.Name.Contains(searchString));
            }

            return countries.Count();
        }
    }
}
