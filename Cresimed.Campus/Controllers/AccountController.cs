using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Cresimed.Core.Entities.Database;
using Cresimed.Core.Interfaces;
using System.Security.Claims;
using Cresimed.Campus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Cresimed.Core.Entities.ViewModel.Campus;
using Cresimed.Core.Entities.Enum;
using MercadoPago.Resource.User;
using System.Linq;

namespace Cresimed.Campus.Controllers
{
    [Route("/Account")]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogRepository _logRepository;
        private readonly IExamRepository _examRepository;
        private readonly IPercentilRepository _percentilRepository;

        private SecurityManager securityManager = new SecurityManager();

        public AccountController(IUserRepository userRepository,
            ILogRepository logRepository,
            IExamRepository examRepository,
            IPercentilRepository percentilRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logRepository = logRepository ?? throw new ArgumentNullException(nameof(logRepository));
            _examRepository = examRepository ?? throw new ArgumentNullException(nameof(examRepository));
            _percentilRepository = percentilRepository ?? throw new ArgumentNullException(nameof(percentilRepository));
        }

        [Route("")] 
        [Route("~/")]
        [Route("~/Account/index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username = "", string password = "")
        {
            var account = _userRepository.processLogin(username, password);

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || account == null)
            {
                ViewBag.error = "Usuario o contraseña incorrecta";
            }
            else
            {

                if (account.UserRoles.Any(x => x.Enable))
                {
                    var esAdmin = account.UserRoles.Where(x=> x.Enable == true && (x.RoleID == (int)UserRoles.SUPERADMIN || x.RoleID == (int)UserRoles.ADMIN)).Any();
                   
                    if (esAdmin)
                    {
                        securityManager.SignIn(this.HttpContext, account);
                        return RedirectToAction("Welcome", "Account");
                    }
                    else
                    {
                        switch (account.Status)
                    {
                        case (int)UserStatus.SUBSCRIBED:
                            securityManager.SignIn(this.HttpContext, account);
                            return RedirectToAction("Welcome", "Account");

                        case (int)UserStatus.CREATED:
                            ViewBag.error = "Usuario con error en el pago de la suscripción";
                            break;

                        case (int)UserStatus.CANCELED:
                            ViewBag.error = "Usuario con error en el pago de la suscripción";
                            break;

                        case (int)UserStatus.EXPIRED:
                            ViewBag.error = "Suscripción expirada para resuscribirse haga <a href='google.com' >click aqui</a>";
                            break;

                        default:
                            ViewBag.error = "Usuario desactivado contactarse con soporte";
                            break;
                    }
                    }
                }
            }
                return View("Index");

        }

        [Authorize(Roles = "SuperAdmin, Admin, Customer")]
        [Route("~/Account/welcome")]
        public IActionResult Welcome()
        {
            DashboardViewModel view = new DashboardViewModel();

            var userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            view.PieChart = _examRepository.GetAllBySpecialty(userID);
            view.Last5Exams = _examRepository.GetLast5Exams(userID);
            view.PercentilChart = _userRepository.GetPercentil(userID);
            
            _percentilRepository.GetPercentilsForUser(userID);

            
            return View("Welcome", view);
        }

        [Route("~/Account/ForgotPwd")]
        public IActionResult ForgotPwd()
        {

            return View("ForgotPwd");
        }

        [HttpPost]
        [Route("~/Account/ForgotPwdPost")]
        public IActionResult ForgotPwdPost(string email)
        {
            //Send Email
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin, Admin, Customer")]
        [Route("~/Account/ResetPwd")]
        public IActionResult ResetPwd(string password)
        {
            var pre = _userRepository.GetById(int.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault()));

            if (pre != null)
            {
                pre.Password = BCrypt.Net.BCrypt.HashPassword(password);
            
                var user = _userRepository.UpdateUser(pre);

                return RedirectToAction("Welcome");
            }
            else
                return View("AccessDenied");

        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin, Admin, Customer")]
        [Route("~/Account/ResetPwd")]
        public IActionResult ResetPwd()
        {

            return View("ResetPwd");
        }

        [Route("~/Account/accessdenied")]
        public IActionResult AccessDenied()
        {
            ViewBag.error = "Access Denied";
            return RedirectToAction("Index", "Account");

        }


        [Route("~/Account/SignOut")]
        public IActionResult SignOut()
        {
            securityManager.SignOut(this.HttpContext);
            return RedirectToAction("Index");
        }

    }
}