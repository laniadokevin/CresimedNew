using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cresimed.Core.Entities.Database;

namespace Cresimed.Core.Entities.ViewModel.Campus
{
    public class ExamStatsViewModel
    {
        public ExamStatsViewModel()
        {
            CountBySpecialties = new List<CorrectBySpecialty>();
        }

        public int TotalCorrect { get; set; }
        public int TotalIncorrect { get; set; }
        public int TotalCount { get; set; }
        public List<CorrectBySpecialty> CountBySpecialties { get; set; }

    }
    public class CorrectBySpecialty
    {
        public string Name { get; set; }
        public int Corrects { get; set; }
        public int Incorrects { get; set; }
    }
}
