using System;
using System.Collections.Generic;
using System.Text;
using Cresimed.Core.Entities;
using System.Threading.Tasks;
using Cresimed.Core.Services;
using Cresimed.Core.Entities.Database;

namespace Cresimed.Core.Interfaces
{
    public interface IKeyWordRepository : IGenericService<KeyWord>
    {

        KeyWord GetById(int id);
        List<KeyWord> GetAll ();
        KeyWord GetOrSave(KeyWord keyWord);
        List<KeyWord> GetKeyWordsForQuestion(int id);
        int UpdateKeyWord(KeyWord keyWord);
        int DeleteKeyWord(int id);


    }
}
