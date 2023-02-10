using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cresimed.Core.Entities.Database
{
    public class Report
    {
        public int ReportID {get;set;}
        public int Type {get;set;}
        public int QuestionID {get;set;}
        public int UserID {get;set;}
        public string Message { get; set; }
        
        [ForeignKey("QuestionID")]
        public virtual Question Question { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
}
