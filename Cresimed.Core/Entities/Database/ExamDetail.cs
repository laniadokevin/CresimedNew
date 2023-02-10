using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cresimed.Core.Entities.Database
{
    public class ExamDetail
    {
        public int ExamDetailID { get; set; }
        public int ExamID { get; set; }
        public int QuestionID { get; set; }
        public int? AnswerID { get; set; }
        public bool? IsCorrect { get; set; }
        public int? Time  { get; set; }


        [ForeignKey("QuestionID")]
        public virtual Question Question { get; set; }
    }
}
