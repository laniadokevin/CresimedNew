using System;
using System.Collections.Generic;
using System.Text;
using Cresimed.Core.Entities;
using System.Threading.Tasks;
using Cresimed.Core.Services;
using Cresimed.Core.Entities.Database;

namespace Cresimed.Core.Interfaces
{
    public interface ILogRepository:IGenericService<Log>
    {
        void SaveLog(Log log);

    }
}
