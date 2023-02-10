using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cresimed.Core.Entities.Database
{
    public class Question
    {

        public Question()
        {
            Answers = new HashSet<Answer>();
        }
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public int CountryID { get; set; }
        public int CareerID { get; set; }
        public int SpecialtyID { get; set; }
        public int CategoryID { get; set; }
        public int UniqueIdentity { get; set; }
        public string? Explanation { get; set; }
        public string? QuestionImage { get; set; }
        public string? ExplanationImage { get; set; }
        public virtual Country Country { get; set; }
        public virtual Career Career { get; set; }
        public virtual Specialty Specialty { get; set; }
        public virtual Category Category { get; set; }
        public virtual QuestionStat QuestionStat { get; set; }

        [ForeignKey("QuestionID")]
        public virtual ICollection<Answer> Answers { get; set; }
        [ForeignKey("QuestionID")]
        public virtual IList<KeyWord> KeyWords { get; set; }

    }

}