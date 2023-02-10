using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cresimed.Core.Entities.Database
{
    public class Log
    {

        public int LogID { get; set; }
        public int lvl { get; set; }
        public string FileName { get; set; }
        public string Method { get; set; }
        public string ShortMessage { get; set; }
        public string FullMessage { get; set; }
        public string IpAddress { get; set; }
        public DateTime Date { get; set; }

    }
}
