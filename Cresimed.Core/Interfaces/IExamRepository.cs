using System;
using System.Collections.Generic;
using System.Text;
using Cresimed.Core.Entities;
using System.Threading.Tasks;
using Cresimed.Core.Services;
using Cresimed.Core.Entities.Database;
using Cresimed.Core.Entities.ViewModel.Campus;
using Cresimed.Core.Entities.Base;

namespace Cresimed.Core.Interfaces
{
    public interface IExamRepository:IGenericService<Exam>
    {
        Exam GetById(int id);
        Exam GetNewExam(CreateExamIndexViewModel filters);
        PaginatedList<Exam> GetAllFiltered(int userID, string sortOrder, string currentFilter, string searchString, int? pageNumber);
        Exam InsertNewExam(List<Question> questions, CreateExamIndexViewModel filters);
        Exam AnswerExam(List<ExamDetail> details);
        int TotalExamsCount(int userID);
        List<Exam> GetAllExams(int userID);
        ExamStatsViewModel GetAllBySpecialty(int userID);
        List<Exam> GetLast5Exams(int userID);



    }
}
