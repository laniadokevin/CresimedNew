using System;
using System.Collections.Generic;
using System.Text;
using Cresimed.Core.Interfaces;
using Cresimed.Core.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq;
using Cresimed.Core.Entities.Const;
using Cresimed.Core.Entities.Database;

namespace Cresimed.Data.Repositories
{
    public class LogRepository : GenericRepository<Log>, ILogRepository
    {
        private readonly CresimedDBContext _context;

        public LogRepository(CresimedDBContext context) : base(context)
        {
            _context = context;

        }

        public void SaveLog(Log log)
        {
            _context.Logs.Add(log);
            _context.SaveChanges();
        }

    }
}