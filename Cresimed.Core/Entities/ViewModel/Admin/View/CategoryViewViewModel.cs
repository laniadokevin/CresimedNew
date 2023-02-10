using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cresimed.Core.Entities.Database;

namespace Cresimed.Core.Entities.ViewModel.Admin.Edition
{
    public class CategoryViewViewModel
    {
        public CategoryViewViewModel()
        {
            Category = new Category();
        }
        public Category Category { get; set; }
    }
}
