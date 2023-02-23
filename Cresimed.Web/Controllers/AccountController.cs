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
using MercadoPago.Resource.User;
using User = Cresimed.Core.Entities.Database.User;

namespace Cresimed.Web.Controllers
{
    [Route("/Account")]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly ILogRepository _logRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private bool Kevin = false;
        private string accessToken = "APP_USR-7796369158563021-021615-aeef5bbd00a850b927b8a9f96f7413fe-1305687415";
        private decimal precio = 1.00m;

        public AccountController(IUserRepository userRepository, ILogRepository logRepository, IUserRoleRepository userRoleRepository, ISubscriptionRepository subscriptionRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logRepository = logRepository ?? throw new ArgumentNullException(nameof(logRepository));
            _userRoleRepository = userRoleRepository ?? throw new ArgumentNullException(nameof(userRoleRepository));
            _subscriptionRepository = subscriptionRepository ?? throw new ArgumentNullException(nameof(subscriptionRepository));
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
            user.Enable = false;
            user.Email = view.Email;
            user.DateAdded = view.DateAdded;
            user.Deleted = false;
            user.UserAverage = 0;
            user.Country = view.Country;
            user.University = view.University;
            user.Province = view.Province;
            user.LastYear = view.LastYear;
            user.Status = (int)UserStatus.CREATED;

            _userRepository.InsertUser(user);

            UserRole userRole = new UserRole();

            userRole.UserID = user.UserID;
            userRole.RoleID = 4;
            userRole.Enable = true;

            _userRoleRepository.InsertOrUpdateUserRole(userRole);

            return Redirect("~/Account/NewPayment?userID=" + user.UserID);
        }

        [Route("~/Account/NewPayment")]
        public async Task<IActionResult> NewPaymentAsync(int userID)
        {
            var n = new RegisterViewModel();

            var preference = CreatePreferenceAsync(userID,precio);

            n.Preference = await preference;
            n.PaymentRsp = "";

            Subscription s = new Subscription();

            s.UserID = userID;
            s.DateCreated = DateTime.Now;
            s.Value = precio;
            s.Status = (int)SubscriptionStatus.Created;
            s.MPPaymentID = n.Preference.Id;

            _subscriptionRepository.InsertSubscription(s);

            return View(n);
        }

        [HttpGet]
        [Route("~/Account/NewPaymentRsp")]
        public async Task<IActionResult> NewPaymentRspAsync(string collection_id, string collection_status, string payment_id, string status, string external_reference, string payment_type, string merchant_order_id, string preference_id, string processing_mode, string merchant_account_id)
        {
            var n = new RegisterViewModel();
            string rta = "";
            var ss = _subscriptionRepository.UpdateSubscription(int.Parse(external_reference), preference_id, status);

            if (ss == null)
            {
                rta += "Error en el pago puede intentar pagar nuevamente con el siguiente boton o comuniquese con nosotros y lo ayudaremos a resolverlo";

                var preference = CreatePreferenceAsync(int.Parse(external_reference), precio);

                n.Preference = await preference;

                Subscription s = new Subscription();

                s.UserID = int.Parse(external_reference);
                s.DateCreated = DateTime.Now;
                s.Value = precio;
                s.Status = (int)SubscriptionStatus.Created;
                s.MPPaymentID = n.Preference.Id;

                _subscriptionRepository.InsertSubscription(s);
            }
            else
            {
                switch (ss.Status)
                {
                    case (int)SubscriptionStatus.Approved:
                        
                        _userRepository.SubscribeUser(int.Parse(external_reference), (int)UserStatus.SUBSCRIBED);

                        rta += "Su pago ha sido aceptado con exito ya puede comenzar a practicar";

                        break;

                    case (int)SubscriptionStatus.Nulled:

                        _userRepository.SubscribeUser(int.Parse(external_reference), (int)UserStatus.CANCELED);
                        
                        rta += "Error en el pago puede intentar pagar nuevamente con el siguiente boton ";

                        var preference = CreatePreferenceAsync(int.Parse(external_reference), precio);

                        n.Preference = await preference;

                        Subscription s = new Subscription();

                        s.UserID = int.Parse(external_reference);
                        s.DateCreated = DateTime.Now;
                        s.Value = precio;
                        s.Status = (int)SubscriptionStatus.Created;
                        s.MPPaymentID = n.Preference.Id;

                        _subscriptionRepository.InsertSubscription(s);
                        
                        break;

                    case (int)SubscriptionStatus.Created:

                        rta += "Error en el pago puede intentar pagar nuevamente con el siguiente boton ";
                        break;

                    case (int)SubscriptionStatus.Deny:

                        _userRepository.SubscribeUser(int.Parse(external_reference), (int)UserStatus.CANCELED);

                        rta += "Su Pago ha sido rechazado";

                        var pref = CreatePreferenceAsync(int.Parse(external_reference), precio);

                        n.Preference = await pref;

                        Subscription sub = new Subscription();

                        sub.UserID = int.Parse(external_reference);
                        sub.DateCreated = DateTime.Now;
                        sub.Value = precio;
                        sub.Status = (int)SubscriptionStatus.Created;
                        sub.MPPaymentID = n.Preference.Id;

                        _subscriptionRepository.InsertSubscription(sub);
                
                        break;



                }
            }

            n.PaymentRsp = rta;


            return View(n);
        }

        [NonAction]
        public async Task<Preference> CreatePreferenceAsync(int userID, decimal precio) {

            MercadoPagoConfig.AccessToken = accessToken;

            string dev = "https://localhost:7062/Account/NewPaymentRsp?";
            string prod = "https://cresimed.com/Account/NewPaymentRsp?";

            var paymentMethods = new PreferencePaymentMethodsRequest
            {
                ExcludedPaymentTypes = new List<PreferencePaymentTypeRequest> { new PreferencePaymentTypeRequest { Id = "ticket", }, },
            };

            var items = new List<PreferenceItemRequest> {
                new PreferenceItemRequest {
                    Title = "Suscripción A Cresimed",
                    Quantity = 1,
                    CurrencyId = "ARS",
                    UnitPrice = precio,

                },
            };


            var backUrls = new PreferenceBackUrlsRequest
            {
                Success = Kevin ? dev : prod,
                Failure = Kevin ? dev : prod,
                Pending = Kevin ? dev : prod,
            };

            // Crea el objeto de request de la preference
            var request = new PreferenceRequest
            {
                PaymentMethods = paymentMethods,
                Items = items,
                BackUrls = backUrls,
                AutoReturn = "approved",
                BinaryMode = true,
                ExternalReference = userID.ToString(),
                Expires = true,
                ExpirationDateTo = DateTime.Now.AddHours(1),

            };

            // Crea la preferencia usando el client
            var client = new PreferenceClient();

            Preference preference = await client.CreateAsync(request);

            return preference;
          

        }


    }
}