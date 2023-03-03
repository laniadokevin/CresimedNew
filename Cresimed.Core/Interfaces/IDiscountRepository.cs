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
    public interface IDiscountRepository : IGenericService<Discount>
    {

        Discount GetById(string id);
        Discount GetById(int id);
        List<Discount> GetAll();
        PaginatedList<Discount> GetAllFiltered(string sortOrder, string currentFilter, string searchString, int? pageNumber);
        Discount InsertDiscount(Discount Discount);
        Discount UpdateDiscount(Discount Discount);
        void DeleteDiscount(int id);
        int TotalDiscountsCount();
        int TotalFilteredCount(string searchString);

    }
}
