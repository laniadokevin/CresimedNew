using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cresimed.Core.Entities.Database;

namespace Cresimed.Core.Entities.ViewModel.Admin.Edition
{
    public class SpecialtyViewViewModel
    {
        public SpecialtyViewViewModel()
        {
            Specialty = new Specialty();
            Countries = new List<Country>();
            Careers = new List<Career>();
        }
        public Specialty Specialty { get; set; }
        public List<Country> Countries { get; set; }
        public List<Career> Careers { get; set; }
    }
}
