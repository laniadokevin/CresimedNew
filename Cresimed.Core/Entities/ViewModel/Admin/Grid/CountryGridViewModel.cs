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
    public class CountryGridViewModel
    {
        public CountryGridViewModel()
        {
            NewCountry = new Country();
        }
        public Country NewCountry { get; set; }
        public PaginatedList<Country> Countries { get; set; }
    }
}
