using System;
using System.Collections.Generic;
using System.Text;
using Cresimed.Core.Entities;
using System.Threading.Tasks;
using Cresimed.Core.Services;
using Cresimed.Core.Entities.Database;
using Cresimed.Core.Entities.Base;
using Cresimed.Core.Entities.ViewModel.Campus;
using Cresimed.Core.Entities.ViewModel.Admin;
using static Cresimed.Core.Entities.ViewModel.Admin.DashboardAdminViewModel;

namespace Cresimed.Core.Interfaces
{
    public interface ISubscriptionRepository : IGenericService<Subscription>
    {
     
        Subscription InsertSubscription(Subscription subscription);
        Subscription UpdateSubscription(int userID, string mpID, string mpStatus);
        List<Subscription> GetAllSubscriptions();
        List<Subscription> GetLast10Subscriptions();
        DashboardAdminViewModel GetDashboardView();
        List<UsersByMonth> GetDataUsersLast6Months();
        List<SubsByAmount> GetDataAmountLast6Months();



    }
}
