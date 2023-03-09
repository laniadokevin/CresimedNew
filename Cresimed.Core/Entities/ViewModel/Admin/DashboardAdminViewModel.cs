using Cresimed.Core.Entities.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cresimed.Core.Entities.ViewModel.Admin
{
    public class DashboardAdminViewModel
    {
        public decimal MonthIncome { get; set; }
        public decimal LastMonthIncome { get; set; }
        public decimal LastSixMonthsIncome { get; set; }
        public decimal YearIncome { get; set; }
        public decimal LastYearIncome { get; set; }
        public int MonthUsers { get; set; }
        public int LastMonthUsers { get; set; }
        public int LastSixMonthsUsers { get; set; }    
        public int YearUsers { get; set; }    
        public int LastYearUsers { get; set; }
        public List<UsersByMonth> LineBarUsers { get; set; }
        public List<SubsByAmount> LineBarAmount { get; set; }
        public List<Subscription> UsersSubscribedTable { get; set; }
        public List<Contact> Contacts { get; set; }

        public class UsersByMonth
        {
            public int Year { get; set; }
            public int Month { get; set; }
            public string MonthName { get; set; }
            
            public int Count { get; set; }

        }
        public class SubsByAmount
        {
            public int Year { get; set; }
            public int Month { get; set; }
            public string MonthName { get; set; }

            public int Amount { get; set; }

        }

    }
}
