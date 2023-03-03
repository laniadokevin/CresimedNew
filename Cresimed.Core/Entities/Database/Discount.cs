using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cresimed.Core.Entities.Database
{
    public class Discount
    {
        public int DiscountID { get; set; }
        public string KeyDiscount { get; set; }
        public int ValueDiscount { get; set; }
        public bool Enable  { get; set; }
        
    }
}
