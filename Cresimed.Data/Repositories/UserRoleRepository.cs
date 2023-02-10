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
using Cresimed.Core.Entities.Base;

namespace Cresimed.Data.Repositories
{
    public class UserRoleRepository : GenericRepository<UserRole>,IUserRoleRepository
    {
        private readonly CresimedDBContext _context;

        public UserRoleRepository(CresimedDBContext context) : base(context)
        {
            _context = context;
        
        }

        public List<UserRole> GetUserRoles(int userID)
        {
            return _context.UserRoles.Include("Role").Where(x=>x.UserID == userID).ToList();
            
        }

        public void InsertOrUpdateUserRole(UserRole userRole)
        {
            var p = _context.UserRoles.Where(x => x.UserID == userRole.UserID && x.RoleID == userRole.RoleID).SingleOrDefault();

            if (p != null)
            {
                p.Enable = userRole.Enable; 
            }
            else
            {
                _context.UserRoles.Add(userRole);
            }
            _context.SaveChanges();
        }
    }
}
