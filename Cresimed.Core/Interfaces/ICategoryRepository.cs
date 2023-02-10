using System;
using System.Collections.Generic;
using System.Text;
using Cresimed.Core.Entities;
using System.Threading.Tasks;
using Cresimed.Core.Services;
using Cresimed.Core.Entities.Database;
using Cresimed.Core.Entities.Base;

namespace Cresimed.Core.Interfaces
{
    public interface ICategoryRepository : IGenericService<Category>
    {

        Category GetById(int id);
        List<Category> GetAll();
        PaginatedList<Category> GetAllFiltered(string sortOrder, string currentFilter, string searchString, int? pageNumber);
        Category GetOrSave(Category Category);
        Category UpdateCategory(Category Category);
        void DeleteCategory(int id);
        int TotalCategoriesCount();
        int TotalFilteredCount(string searchString);
    }
}
