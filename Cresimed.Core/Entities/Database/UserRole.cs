using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cresimed.Core.Entities.Database
{
    [Table("User_Role")]
    public partial class UserRole
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public bool Enable { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }
    }
}
