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
    public interface ISpecialtyRepository : IGenericService<Specialty>
    {

        Specialty GetById(int id);
        List<Specialty> GetAll ();
        PaginatedList<Specialty> GetAllFiltered(string sortOrder, string currentFilter, string searchString, int? pageNumber);
        Specialty GetOrSave(Specialty specialty);
        Specialty UpdateSpecialty(Specialty specialty);
        void DeleteSpecialty(int id);
        int TotalSpecialtiesCount();
        int TotalFilteredCount(string searchString);


    }
}
