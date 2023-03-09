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
using System.ComponentModel.DataAnnotations;
using Cresimed.Core.Entities.ViewModel.Campus;
using System.Configuration;
using Cresimed.Core.Entities.Enum;
using Cresimed.Core.Entities.ViewModel.Admin;
using static Cresimed.Core.Entities.ViewModel.Admin.DashboardAdminViewModel;
using System.Drawing;
using System.Globalization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Cresimed.Data.Repositories
{
    public class SubscriptionRepository : GenericRepository<Subscription>, ISubscriptionRepository
    {
        private readonly CresimedDBContext _context;

        public SubscriptionRepository(CresimedDBContext context) : base(context)
        {
            _context = context;
        }

        public List<Subscription> GetAllSubscriptions()
        {
            return _context.Subscriptions.Include("User").ToList();
        }

        public DashboardAdminViewModel GetDashboardView()
        {
            var view = new DashboardAdminViewModel();

            view.MonthIncome = _context.Subscriptions.Where(x => x.DatePayed > DateTime.Now.AddDays(-30)).Sum(x => x.Value);
            view.LastMonthIncome = _context.Subscriptions.Where(x => x.DatePayed > DateTime.Now.AddDays(-60) && x.DatePayed < DateTime.Now.AddDays(-30)).Sum(x => x.Value);
            view.LastSixMonthsIncome = _context.Subscriptions.Where(x => x.DatePayed > DateTime.Now.AddDays(-180)).Sum(x => x.Value);
            view.YearIncome = _context.Subscriptions.Where(x => x.DatePayed > DateTime.Now.AddDays(-360)).Sum(x => x.Value);
            view.LastYearIncome = _context.Subscriptions.Where(x => x.DatePayed > DateTime.Now.AddDays(-720) && x.DatePayed < DateTime.Now.AddDays(-360)).Sum(x => x.Value);
            view.MonthUsers = _context.Users.Where(x => x.DateAdded > DateTime.Now.AddDays(-30)).Count();
            view.LastMonthUsers = _context.Users.Where(x => x.DateAdded > DateTime.Now.AddDays(-60) && x.DateAdded < DateTime.Now.AddDays(-30)).Count();
            view.LastSixMonthsUsers = _context.Users.Where(x => x.DateAdded > DateTime.Now.AddDays(-180)).Count();
            view.YearUsers = _context.Users.Where(x => x.DateAdded > DateTime.Now.AddDays(-360)).Count();
            view.LastYearUsers = _context.Users.Where(x => x.DateAdded > DateTime.Now.AddDays(-720) && x.DateAdded < DateTime.Now.AddDays(-360)).Count();


            return view;
        }

        public List<Subscription> GetLast10Subscriptions()
        {
            return _context.Subscriptions.Include("User").Where(x => x.DatePayed != null).OrderByDescending(x => x.DatePayed).Take(10).ToList();
        }

        public List<UsersByMonth> GetDataUsersLast6Months()
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            
            var data = _context.Users.Where(x => x.DateAdded > DateTime.Now.AddDays(-180)).Select(k => new { k.DateAdded.Year, k.DateAdded.Month, k }).GroupBy(x => new { x.Year, x.Month }, (key, group) => new UsersByMonth
            {
                Year =  (key.Year),

                Month = (key.Month),
                MonthName = dtinfo.GetAbbreviatedMonthName(key.Month),

                Count = group.Count()

            }).AsEnumerable().OrderBy(x => (x.Year))
              .ThenBy(x => (x.Month)).Take(6)
              .ToList();

            return data;
        }

        public Subscription InsertSubscription(Subscription subscription)
        {

            _context.Subscriptions.Add(subscription);
            _context.SaveChanges();

            return subscription;

        }

        public Subscription UpdateSubscription(int userID, string mpID, string mpStatus)
        {
            var sub = _context.Subscriptions.Where(x => x.UserID == userID && x.MPPaymentID == mpID).FirstOrDefault();

            if (sub != null)
            {
                if (mpStatus == "approved") {
                    sub.DatePayed = DateTime.Now;
                    sub.DateExpiracy = DateTime.Now.AddMonths(8);
                    sub.Status = (int)SubscriptionStatus.Approved;
                    sub.MPStatus = mpStatus;
                }

                if (mpStatus == "failure")
                {
                    sub.Status = (int)SubscriptionStatus.Deny;
                    sub.MPStatus = mpStatus;
                }

                if (mpStatus == "null")
                {
                    sub.Status = (int)SubscriptionStatus.Nulled;
                    sub.MPStatus = "NULL";
                }



                _context.SaveChanges();
                
                return sub;
            }
            else
                return null;
        }

        public List<SubsByAmount> GetDataAmountLast6Months()
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;

            var data = _context.Subscriptions.Where(x => x.DatePayed!=null && x.DatePayed > DateTime.Now.AddDays(-180)).Select(k => new { k.DatePayed.Value.Year, k.DatePayed.Value.Month, k.Value }).GroupBy(x => new { x.Year,x.Month }, (key, group) => new SubsByAmount
            {
                Year =  (key.Year),
                Month = (key.Month),
                MonthName = dtinfo.GetAbbreviatedMonthName(key.Month),
                Amount = (int)group.Sum(k => k.Value)

            }).OrderBy(x => (x.Year))
              .ThenBy(x => (x.Month))
              .ToList();


            return data;
        }
    }
}