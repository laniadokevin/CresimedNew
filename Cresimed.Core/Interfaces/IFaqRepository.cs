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
    public interface IFaqRepository : IGenericService<Faq>
    {
        Faq GetById(int id);
        List<Faq> GetAll();
        PaginatedList<Faq> GetAllFiltered(string sortOrder, string currentFilter, string searchString, int? pageNumber);
        Faq InsertFaq(Faq Faq);
        Faq UpdateFaq(Faq Faq);
        void DeleteFaq(int id);
        int TotalFaqsCount();
        int TotalFilteredCount(string searchString);
    }
}
