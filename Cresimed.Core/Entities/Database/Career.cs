using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cresimed.Core.Entities.Database
{
    public class Career
    {
        public int CareerID { get; set; }
        public string Name { get; set; }
        public int CountryID { get; set; }
        public virtual Country Country { get; set; }

        [ForeignKey("CareerID")]
        public virtual ICollection<Question> Questions { get; set; }

        [ForeignKey("CareerID")]
        public virtual ICollection<Specialty> Specialties { get; set; }
    }
}
