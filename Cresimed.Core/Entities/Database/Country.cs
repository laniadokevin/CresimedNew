using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cresimed.Core.Entities.Database
{
    public class Country
    {
        public Country()
        {
            Questions = new HashSet<Question>();
            Specialties = new HashSet<Specialty>();
            Careers = new HashSet<Career>();
        }
        public int CountryID { get; set; }
        public string Name { get; set; }

        [ForeignKey("CountryID")]
        public virtual ICollection<Question> Questions { get; set; }

        [ForeignKey("CountryID")]
        public virtual ICollection<Specialty> Specialties { get; set; }
        
        [ForeignKey("CountryID")]
        public virtual ICollection<Career> Careers { get; set; }
    }
}
