using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cresimed.Core.Entities.Database;

namespace Cresimed.Core.Entities.ViewModel.Admin.Edition
{
    public class ContactViewViewModel
    {
        public ContactViewViewModel()
        {
            Contact = new Contact();
        }
        public Contact Contact { get; set; }
    }
}
