using System;
using System.Collections.Generic;
using System.Text;
using Cresimed.Core.Entities;
using System.Threading.Tasks;
using Cresimed.Core.Services;
using Cresimed.Core.Entities.Database;
using Cresimed.Core.Entities.Base;

namespace Cresimed.Core.Interfaces
{
    public interface IUserRoleRepository:IGenericService<UserRole>
    {
        void InsertOrUpdateUserRole(UserRole userRole);
        List<UserRole> GetUserRoles(int userID);
    }
}
