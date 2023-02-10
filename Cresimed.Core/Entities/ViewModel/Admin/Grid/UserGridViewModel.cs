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
    public class UserGridViewModel
    {
        public UserGridViewModel()
        {
            NewUser = new User();
            NewRoles = new List<string>();
            Roles = new List<Role>();
        }
        public User NewUser { get; set; }
        public List<string> NewRoles { get; set; }
        public List<Role> Roles { get; set; }
        public PaginatedList<User> Users { get; set; }
    }
}
