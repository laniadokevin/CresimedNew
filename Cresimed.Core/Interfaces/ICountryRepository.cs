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
    public interface ICountryRepository : IGenericService<Country>
    {

        Country GetById(int id);
        List<Country> GetAll();
        PaginatedList<Country> GetAllFiltered(string sortOrder, string currentFilter, string searchString, int? pageNumber);
        Country GetOrSave(Country Country);
        Country UpdateCountry(Country Country);
        void DeleteCountry(int id);
        int TotalCountriesCount();
        int TotalFilteredCount(string searchString);

    }
}
