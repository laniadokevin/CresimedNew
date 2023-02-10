using Microsoft.AspNetCore.Http;
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
    public class QuestionGridViewModel
    {
        public QuestionGridViewModel()
        {
            NewQuestion = new Question();
            Careers = new List<Career>();
            Categories = new List<Category>();
            Countries = new List<Country>();
            Specialties = new List<Specialty>();
            NewAnswers = new List<Answer>();
            NewKeyWords = new List<KeyWord>();
        }
        public Question NewQuestion { get; set; }
        public PaginatedList<Question> Questions { get; set; }
        public List<Answer> NewAnswers { get; set; }
        public List<KeyWord> NewKeyWords { get; set; }
        public List<string> NewSpecialties{ get; set; }
        public IFormFile? File { get; set; }
        public IFormFile? AnswerFile { get; set; }
        public List<Career> Careers { get; set; }
        public List<Category> Categories { get; set; }
        public List<Country> Countries { get; set; }
        public List<Specialty> Specialties { get; set; }
    }
}
