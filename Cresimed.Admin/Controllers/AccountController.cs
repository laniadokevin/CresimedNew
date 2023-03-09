using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Cresimed.Admin.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Cresimed.Core.Interfaces;
using Cresimed.Core.Entities.Enum;
using Cresimed.Core.Entities.ViewModel.Campus;
using Cresimed.Core.Entities.ViewModel.Admin;

namespace Cresimed.Admin.Controllers
{
    [Route("Admin")]
    [Route("/Admin/Account")]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogRepository _logRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IContactRepository _contactRepository;

        private SecurityManager securityManager = new SecurityManager();

        public AccountController(IUserRepository userRepository, ILogRepository logRepository, ISubscriptionRepository subscriptionRepository, IContactRepository contactRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logRepository = logRepository ?? throw new ArgumentNullException(nameof(logRepository));
            _subscriptionRepository = subscriptionRepository ?? throw new ArgumentNullException(nameof(subscriptionRepository)); ;
            _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository)); ;
        }

        [Route("")]
        [Route("~/")]
        [Route("~/Admin/Account/index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username != null && password != null && username != "" && password != "")
            {
                var account = _userRepository.processLogin(username, password);
                if (account == null)
                {
                    ViewBag.error = "User Not Found";
                    return View("Index");
                }
                else
                {
                    securityManager.SignIn(this.HttpContext, account);

                    if (account.UserRoles.Any(x => (x.RoleID == (int)UserRoles.SUPERADMIN || x.RoleID == (int)UserRoles.ADMIN) && x.Enable))
                        return RedirectToAction("Welcome", "Account");
                    else if (account.UserRoles.Any(x => x.RoleID == (int)UserRoles.EMPLOYEE && x.Enable))
                        return RedirectToAction("Questions", "Grids");
                    else
                        return RedirectToAction("AccessDenied", "Account");
                }
            }
            else
            {
                ViewBag.error = "Complete the fields";
                return View("Index");
            }
        }

        [Route("/Admin/Account/Welcome")]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public IActionResult Welcome()
        {
            DashboardAdminViewModel view = new DashboardAdminViewModel();
          
            view = _subscriptionRepository.GetDashboardView();

            view.UsersSubscribedTable = _subscriptionRepository.GetLast10Subscriptions();
            view.LineBarUsers = _subscriptionRepository.GetDataUsersLast6Months();
            view.LineBarAmount = _subscriptionRepository.GetDataAmountLast6Months();
            view.Contacts = _contactRepository.GetLast5();

            

            return View("Welcome",view);
        }

        [Route("/Admin/Account/AccessDenied")]
        public IActionResult AccessDenied()
        {
            ViewBag.error = "Access Denied";

            return View();

        }


        [Route("/Admin/Account/SignOut")]
        public IActionResult SignOut()
        {
            securityManager.SignOut(this.HttpContext);
            return RedirectToAction("Index");
        }

    }
}