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
    public interface ICareerRepository : IGenericService<Career>
    {
        Career GetById(int id);
        List<Career> GetAll();
        PaginatedList<Career> GetAllFiltered(string sortOrder, string currentFilter, string searchString, int? pageNumber);
        Career GetOrSave(Career Career);
        Career UpdateCareer(Career Career);
        void DeleteCareer(int id);
        int TotalCareersCount();
        int TotalFilteredCount(string searchString);

    }
}
