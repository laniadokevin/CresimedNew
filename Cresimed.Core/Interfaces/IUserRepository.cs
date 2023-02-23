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
    public interface IUserRepository:IGenericService<User>
    {

        User GetById(int id);
        List<User> GetAll();
        PaginatedList<User> GetAllFiltered(string sortOrder, string currentFilter, string searchString, int? pageNumber, int rolID);
        int TotalUsersCount();
        int TotalFilteredCount(string searchString, int roleID);
        User ProcessLoginAdmin(string username, string password);
        User ProcessLoginCampus(string username, string password);
        User EnableOrDisable(int id);
        User SubscribeUser(int userID,int status);
        User InsertUser(User user);
        User UpdateUser(User user);
        void DeleteUser(int id);
        bool CheckUserNameDisponibility(string name);
        bool CheckEmailDisponibility(string email);
        bool UpdatePwd(string password);
        User UpdateUserAverage(int userID, decimal average);
        List<decimal> GetPercentil(int userID);

    }
}
