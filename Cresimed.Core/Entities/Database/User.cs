using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cresimed.Core.Entities.Database
{
    [Table("Users")]
    public class User
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public bool Enable { get; set; }
        public string Email { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool Deleted { get; set; }
        public decimal UserAverage { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        
        [ForeignKey("PercentilID")]
        public virtual ICollection<Percentil> Percentils { get; set; }
    }
}
