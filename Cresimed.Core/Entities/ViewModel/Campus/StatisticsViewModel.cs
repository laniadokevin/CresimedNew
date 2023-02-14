using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cresimed.Core.Entities.Database;

namespace Cresimed.Core.Entities.ViewModel.Campus
{
    public class StatisticsViewModel
    {
        public ExamStatsViewModel ExamStats { get; set; }
        public List<decimal> PercentilChart { get; set; }
        public List<Percentil> Percentils { get; set; }

        public int CantExams { get; set; }
        public double TimeAverageQuestion { get; set; }
        public int TotalTimeSpent { get; set; }


    }
}
