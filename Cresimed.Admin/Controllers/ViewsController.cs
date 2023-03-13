using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Cresimed.Admin.Models;
using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;
using System.Data;
using System.Text;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cresimed.Core.Entities.ViewModel.Admin.Edition;
using Cresimed.Core.Interfaces;

namespace Cresimed.Admin.Controllers
{
    [Route("admin/view")]
    public class ViewsController : Controller
    {

        #region Properties


        private IAnswerRepository _answerRepository;
        private IBufferedFileUploadService _bufferedFileUploadService;
        private ICareerRepository _careerRepository;
        private ICategoryRepository _categoryRepository;
        private ICountryRepository _countryRepository;
        private IContactRepository _contactRepository;
        private IExamRepository _examRepository;
        private IFaqRepository _faqRepository;
        private IKeyWordRepository _keyWordRepository;
        private ILogRepository _logRepository;
        private IQuestionRepository _questionRepository;
        private IReportRepository _reportRepository;
        private IRoleRepository _roleRepository;
        private ISpecialtyRepository _specialtyRepository;
        private IUserRepository _userRepository;
        private IUserRoleRepository _userRoleRepository;

        #endregion

        #region Constructor

        public ViewsController(
                IAnswerRepository answerRepository,
                IBufferedFileUploadService bufferedFileUploadService,
                ICareerRepository careerRepository,
                ICategoryRepository categoryRepository,
                ICountryRepository countryRepository,
                IContactRepository contactRepository,
                IExamRepository examRepository,
                IFaqRepository faqRepository,
                IKeyWordRepository keyWordRepository,
                ILogRepository logRepository,
                IQuestionRepository questionRepository,
                IReportRepository reportRepository,
                IRoleRepository roleRepository,
                ISpecialtyRepository specialtyRepository,
                IUserRepository userRepository,
                IUserRoleRepository userRoleRepository
            )
        {
            _answerRepository = answerRepository ?? throw new ArgumentNullException(nameof(answerRepository));
            _bufferedFileUploadService = bufferedFileUploadService ?? throw new ArgumentNullException(nameof(bufferedFileUploadService));
            _careerRepository = careerRepository ?? throw new ArgumentNullException(nameof(careerRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
            _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
            _examRepository = examRepository ?? throw new ArgumentNullException(nameof(examRepository));
            _faqRepository = faqRepository ?? throw new ArgumentNullException(nameof(faqRepository));
            _keyWordRepository = keyWordRepository ?? throw new ArgumentNullException(nameof(keyWordRepository));
            _logRepository = logRepository ?? throw new ArgumentNullException(nameof(logRepository));
            _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
            _reportRepository = reportRepository ?? throw new ArgumentNullException(nameof(reportRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _specialtyRepository = specialtyRepository ?? throw new ArgumentNullException(nameof(specialtyRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userRoleRepository = userRoleRepository ?? throw new ArgumentNullException(nameof(userRoleRepository));

        }

        #endregion

        #region Answers

        [Authorize(Roles = "Employee, SuperAdmin, Admin")]
        [Route("ViewAnswer")]
        [HttpGet]
        public IActionResult ViewAnswer(int id)
        {

            AnswerViewViewModel view = new AnswerViewViewModel();
            view.Answer = _answerRepository.GetById(id);
            view.Question = _questionRepository.GetById(view.Answer.QuestionID);


            return View("Answers", view);
        }

        #endregion

        #region Careers

       
        [Route("ViewCareer")]
        [HttpGet]
        public IActionResult ViewCareer(int id)
        {
            var career = _careerRepository.GetById(id);

            CareerViewViewModel view = new CareerViewViewModel();
            view.Career = career;
            view.Countries = _countryRepository.GetAll();


            return View("Careers", view);
        }

        #endregion

        #region Categories

     
        [Route("ViewCategory")]
        [HttpGet]
        public IActionResult ViewCategory(int id)
        {
            var Category = _categoryRepository.GetById(id);

            CategoryViewViewModel view = new CategoryViewViewModel();
            view.Category = Category;

            return View("Categories", view);
        }

        #endregion

        #region Countries

        [Route("ViewCountry")]
        [HttpGet]
        public IActionResult ViewCountry(int id)
        {
            var Country = _countryRepository.GetById(id);

            CountryViewViewModel view = new CountryViewViewModel();
            view.Country = Country;
            //view.Countries = _countryRepository.GetAll();


            return View("Countries", view);
        }

        #endregion

        #region Contacts

        [Route("ViewContact")]
        [HttpGet]
        public IActionResult ViewContact(int id)
        {
            var contact = _contactRepository.GetById(id);

            ContactViewViewModel view = new ContactViewViewModel();
            view.Contact = contact;


            return View("Contacts", view);
        }

        #endregion

        #region Faqs

        [Route("ViewFaq")]
        [HttpGet]
        public IActionResult ViewFaq(int id)
        {
            var Faq = _faqRepository.GetById(id);

            FaqViewViewModel view = new FaqViewViewModel();
            view.Faq = Faq;


            return View("Faqs", view);
        }

        #endregion

        #region KeyWord

        [Authorize(Roles = "Employee, SuperAdmin, Admin")]
        [Route("ViewKeyWord")]
        [HttpGet]
        public IActionResult ViewKeyWord(int id)
        {
            KeyWordViewViewModel view = new KeyWordViewViewModel();
            view.KeyWord = _keyWordRepository.GetById(id);
            view.Question = _questionRepository.GetById(view.KeyWord.QuestionID);

            return View("KeyWords", view);
        }

        #endregion KeyWord

        #region Questions & Answers

        [Authorize(Roles = "Employee, SuperAdmin, Admin")]
        [Route("Questions")]
        [HttpGet]
        public IActionResult Questions()
        {
            return View("Questions");
        }

        [Route("ImportQuestions")]
        [HttpGet]
        public IActionResult ImportQuestions()
        {
            return View("ImportQuestions");
        }

        [Authorize(Roles = "Employee, SuperAdmin, Admin")]
        [Route("ViewQuestion")]
        [HttpGet]
        public IActionResult ViewQuestion(int id)
        {
            var question = _questionRepository.GetById(id);

            QuestionViewViewModel view = new QuestionViewViewModel();
            view.Question = question;
            view.Answers = _answerRepository.GetAnswersForQuestion(id);
            view.Answers.ForEach(x => x.Text = x.Text.PadRight(100, ' ').Substring(0, 80) + "...");

            view.KeyWords = _keyWordRepository.GetKeyWordsForQuestion(id);
            view.Careers = _careerRepository.GetAll();
            view.Categories = _categoryRepository.GetAll();
            view.Specialties = _specialtyRepository.GetAll();
            view.Countries = _countryRepository.GetAll();


            return View("Questions", view);
        }

        #endregion  

        #region Reports

        [Route("ViewReport")]
        [HttpGet]
        public IActionResult ViewReport(int id)
        {
            var report = _reportRepository.GetById(id);

            ReportViewViewModel view = new ReportViewViewModel();
            view.Report = report;


            return View("Reports", view);
        }


        #endregion

        #region Specialties

        [Route("ViewSpecialty")]
        [HttpGet]
        public IActionResult ViewSpecialty(int id)
        {
            var specialty = _specialtyRepository.GetById(id);

            SpecialtyViewViewModel view = new SpecialtyViewViewModel();
            view.Specialty = specialty;
            view.Careers = _careerRepository.GetAll();
            view.Countries = _countryRepository.GetAll();


            return View("Specialties", view);
        }

        #endregion

        #region Users

        [Route("ViewUser")]
        [HttpGet]
        public IActionResult ViewUser(int id)
        {
            var user = _userRepository.GetById(id);
            var roles = _roleRepository.GetAll();

            UserViewViewModel view = new UserViewViewModel();
            view.User = user;
            view.Roles = roles;


            return View("Users", view);
        }

        #endregion

        #region  NonActions

      

        #endregion

    }
}