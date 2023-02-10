using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cresimed.Core.Entities.Database
{
    public class Exam
    {
        public Exam()
        {
            ExamDetails = new List<ExamDetail>();
        }

        public int ExamID { get; set; }
        public int UserID { get; set; }
        public bool Tutor { get; set; }
        public bool Timer { get; set; }
        public int TimeAmount { get; set; }
        public int QuestionsAmount { get; set; }
        public int QuestionsType { get; set; } // Todas / Nuevas  / Incorrectas 
        public string QuestionsSpecialty { get; set; }
        public int QuestionsCategory { get; set; } // Casos clínicos / Fundamentos
        public DateTime DateStarted { get; set; }
        public DateTime? DateEnd { get; set; }

        [ForeignKey("ExamID")]
        public virtual ICollection<ExamDetail> ExamDetails { get; set; }
    }
}
