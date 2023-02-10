using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cresimed.Core.Entities.Database;

namespace Cresimed.Core.Entities.ViewModel.Admin.Edition
{
    public class QuestionViewViewModel
    {
        public QuestionViewViewModel()
        {
            Question = new Question();
            Answers = new List<Answer>();
            NewAnswers = new List<Answer>();
            Careers = new List<Career>();
            Categories = new List<Category>();
            Specialties = new List<Specialty>();
            Countries = new List<Country>();
            KeyWords= new List<KeyWord>();
            NewKeyWords= new List<KeyWord>();
        }
        public Question Question { get; set; }
        public List<Answer> Answers { get; set; }
        public List<Answer> NewAnswers { get; set; }
        public List<Career> Careers { get; set; }
        public List<Category> Categories { get; set; }
        public List<KeyWord> KeyWords { get; set; }
        public List<KeyWord> NewKeyWords { get; set; }
        public IFormFile? File { get; set; }
        public IFormFile? AnswerFile { get; set; }
        public List<Specialty> Specialties { get; set; }
        public List<Country> Countries { get; set; }
    }
}
