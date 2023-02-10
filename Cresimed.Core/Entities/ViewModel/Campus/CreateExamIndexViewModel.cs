using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cresimed.Core.Entities.Database;

namespace Cresimed.Core.Entities.ViewModel.Campus
{
    public class CreateExamIndexViewModel
    {
        public CreateExamIndexViewModel()
        {
            QuestionsSpecialty = new List<int> ();
            Specialties = new List<Specialty>();
            Categories = new List<Category>();
        }
        public int UserID { get; set; }
        public bool Tutor { get; set; }
        public bool Timer { get; set; }
        public int TimeAmount { get; set; }
        public int QuestionsAmount { get; set; }
        public int QuestionsType { get; set; } // Todas / Nuevas  / Incorrectas 
        public List<int> QuestionsSpecialty { get; set; } 
        public int QuestionsCategory { get; set; } // Casos clínicos / Fundamentos


        public List<Specialty> Specialties { get; set; }
        public List<Category> Categories { get; set; }   

    }
}
