using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cresimed.Core.Entities.Database
{
    public class Specialty
    {
        public Specialty()
        {
            Questions = new HashSet<Question>();
        }
        public int SpecialtyID { get; set; }
        public string Name { get; set; }
        public int CareerID { get; set; }
        public int CountryID { get; set; }
    
        public virtual Career Career { get; set; }
        public virtual Country Country { get; set; }
        [ForeignKey("SpecialtyID")]
        public virtual ICollection<Question> Questions{ get; set; }
    }
}
