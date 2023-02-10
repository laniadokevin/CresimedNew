using System;
using System.Collections.Generic;
using System.Text;
using Cresimed.Core.Entities;
using System.Threading.Tasks;
using Cresimed.Core.Services;
using Cresimed.Core.Entities.Database;
using Cresimed.Core.Entities.Base;
using Cresimed.Core.Entities.ViewModel.Campus;

namespace Cresimed.Core.Interfaces
{
    public interface IPercentilRepository : IGenericService<Percentil>
    {
        Percentil UpdatePercentil(List<ExamDetail> examDetails);
        PercentilsViewModel GetPercentilsForUser(int userID);
        List<Percentil> GetListPercentilsForUser(int userID);

    }
}
