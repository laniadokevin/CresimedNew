using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cresimed.Core.Entities.Database;

namespace Cresimed.Core.Entities.ViewModel.Admin.Edition
{
    public class UserViewViewModel
    {
        public UserViewViewModel()
        {
            User = new User();
            Roles = new List<Role>();
            NewRoles = new List<string>();
        }
        public User User { get; set; }
        public List<Role> Roles { get; set; }
        public List<string> NewRoles { get; set; }
    }
}
