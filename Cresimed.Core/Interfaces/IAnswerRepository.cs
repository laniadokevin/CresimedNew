using System;
using System.Collections.Generic;
using System.Text;
using Cresimed.Core.Entities;
using System.Threading.Tasks;
using Cresimed.Core.Services;
using Cresimed.Core.Entities.Database;
using Microsoft.EntityFrameworkCore;
using Cresimed.Core.Entities.Base;

namespace Cresimed.Core.Interfaces
{
    public interface IAnswerRepository : IGenericService<Answer>
    {

        Answer InsertAnswer(Answer answer);
        Answer GetById(int id);
        List<Answer> GetAnswersForQuestion (int QuestionID);
        Answer UpdateAnswer(Answer Answer);
        int DeleteAnswer(int id);

    }
}
