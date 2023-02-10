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
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private readonly CresimedDBContext _context;

        public RoleRepository(CresimedDBContext context) : base(context)
        {
            _context = context;

        }

        public List<Role> GetAll()
        {
            return _context.Roles.ToList();
        }

        public void SaveRole(Role Role)
        {
            _context.Roles.Add(Role);
            _context.SaveChanges();
        }

    }
}