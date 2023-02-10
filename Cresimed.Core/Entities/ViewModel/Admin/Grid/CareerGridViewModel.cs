using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cresimed.Core.Entities.Base;
using Cresimed.Core.Entities.Database;

namespace Cresimed.Core.Entities.ViewModel.Admin.Grid
{
    public class CareerGridViewModel
    {
        public CareerGridViewModel()
        {
            NewCareer = new Career();
            Countries = new List<Country>();
        }
        public Career NewCareer { get; set; }
        public PaginatedList<Career> Careers { get; set; }
        public List<Country> Countries { get; set; }
    }
}
