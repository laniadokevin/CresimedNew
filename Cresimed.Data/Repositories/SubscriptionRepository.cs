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

namespace Cresimed.Data.Repositories
{
    public class SubscriptionRepository : GenericRepository<Subscription>, ISubscriptionRepository
    {
        private readonly CresimedDBContext _context;

        public SubscriptionRepository(CresimedDBContext context) : base(context)
        {
            _context = context;
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
                    sub.MPStatus = mpStatus;
                }



                _context.SaveChanges();
                
                return sub;
            }
            else
                return null;
        }

    }
}
