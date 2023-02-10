using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cresimed.Core.Entities.Database
{
    public class QuestionStat
    {

        public int QuestionStatID { get; set; }
        public int QuestionID { get; set; }
        public int CorrectsAmount { get; set; }
        public int IncorrectsAmount { get; set; }
        public int TotalAmount { get; set; }
        public int Percentage { get; set; }
        public virtual Question Question { get; set; }

    }

}