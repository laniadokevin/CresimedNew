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
    public class DiscountRepository : GenericRepository<Discount>,IDiscountRepository
    {
        private readonly CresimedDBContext _context;

        public DiscountRepository(CresimedDBContext context) : base(context)
        {
            _context = context;
        
        }


        public void DeleteDiscount(int id)
        {
            Discount discount = _context.Discounts.Where(x => x.DiscountID == id).SingleOrDefault();
            if (discount != null)
            {
                _context.Remove(discount);
                _context.SaveChanges();
            }
        }

        public List<Discount> GetAll()
        {
            return _context.Discounts.ToList();

        }

        public PaginatedList<Discount> GetAllFiltered(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {

            var discounts = _context.Discounts.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                discounts = discounts.Where(s => s.KeyDiscount.ToLower().Contains(searchString));
            }
            switch (sortOrder)
            {

                case "ID":
                    discounts = discounts.OrderBy(s => s.DiscountID);
                    break;
                case "ID_desc":
                    discounts = discounts.OrderByDescending(s => s.DiscountID);
                    break;
                default:
                    discounts = discounts.OrderByDescending(s => s.DiscountID);
                    break;

            }

            int pageSize = 20;
            return PaginatedList<Discount>.Create(discounts, pageNumber ?? 1, pageSize);

        }


        public Discount InsertDiscount(Discount discount)
        {
            var discountDb = _context.Discounts.Where(x => x.KeyDiscount == discount.KeyDiscount).SingleOrDefault();

            if (discountDb == null)
            {
                _context.Discounts.Add(discount);
                _context.SaveChanges();

                return discount;
            }
            else
                return null;

        }
        public Discount UpdateDiscount(Discount Discount)
        {
            var p = _context.Discounts.Where(x => x.DiscountID == Discount.DiscountID).SingleOrDefault();
            if (p != null)
            {
                p.ValueDiscount = Discount.ValueDiscount;
                p.Enable = Discount.Enable;

                _context.SaveChanges();
            }
            return p;
        }

        public Discount GetById(string kd)
        {
            var p = _context.Discounts.Where(x => x.KeyDiscount == kd).SingleOrDefault();

            return p;

        }

        public int TotalDiscountsCount()
        {
            return _context.Discounts.Count();
        }

        public int TotalFilteredCount(string searchString)
        {

            var discounts = _context.Discounts.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                discounts = discounts.Where(s => s.KeyDiscount.Contains(searchString));
            }

            return discounts.Count();
        }

        public Discount GetById(int id)
        {
            var p = _context.Discounts.Where(x => x.DiscountID == id).SingleOrDefault();

            return p;
        }
    }
}
