using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cresimed.Core.Entities.Database
{
    public class Faq
    {
        public int FaqID { get; set; }
        public string Subject { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
