using Cresimed.Core.Entities.Database;
using Cresimed.Core.Entities.ViewModel.Admin.Grid;
using Cresimed.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cresimed.Admin.Controllers
{

    [Authorize]
    [Route("admin/grids")]
    public class GridsController : Controller
    {

        #region Properties

        private IAnswerRepository _answerRepository;
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

        #endregion

        #region Constructor
        public GridsController(
                IAnswerRepository answerRepository,
                ICareerRepository careerRepository,
                ICategoryRepository categoryRepository,
                ICountryRepository countryRepository,
                IContactRepository contactRepository,
                IExamRepository examRepository,
                IFaqRepository faqRepository,
                ILogRepository logRepository,
                IKeyWordRepository keyWordRepository,
                IQuestionRepository questionRepository,
                IRoleRepository roleRepository,
                IReportRepository reportRepository,
                ISpecialtyRepository specialtyRepository,
                IUserRepository userRepository
            )
        {
            _answerRepository = answerRepository ?? throw new ArgumentNullException(nameof(answerRepository));
            _careerRepository = careerRepository ?? throw new ArgumentNullException(nameof(careerRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
            _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
            _examRepository = examRepository ?? throw new ArgumentNullException(nameof(examRepository));
            _faqRepository = faqRepository ?? throw new ArgumentNullException(nameof(faqRepository));
            _logRepository = logRepository ?? throw new ArgumentNullException(nameof(logRepository));
            _keyWordRepository = keyWordRepository ?? throw new ArgumentNullException(nameof(keyWordRepository));
            _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _reportRepository = reportRepository ?? throw new ArgumentNullException(nameof(reportRepository));
            _specialtyRepository = specialtyRepository ?? throw new ArgumentNullException(nameof(specialtyRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        #endregion

        #region Careers

        [Authorize(Roles = "SuperAdmin")]
        [Authorize(Roles = "Admin")]
        [Route("Careers")]
        public IActionResult Careers(CareerGridViewModel view, int? pageNumber, string sortOrder, string currentFilter, string searchString)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentSort"] = sortOrder;
            ViewData["IDSortParm"] = sortOrder == "ID" ? "ID_desc" : "ID";
            ViewData["CareerSortParm"] = sortOrder == "Career" ? "Career_desc" : "Career";
            ViewData["CountriesSortParm"] = sortOrder == "Countries" ? "Countries_desc" : "Countries";
            ViewData["SpecialtiesSortParm"] = sortOrder == "Specialties" ? "Specialties_desc" : "Specialties";
            ViewData["QuestionsSortParm"] = sortOrder == "Questions" ? "Questions_desc" : "Questions"; ;
            ViewData["CurrentFilter"] = searchString;


            view.Careers = _careerRepository.GetAllFiltered(sortOrder, currentFilter, searchString, pageNumber ?? 1);
            view.Countries = _countryRepository.GetAll();

            ViewData["TotalCareers"] = _careerRepository.TotalCareersCount();
            ViewData["FilteredCareers"] = _careerRepository.TotalFilteredCount(searchString);
            ViewData["ShowingCareers"] = view.Careers.Count;


            return View("Careers", view);
        }

        #endregion

        #region Categories

        [Authorize(Roles = "SuperAdmin")]
        [Authorize(Roles = "Admin")]
        [Route("Categories")]
        public IActionResult Categories(CategoryGridViewModel view, int? pageNumber, string sortOrder, string currentFilter, string searchString)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentSort"] = sortOrder;
            ViewData["IDSortParm"] = sortOrder == "ID" ? "ID_desc" : "ID";
            ViewData["CategorySortParm"] = sortOrder == "Category" ? "Category_desc" : "Category";
            ViewData["QuestionsSortParm"] = sortOrder == "Questions" ? "Questions_desc" : "Questions"; ;
            ViewData["CurrentFilter"] = searchString;


            view.Categories = _categoryRepository.GetAllFiltered(sortOrder, currentFilter, searchString, pageNumber ?? 1);

            ViewData["TotalCategories"] = _categoryRepository.TotalCategoriesCount();
            ViewData["FilteredCategories"] = _categoryRepository.TotalFilteredCount(searchString);
            ViewData["ShowingCategories"] = view.Categories.Count;


            return View("Categories", view);
        }

        #endregion

        #region Countries

        [Authorize(Roles = "SuperAdmin")]
        [Authorize(Roles = "Admin")]
        [Route("Countries")]
        public IActionResult Countries(CountryGridViewModel view, int? pageNumber, string sortOrder, string currentFilter, string searchString)
        {

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentSort"] = sortOrder;
            ViewData["IDSortParm"] = sortOrder== "ID" ? "ID_desc" : "ID";
            ViewData["CountrySortParm"] = sortOrder== "Country" ? "Country_desc" : "Country";
            ViewData["CareersSortParm"] = sortOrder== "Careers" ? "Careers_desc" : "Careers";
            ViewData["SpecialtiesSortParm"] = sortOrder== "Specialties" ? "Specialties_desc" : "Specialties";
            ViewData["QuestionsSortParm"] = sortOrder == "Questions" ? "Questions_desc" : "Questions"; ;
            ViewData["CurrentFilter"] = searchString;

            view.Countries = _countryRepository.GetAllFiltered(sortOrder, currentFilter, searchString, pageNumber ?? 1);

            ViewData["TotalCountries"] = _countryRepository.TotalCountriesCount();
            ViewData["FilteredCountries"] = _countryRepository.TotalFilteredCount(searchString);
            ViewData["ShowingCountries"] = view.Countries.Count;

            return View("Countries", view);
        }

        #endregion

        #region Contacts

        [Authorize(Roles = "SuperAdmin")]
        [Authorize(Roles = "Admin")]
        [Route("Contacts")]
        public IActionResult Contacts(ContactGridViewModel view, int? pageNumber, string sortOrder, string currentFilter, string searchString)
        {

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentSort"] = sortOrder;
            ViewData["IDSortParm"] = sortOrder == "ID" ? "ID_desc" : "ID";
            ViewData["ContactSortParm"] = sortOrder == "Contact" ? "Contact_desc" : "Contact";
            ViewData["CurrentFilter"] = searchString;

            view.Contacts = _contactRepository.GetAllFiltered(sortOrder, currentFilter, searchString, pageNumber ?? 1);

            ViewData["TotalContacts"] = _contactRepository.TotalContactsCount();
            ViewData["FilteredContacts"] = _contactRepository.TotalFilteredCount(searchString);
            ViewData["ShowingContacts"] = view.Contacts.Count;

            return View("Contacts", view);
        }

        #endregion

        #region Faqs

        [Authorize(Roles = "SuperAdmin")]
        [Authorize(Roles = "Admin")]
        [Route("Faqs")]
        public IActionResult Faqs(FaqGridViewModel view, int? pageNumber, string sortOrder, string currentFilter, string searchString)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentSort"] = sortOrder;
            ViewData["IDSortParm"] = sortOrder == "ID" ? "ID_desc" : "ID";
            ViewData["SubjectSortParm"] = sortOrder == "Subject" ? "Subject_desc" : "Subject";
            ViewData["QuestionSortParm"] = sortOrder == "Question" ? "Question_desc" : "Question";
            ViewData["AnswerSortParm"] = sortOrder == "Answer" ? "Answer_desc" : "Answer";
            ViewData["CurrentFilter"] = searchString;

            view.Faqs = _faqRepository.GetAllFiltered(sortOrder, currentFilter, searchString, pageNumber ?? 1);


            ViewData["TotalFaqs"] = _faqRepository.TotalFaqsCount();
            ViewData["FilteredFaqs"] = _faqRepository.TotalFilteredCount(searchString);
            ViewData["ShowingFaqs"] = view.Faqs.Count;

            return View("Faqs", view);
        }

        #endregion

        #region Reports

        [Authorize(Roles = "SuperAdmin")]
        [Authorize(Roles = "Admin")]
        [Route("Reports")]
        public IActionResult Reports(ReportGridViewModel view, int? pageNumber, string sortOrder, string currentFilter, string searchString)
        {

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentSort"] = sortOrder;
            ViewData["IDSortParm"] = sortOrder == "ID" ? "ID_desc" : "ID";
            ViewData["ReportSortParm"] = sortOrder == "Report" ? "Report_desc" : "Report";
            ViewData["CurrentFilter"] = searchString;

            view.Reports = _reportRepository.GetAllFiltered(sortOrder, currentFilter, searchString, pageNumber ?? 1);

            ViewData["TotalReports"] = _reportRepository.TotalReportsCount();
            ViewData["FilteredReports"] = _reportRepository.TotalFilteredCount(searchString);
            ViewData["ShowingReports"] = view.Reports.Count;

            return View("Reports", view);
        }

        #endregion

        #region Specialties

        [Authorize(Roles = "SuperAdmin")]
        [Authorize(Roles = "Admin")]
        [Route("Specialties")]
        public IActionResult Specialties(SpecialtyGridViewModel view, int? pageNumber, string sortOrder, string currentFilter, string searchString)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentSort"] = sortOrder;
            ViewData["IDSortParm"] = sortOrder == "ID" ? "ID_desc" : "ID";
            ViewData["SpecialtySortParm"] = sortOrder == "Specialty" ? "Specialty_desc" : "Specialty";
            ViewData["CareersSortParm"] = sortOrder == "Careers" ? "Careers_desc" : "Careers";
            ViewData["CountriesSortParm"] = sortOrder == "Countries" ? "Countries_desc" : "Countries";
            ViewData["QuestionsSortParm"] = sortOrder == "Questions" ? "Questions_desc" : "Questions"; ;
            ViewData["CurrentFilter"] = searchString;

            view.Specialties = _specialtyRepository.GetAllFiltered(sortOrder, currentFilter, searchString, pageNumber ?? 1);
            view.Careers = _careerRepository.GetAll();
            view.Countries = _countryRepository.GetAll();

            ViewData["TotalSpecialties"] = _specialtyRepository.TotalSpecialtiesCount();
            ViewData["FilteredSpecialties"] = _specialtyRepository.TotalFilteredCount(searchString);
            ViewData["ShowingSpecialties"] = view.Specialties.Count;

            return View("Specialties", view);
        }

        #endregion

        #region Question

        [Authorize(Roles = "Employee")]
        [Authorize(Roles = "SuperAdmin")]
        [Authorize(Roles = "Admin")]
        [Route("Questions")]
        public IActionResult Question(QuestionGridViewModel view, int? pageNumber, string sortOrder, string currentFilter, string searchString, string SpecialtyFilter, string CategoryFilter)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentSort"] = sortOrder;
            ViewData["IDSortParm"] = sortOrder == "ID" ? "ID_desc" : "ID";
            ViewData["CategoriesSortParm"] = sortOrder == "Categories" ? "Categories_desc" : "Categories";
            ViewData["SpecialtiesSortParm"] = sortOrder == "Specialties" ? "Specialties_desc" : "Specialties";
            ViewData["CareersSortParm"] = sortOrder == "Careers" ? "Careers_desc" : "Careers";
            ViewData["CountriesSortParm"] = sortOrder == "Countries" ? "Countries_desc" : "Countries"; 
            ViewData["CurrentFilter"] = searchString ?? "";
            ViewData["CurrentCategoryFilter"] = CategoryFilter ?? "";
            ViewData["CurrentSpecialtyFilter"] = SpecialtyFilter ?? "";

            view.Questions = _questionRepository.GetAllFiltered(sortOrder, currentFilter, searchString, pageNumber ?? 1, int.Parse(CategoryFilter ?? "0"), int.Parse(SpecialtyFilter ?? "0"));
            view.Questions.ForEach(x => x.QuestionText = x.QuestionText.PadRight(100, ' ').Substring(0, 80));
            view.NewQuestion.CareerID = 1;
            view.NewQuestion.CountryID = 1;

            Career career = new Career();
            career.Name = "Pick a career";
            Category category = new Category();
            category.Name = "Pick a category";
            Country country = new Country();
            country.Name = "Pick a country";
            Specialty specialty = new Specialty();
            specialty.Name = "Pick a specialty";


            view.Careers = _careerRepository.GetAll();
            view.Categories = _categoryRepository.GetAll();
            view.Countries = _countryRepository.GetAll();
            view.Specialties = _specialtyRepository.GetAll();

            view.Careers.Insert(0,career);
            view.Categories.Insert(0,category);
            view.Countries.Insert(0,country);
            view.Specialties.Insert(0,specialty);

            
            ViewData["SpecialtiesList"] = _specialtyRepository.GetAll().ConvertAll(a => { return new SelectListItem() { Text = a.Name.ToString(), Value = a.SpecialtyID.ToString() }; }); 
            ViewData["TotalQuestions"] = _questionRepository.TotalQuestionsCount(); 
            ViewData["FilteredQuestions"] = _questionRepository.TotalFilteredCount(searchString, int.Parse(CategoryFilter ?? "0"), int.Parse(SpecialtyFilter ?? "0"));
            ViewData["ShowingQuestions"]  = view.Questions.Count;

            return View("Questions",view);
        }

        #endregion

        #region Users

        [Authorize(Roles = "SuperAdmin")]
        [Authorize(Roles = "Admin")]
        [Route("Users")]
        public IActionResult Users(UserGridViewModel view, int? pageNumber, string sortOrder, string currentFilter, string searchString, string RoleFilter)
        {

            if (searchString != null)
                pageNumber = 1;
            else
                searchString = currentFilter;

            ViewData["CurrentSort"] = sortOrder;
            ViewData["IDSortParm"] = sortOrder == "ID" ? "ID_desc" : "ID";
            ViewData["RolSortParm"] = sortOrder == "Rol" ? "Rol_desc" : "Rol";
            ViewData["UsernameSortParm"] = sortOrder == "Username" ? "Username_desc" : "Username";
            ViewData["FullnameSortParm"] = sortOrder == "Fullname" ? "Fullname_desc" : "Fullname";
            ViewData["EmailSortParm"] = sortOrder == "Email" ? "Email_desc" : "Email"; ;
            ViewData["EnabledSortParm"] = sortOrder == "Enabled" ? "Enabled_desc" : "Enabled"; ;
            ViewData["CurrentFilter"] = searchString;
            ViewData["RoleFilter"] = RoleFilter ?? "";


            Role role = new Role();
            role.Name = "Pick a role";

            view.Users = _userRepository.GetAllFiltered(sortOrder, currentFilter, searchString, pageNumber ?? 1, int.Parse(RoleFilter ?? "0"));
            view.Roles = _roleRepository.GetAll();
                
            view.Roles.Insert(0, role);

            ViewData["Roles"] = _roleRepository.GetAll().Select(r => new SelectListItem { Value = r.RoleID.ToString(), Text = r.Name }).ToList();

            ViewData["TotalUsers"] = _userRepository.TotalUsersCount();
            ViewData["FilteredUsers"] = _userRepository.TotalFilteredCount(searchString, int.Parse(RoleFilter?? "0"));
            ViewData["ShowingUsers"] = view.Users.Count;

            return View("Users",view);
        }

        #endregion



    }
}