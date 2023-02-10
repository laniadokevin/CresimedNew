using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cresimed.Core.Entities.Database
{
    public class Percentil
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int PercentilID  { get; set; }
        public int UserID  { get; set; }
        public int SpecialtyID  { get; set; }
        public int CorrectsAmount  { get; set; }
        public int IncorrectsAmount { get; set; }
        public int QuestionsAmount { get; set; }
        public int Value { get; set; }
        public virtual User User { get; set; }
        public virtual Specialty Specialty { get; set; }
    }

}