using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Cresimed.Core.Entities.Database;
using Cresimed.Core.Interfaces;
using Cresimed.Web.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Cresimed.Core.Entities.Enum;
using Cresimed.Core.Entities.ViewModel.Admin.Grid;
using Cresimed.Core.Entities.ViewModel.Web;
using Cresimed.Data.Repositories;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;

namespace Cresimed.Web.Controllers
{
    [Route("/Account")]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly ILogRepository _logRepository;


        public AccountController(IUserRepository userRepository, ILogRepository logRepository, IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logRepository = logRepository ?? throw new ArgumentNullException(nameof(logRepository));
            _userRoleRepository = userRoleRepository ?? throw new ArgumentNullException(nameof(userRoleRepository));
        }

        [Route("~/Account/Register")]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [Route("~/Account/Register")]
        public IActionResult Register(RegisterViewModel view)
        {

            User user = new User();
            user.Username = view.Username;
            user.Password = BCrypt.Net.BCrypt.HashPassword(view.Password);
            user.FullName = view.Name + " " + view.LastName;
            user.Enable = true;
            user.Email = view.Email;
            user.DateAdded = view.DateAdded;
            user.Deleted = false;
            user.UserAverage = 0;
            user.Country = view.Country ;
            user.University = view.University ;
            user.Province = view.Province ;
            user.LastYear = view.LastYear;

            _userRepository.InsertUser(user);


            UserRole userRole = new UserRole();

            userRole.UserID = user.UserID;
            userRole.RoleID = 4;
            userRole.Enable = true;

            _userRoleRepository.InsertOrUpdateUserRole(userRole);

            return Redirect("~/Account/NewPayment?userID="+user.UserID);
        }
        
        [Route("~/Account/NewPayment")]
        public async Task<IActionResult> NewPaymentAsync(int userID)
        {
            MercadoPagoConfig.AccessToken = "APP_USR-6633196756476060-020908-bf2a224f99421a5877384a052ea96d3a-202416641";
            // Crea el objeto de request de la preference
            var request = new PreferenceRequest
            {
                Items = new List<PreferenceItemRequest>
                {
                    new PreferenceItemRequest
                    {
                        Title = "Suscripción A Cresimed",
                        Quantity = 1,
                        CurrencyId = "ARS",
                        UnitPrice = 1.00m,
                    },
                },
                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = "https://www.tu-sitio/success",
                    Failure = "http://www.tu-sitio/failure",
                    Pending = "http://www.tu-sitio/pendings",
                },
                AutoReturn = "approved",

            };
            // Crea la preferencia usando el client
            var client = new PreferenceClient();
            Preference preference = await client.CreateAsync(request);
            var n = new RegisterViewModel();

            n.Preference = preference;

            return View(n);
        }
        [HttpPost]
        [Route("~/Account/NewPayment")]
        public IActionResult NewPayment(int userID)
        {

            
            
            
            
            
            
            
            
            
            
            
            
            

            


            

            
            
            

            

            return View();
        }


    }
}