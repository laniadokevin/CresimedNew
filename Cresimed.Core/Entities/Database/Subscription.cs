using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cresimed.Core.Entities.Database
{
    public class Subscription
    {

        public int SubscriptionID { get; set; }
        public int UserID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DatePayed { get; set; }
        public DateTime? DateExpiracy { get; set; }
        public decimal Value { get; set; }
        public int Status { get; set; }
        public string MPPaymentID { get; set; }
        public string? MPStatus { get; set; }

        public virtual User User { get; set; }
    }
}
