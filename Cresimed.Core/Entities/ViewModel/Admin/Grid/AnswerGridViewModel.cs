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
    public class AnswerGridViewModel
    {
        public AnswerGridViewModel()
        {
            NewAnswer = new Answer();
            Question = new Question();
        }
        public Answer NewAnswer { get; set; }
        public PaginatedList<Answer> Answers { get; set; }
        public Question Question { get; set; }
    }
}
