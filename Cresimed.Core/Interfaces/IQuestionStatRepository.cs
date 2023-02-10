using System;
using System.Collections.Generic;
using System.Text;
using Cresimed.Core.Entities;
using System.Threading.Tasks;
using Cresimed.Core.Services;
using Cresimed.Core.Entities.Database;
using Cresimed.Core.Entities.Base;

namespace Cresimed.Core.Interfaces
{
    public interface IQuestionStatRepository : IGenericService<QuestionStat>
    {
        QuestionStat UpdateQuestionStat(List<ExamDetail> examDetails);

    }
}
