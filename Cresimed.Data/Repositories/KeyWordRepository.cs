using System;
using System.Collections.Generic;
using System.Text;
using Cresimed.Core.Interfaces;
using Cresimed.Core.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq;
using Cresimed.Core.Entities.Database;

namespace Cresimed.Data.Repositories
{
    public class KeyWordRepository : GenericRepository<KeyWord>,IKeyWordRepository
    {
        private readonly CresimedDBContext _context;

        public KeyWordRepository(CresimedDBContext context) : base(context)
        {
            _context = context;
        
        }

        public int DeleteKeyWord(int id)
        {
            
            KeyWord keyWord = _context.KeyWords.Where(x => x.KeyWordID == id).SingleOrDefault();
            
            int questionid = keyWord.QuestionID;
            
            _context.Remove(keyWord);
            _context.SaveChanges();

            return questionid;
        }

        public List<KeyWord> GetAll()
        {
            return _context.KeyWords.ToList();
        }

        public KeyWord GetById(int id)
        {
            var p = _context.KeyWords.Where(x => x.KeyWordID == id).SingleOrDefault();
           

            return p;
        }

        public List<KeyWord> GetKeyWordsForQuestion(int id)
        {
            return _context.KeyWords.Where(x=>x.QuestionID == id).ToList();
        }

        public KeyWord GetOrSave(KeyWord keyWord)
        {


            _context.KeyWords.Add(keyWord);
            _context.SaveChanges();

            return keyWord;
        }

        public int UpdateKeyWord(KeyWord keyWord)
        {
            var p = _context.KeyWords.Where(x => x.KeyWordID == keyWord.KeyWordID).SingleOrDefault();
            if (p != null)
            {
                
                p.Text= keyWord.Text;
                
                _context.SaveChanges();
            }
            return p.QuestionID;
        }
    }
}
