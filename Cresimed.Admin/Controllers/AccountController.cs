using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Cresimed.Admin.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Cresimed.Core.Interfaces;
using Cresimed.Core.Entities.Enum;

namespace Cresimed.Admin.Controllers
{
    [Route("Admin")]
    [Route("/Admin/Account")]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogRepository _logRepository;

        private SecurityManager securityManager = new SecurityManager();

        public AccountController(IUserRepository userRepository, ILogRepository logRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logRepository = logRepository ?? throw new ArgumentNullException(nameof(logRepository));
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
            var account = _userRepository.processLogin(username, password);
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || account == null)
            {
                ViewBag.error = "Invalid";
                return View("Index");
            }
            else
            {
                securityManager.SignIn(this.HttpContext, account);

                if (account.UserRoles.Any(x=> (x.RoleID == (int)UserRoles.SUPERADMIN || x.RoleID == (int)UserRoles.ADMIN) && x.Enable))
                    return RedirectToAction("Welcome", "Account");
                else if (account.UserRoles.Any(x=> x.RoleID == (int)UserRoles.EMPLOYEE && x.Enable))
                    return RedirectToAction("Questions", "Grids");
                else
                    return RedirectToAction("AccessDenied", "Account");
            }
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [Route("/Admin/Account/Welcome")]
        public IActionResult Welcome()
        {
            
            return View("Welcome");
        }

        [Route("/Admin/Account/access-denied")]
        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }


        [Route("/Admin/Account/SignOut")]
        public IActionResult SignOut()
        {
            securityManager.SignOut(this.HttpContext);
            return RedirectToAction("Index");
        }

    }
}