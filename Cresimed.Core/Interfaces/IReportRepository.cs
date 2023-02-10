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
    public interface IReportRepository : IGenericService<Report>
    {
        Report GetById(int id);
        List<Report> GetAll();
        PaginatedList<Report> GetAllFiltered(string sortOrder, string currentFilter, string searchString, int? pageNumber);
        Report InsertReport(Report Report);
        void DeleteReport(int id);
        int TotalReportsCount();
        int TotalFilteredCount(string searchString);
    }
}
