using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cresimed.Core.Entities.Database
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }

        [ForeignKey("CategoryID")]
        public virtual ICollection<Question> Questions { get; set; }
    }
}
