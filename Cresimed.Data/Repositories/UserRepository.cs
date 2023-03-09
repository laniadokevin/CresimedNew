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
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Xml.Linq;
using Cresimed.Core.Entities.Enum;

namespace Cresimed.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly CresimedDBContext _context;

        public UserRepository(CresimedDBContext context) : base(context)
        {
            _context = context;

        }

        public List<User> GetAll()
        {
            var u = _context.Users.Where(x => x.Deleted == false).Include("UserRoles").Include("UserRoles.Role").ToList();

            return u;
        }

        public PaginatedList<User> GetAllFiltered(string sortOrder, string currentFilter, string searchString, int? pageNumber, int rolID)
        {

            var user = _context.Users.Where(x => x.Deleted == false).Include("UserRoles").Include("UserRoles.Role").AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {

                user = user.Where(s => (s.FullName.ToLower().Replace("à", "a")
                .Replace("â", "a")
                .Replace("ä", "a")
                .Replace("ç", "c")
                .Replace("é", "e")
                .Replace("è", "e")
                .Replace("ê", "e")
                .Replace("ë", "e")
                .Replace("î", "i")
                .Replace("ï", "i")
                .Replace("ô", "o")
                .Replace("ù", "u")
                .Replace("û", "u")
                .Replace("ü", "u").Contains(searchString) || s.Email.ToLower().Replace("à", "a")
                .Replace("â", "a")
                .Replace("ä", "a")
                .Replace("ç", "c")
                .Replace("é", "e")
                .Replace("è", "e")
                .Replace("ê", "e")
                .Replace("ë", "e")
                .Replace("î", "i")
                .Replace("ï", "i")
                .Replace("ô", "o")
                .Replace("ù", "u")
                .Replace("û", "u")
                .Replace("ü", "u").Contains(searchString) || s.Username.ToLower().Replace("à", "a")
                .Replace("â", "a")
                .Replace("ä", "a")
                .Replace("ç", "c")
                .Replace("é", "e")
                .Replace("è", "e")
                .Replace("ê", "e")
                .Replace("ë", "e")
                .Replace("î", "i")
                .Replace("ï", "i")
                .Replace("ô", "o")
                .Replace("ù", "u")
                .Replace("û", "u")
                .Replace("ü", "u").Contains(searchString)));
            }

            if (rolID > 0)
            {
                user = user.Where(s => s.UserRoles.Where(x => x.RoleID == rolID).SingleOrDefault().Enable);
            }
            switch (sortOrder)
            {

                case "ID":
                    user = user.OrderBy(s => s.UserID);
                    break;
                case "ID_desc":
                    user = user.OrderByDescending(s => s.UserID);
                    break;
                case "Username":
                    user = user.OrderBy(s => s.Username);
                    break;
                case "Username_desc":
                    user = user.OrderByDescending(s => s.Username);
                    break;
                case "Fullname":
                    user = user.OrderBy(s => s.FullName);
                    break;
                case "Fullname_desc":
                    user = user.OrderByDescending(s => s.FullName);
                    break;
                case "Email":
                    user = user.OrderBy(s => s.Email);
                    break;
                case "Email_desc":
                    user = user.OrderByDescending(s => s.Email);
                    break;
                case "Enabled":
                    user = user.OrderBy(s => s.Enable);
                    break;
                case "Enabled_desc":
                    user = user.OrderByDescending(s => s.Enable);
                    break;
                default:
                    user = user.OrderByDescending(s => s.UserID);
                    break;
            }

            int pageSize = 20;
            return PaginatedList<User>.Create(user, pageNumber ?? 1, pageSize);

        }
        
        public User GetById(int id)
        {
            var p = _context.Users.Include(x => x.UserRoles.Where(a => a.Enable == true)).Where(x => x.UserID == id).SingleOrDefault();


            return p;
        }

        public User processLogin(string username, string password)
        {
            var user = _context.Users
                .Where(x => x.Deleted == false)
                .Include(x => x.Subscriptions)
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .SingleOrDefault(a => a.Username.Equals(username) && a.Enable == true);

            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    //if(user.Subscriptions.Any(x=>x.DatePayed.))
                    return user;
                }
            }
          

            return null;
        }

        public User EnableOrDisable(int id)
        {
            var u = _context.Users.Where(x => x.UserID == id).SingleOrDefault();

            u.Enable = !u.Enable;

            _context.SaveChanges();

            return u;
        }

        public User InsertUser(User user)
        {
            try
            {
                user.DateAdded = DateTime.Now;
                user.Deleted = false;

                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return null;
            }

            return user;
        }

        public void DeleteUser(int id)
        {
            User user = _context.Users.Where(x => x.UserID == id).SingleOrDefault();
            user.Deleted = true;
            user.DateDeleted = DateTime.Now;

            _context.SaveChanges();
        }

        public User UpdateUser(User user)
        {
            var p = _context.Users.Include("UserRoles").Where(x => x.UserID == user.UserID).SingleOrDefault();
            if (p != null)
            {
                p.Username = user.Username;
                if (user.Password != null)
                    p.Password = user.Password;
                p.FullName = user.FullName;
                p.Enable = user.Enable;
                p.Email = user.Email;
                if (user.Password != null)
                    p.DateDeleted = user.DateDeleted;
                p.Deleted = user.Deleted;
                if (user.UserAverage != 0)
                    p.UserAverage = user.UserAverage ;
                p.Country = user.Country ;
                p.University = user.University ;
                p.Province = user.Province ;
                p.LastYear = user.LastYear ;
                
                _context.SaveChanges();
            }
            return p;

        }

        public int TotalUsersCount()
        {
            return _context.Users.Where(x => x.Deleted == false).Include("UserRoles").Include("UserRoles.Role").Count();
        }

        public int TotalFilteredCount(string searchString, int roleID)
        {

            var user = _context.Users.Where(x => x.Deleted == false).Include("UserRoles").Include("UserRoles.Role").AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                user = user.Where(s => (s.FullName.Contains(searchString) || s.Email.Contains(searchString) || s.Username.Contains(searchString)));
            }

            if (roleID > 0)
            {
                user = user.Where(s => s.UserRoles.Where(x => x.RoleID == roleID).SingleOrDefault().Enable);
            }
            return user.Count();

        }

        public bool CheckUserNameDisponibility(string name)
        {
            return _context.Users.Any(x => x.Username.ToLower() == name.ToLower());
        }

        public bool CheckEmailDisponibility(string email)
        {
            return _context.Users.Any(x => x.Email.ToLower() == email.ToLower());
        }

        public bool UpdatePwd(string password)
        {
            throw new NotImplementedException();
        }

        public User UpdateUserAverage(int userID, decimal average)
        {
            var user = _context.Users.Where(x => x.UserID == userID).SingleOrDefault();
            if (user != null)
            {
                user.UserAverage = average;
                _context.SaveChanges();
            }
            return user;
        }

        public List<decimal> GetPercentil(int userID)
        {
            List<decimal> rta = new List<decimal>();
            decimal percentil = 0;
            decimal average = 0;

            var userAverage = _context.Users
                .Where(x => x.UserID == userID)
                .SingleOrDefault()
                .UserAverage;

            var averagesList = _context.Users
                .Where(x => x.Enable == true
                        && x.UserAverage > 0)
                .OrderByDescending(x => x.UserAverage)
                .Select(x => x.UserAverage)
                .ToList();

            var totUsers = averagesList.Count();
            var underUser = averagesList.Where(x => x < userAverage).Count();

            percentil = Decimal.Round((decimal)underUser / (decimal)totUsers, 4, MidpointRounding.AwayFromZero) * 100;

            rta.Add(percentil);

            var averageUser = averagesList.Sum(x => x) / totUsers;

            var underAverage = averagesList.Where(x => x < averageUser).Count();

            average = Decimal.Round((decimal)underAverage / (decimal)totUsers, 4, MidpointRounding.AwayFromZero) * 100;

            rta.Add(average);


            return rta;
        }

        public User SubscribeUser(int userID, int status)
        {
            var u = _context.Users.Where(x => x.UserID == userID).SingleOrDefault();
            if (u != null)
            {

                switch (status)
                {
                    case (int)UserStatus.CREATED:
                        break;
                    case (int)UserStatus.SUBSCRIBED:
                        u.Enable = true;
                        u.Status = status;
                        break;
                    case (int)UserStatus.EXPIRED:
                        break;
                    case (int)UserStatus.CANCELED:
                        u.Status = status;
                        break;
                }

                _context.SaveChanges();
            }

            return u;
        }
    }
}

