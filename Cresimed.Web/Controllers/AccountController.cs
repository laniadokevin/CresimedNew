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

namespace Cresimed.Web.Controllers
{
    [Route("/Account")]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogRepository _logRepository;


        public AccountController(IUserRepository userRepository, ILogRepository logRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logRepository = logRepository ?? throw new ArgumentNullException(nameof(logRepository));
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
            return View();
        }


    }
}