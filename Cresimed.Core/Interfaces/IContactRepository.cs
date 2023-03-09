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
    public interface IContactRepository : IGenericService<Contact>
    {
        Contact GetById(int id);
        List<Contact> GetAll();
        List<Contact> GetLast5();
        PaginatedList<Contact> GetAllFiltered(string sortOrder, string currentFilter, string searchString, int? pageNumber);
        Contact SaveContact(Contact Contact);
        void DeleteContact(int id);
        int TotalContactsCount();
        int TotalFilteredCount(string searchString);

    }
}
