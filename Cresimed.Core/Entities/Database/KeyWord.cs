using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cresimed.Core.Entities.Database
{
    public class KeyWord
    {
        public int KeyWordID { get; set; }
        public string Text { get; set; }
        public int QuestionID { get; set; }
        public virtual Question Question { get; set; }
    }
}
