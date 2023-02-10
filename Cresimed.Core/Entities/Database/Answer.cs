using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cresimed.Core.Entities.Database
{
    public class Answer
    {
        public int AnswerID { get; set; }
        public int QuestionID { get; set; }
        public bool IsCorrect { get; set; }
        public string Text { get; set; }
        public int Ratio { get; set; }

        public virtual Question Question { get; set; }
    }
}
