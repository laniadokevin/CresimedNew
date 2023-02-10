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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly CresimedDBContext _context;

        public CategoryRepository(CresimedDBContext context) : base(context)
        {
            _context = context;

        }

        public void DeleteCategory(int id)
        {
            Category Category = _context.Categories.Where(x => x.CategoryID == id).SingleOrDefault();
            _context.Remove(Category);
            _context.SaveChanges();
        }

        public List<Category> GetAll()
        {
            return _context.Categories.Include("Questions").ToList();

        }

        public Category GetOrSave(Category Category)
        {
            var CategoryDb = _context.Categories.Where(x => x.Name == Category.Name).SingleOrDefault();

            if (CategoryDb == null)
            {
                _context.Categories.Add(Category);
                _context.SaveChanges();

                return Category;
            }
            else
                return CategoryDb;

        }
        public Category UpdateCategory(Category Category)
        {
            var p = _context.Categories.Where(x => x.CategoryID == Category.CategoryID).SingleOrDefault();
            if (p != null)
            {
                p.Name = Category.Name;

                _context.SaveChanges();
            }
            return p;
        }
        public Category GetById(int id)
        {
            var p = _context.Categories.Where(x => x.CategoryID == id).SingleOrDefault();

            return p;
        }

        public PaginatedList<Category> GetAllFiltered(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {

            var categories = _context.Categories.Include("Questions").AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(s => s.Name.ToLower().Replace("à", "a")
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
                    categories = categories.OrderBy(s => s.CategoryID);
                    break;
                case "ID_desc":
                    categories = categories.OrderByDescending(s => s.CategoryID);
                    break;
                case "Category":
                    categories = categories.OrderBy(s => s.Name);
                    break;
                case "Category_desc":
                    categories = categories.OrderByDescending(s => s.Name);
                    break;
                case "Questions":
                    categories = categories.OrderBy(s => s.Questions.Count);
                    break;
                case "Questions_desc":
                    categories = categories.OrderByDescending(s => s.Questions.Count);
                    break;
                default:
                    categories = categories.OrderByDescending(s => s.CategoryID);
                    break;

            }

            int pageSize = 20;
            return PaginatedList<Category>.Create(categories, pageNumber ?? 1, pageSize);

        }

        public int TotalCategoriesCount()
        {
            return _context.Categories.Count();
        }

        public int TotalFilteredCount(string searchString)
        {
            var categories = _context.Categories.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(s => s.Name.Contains(searchString));
            }
            return categories.Count();

        }
    }
}
