using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cresimed.Core.Entities.ViewModel.Admin.Edition;
using Cresimed.Core.Interfaces;
using Cresimed.Core.Entities.Database;
using Cresimed.Core.Entities.ViewModel.Admin.Grid;
using ExcelDataReader;

namespace TestYourself.Admin.Controllers
{
    [Route("admin/edition")]
    [Authorize(Roles = "Employee, SuperAdmin, Admin")]
    public class EditionController : Controller
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

        public EditionController(
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
        
        [Route("UpdateAnswer")]
        [HttpPost]
        public IActionResult UpdateAnswer(AnswerViewViewModel view)
        {
            _answerRepository.UpdateAnswer(view.Answer);
            return RedirectToAction("ViewQuestion", "Views", new { id = view.Answer.QuestionID.ToString() });
        }

        [Route("DeleteAnswer")]
        [HttpGet]
        public IActionResult DeleteAnswer(int id)
        {
            int q = _answerRepository.DeleteAnswer(id);
            return RedirectToAction("ViewQuestion", "Views", new { id = q.ToString() });
        }

        #endregion

        #region Careers

        [Route("NewCareer")]
        [HttpPost]
        public IActionResult NewCareer(CareerGridViewModel view)
        {
            var e = _careerRepository.GetOrSave(view.NewCareer);
            return RedirectToAction("Careers", "Grids");
        }

        [Route("UpdateCareer")]
        [HttpPost]
        public IActionResult UpdateCareer(CareerViewViewModel view)
        {
            _careerRepository.UpdateCareer(view.Career);
            return RedirectToAction("Careers", "Grids");
        }

        [Route("DeleteCareer")]
        [HttpGet]
        public IActionResult DeleteCareer(int id)
        {
            _careerRepository.DeleteCareer(id);
            return RedirectToAction("Careers", "Grids");
        }

        #endregion

        #region Categories

        [Route("NewCategory")]
        [HttpPost]
        public IActionResult NewCategory(CategoryGridViewModel view)
        {
            var e = _categoryRepository.GetOrSave(view.NewCategory);
            return RedirectToAction("Categories", "Grids");
        }

        [Route("UpdateCategory")]
        [HttpPost]
        public IActionResult UpdateCategory(CategoryViewViewModel view)
        {
            _categoryRepository.UpdateCategory(view.Category);
            return RedirectToAction("Categories", "Grids");
        }

        [Route("DeleteCategory")]
        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {
            _categoryRepository.DeleteCategory(id);
            return RedirectToAction("Categories", "Grids");
        }

        #endregion

        #region Countries

        [Route("NewCountry")]
        [HttpPost]
        public IActionResult NewCountry(CountryGridViewModel view)
        {
            var e = _countryRepository.GetOrSave(view.NewCountry);
            return RedirectToAction("Countries", "Grids");
        }

        [Route("UpdateCountry")]
        [HttpPost]
        public IActionResult UpdateCountry(CountryViewViewModel view)
        {
            _countryRepository.UpdateCountry(view.Country);
            return RedirectToAction("Countries", "Grids");
        }

        [Route("DeleteCountry")]
        [HttpGet]
        public IActionResult DeleteCountry(int id)
        {
            _countryRepository.DeleteCountry(id);
            return RedirectToAction("Countries", "Grids");
        }

        #endregion

        #region Contacts

        [Route("DeleteContact")]
        [HttpGet]
        public IActionResult DeleteContact(int id)
        {
            _contactRepository.DeleteContact(id);
            return RedirectToAction("Contacts", "Grids");
        }

        #endregion

        #region Faqs

        [Route("NewFaq")]
        [HttpPost]
        public IActionResult NewFaq(FaqGridViewModel view)
        {
            var e = _faqRepository.InsertFaq(view.NewFaq);
            return RedirectToAction("Faqs", "Grids");
        }

        [Route("UpdateFaq")]
        [HttpPost]
        public IActionResult UpdateFaq(FaqViewViewModel view)
        {
            _faqRepository.UpdateFaq(view.Faq);
            return RedirectToAction("Faqs", "Grids");
        }

        [Route("DeleteFaq")]
        [HttpGet]
        public IActionResult DeleteFaq(int id)
        {
            _faqRepository.DeleteFaq(id);
            return RedirectToAction("Faqs", "Grids");
        }

        #endregion

        #region KeyWord

        [Route("KeyWord")]
        [HttpPost]
        public IActionResult UpdateKeyWord(KeyWordViewViewModel view)
        {

            int q = _keyWordRepository.UpdateKeyWord(view.KeyWord);
            return RedirectToAction("ViewQuestion", "Views", new { id = q.ToString() });

        }

        [Route("DeleteKeyWord")]
        [HttpGet]
        public IActionResult DeleteKeyWord(int id)
        {
            int q = _keyWordRepository.DeleteKeyWord(id);
            return RedirectToAction("UpdateQuestionView", "Edition", new { id = q.ToString() });

        }

        #endregion KeyWord

        #region Questions & Answers

        [Route("Questions")]
        [HttpGet]
        public IActionResult Questions()
        {

            return View("Questions");
        }

        [Route("NewQuestion")]
        [HttpPost]
        public IActionResult NewQuestion(QuestionGridViewModel view)
        {
            try
            {
                if ((view.NewSpecialties.Count == 1))
                {
                    view.NewQuestion.SpecialtyID = int.Parse(view.NewSpecialties.FirstOrDefault());
                    var date = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss_fffffff");
                    var question = new Question();

                    if ((view.File != null) && _bufferedFileUploadService.UploadFile(view.File, date))
                        view.NewQuestion.QuestionImage = view.File.FileName.Replace(System.IO.Path.GetExtension(view.File.FileName), "") + date + System.IO.Path.GetExtension(view.File.FileName);
                    
                    date = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss_fffffff");

                    if ((view.AnswerFile != null) && _bufferedFileUploadService.UploadFile(view.AnswerFile, date))
                        view.NewQuestion.ExplanationImage = view.AnswerFile.FileName.Replace(System.IO.Path.GetExtension(view.AnswerFile.FileName), "") + date + System.IO.Path.GetExtension(view.AnswerFile.FileName);


                    question = _questionRepository.InsertQuestion(view.NewQuestion);

                    for (int i = 0; i < view.NewKeyWords.Count; i++)
                    {
                        KeyWord aux = new KeyWord();

                        aux.QuestionID = question.QuestionID;
                        aux.Text = view.NewKeyWords[i].Text;

                        _keyWordRepository.GetOrSave(aux);

                    }

                    for (int i = 0; i < view.NewAnswers.Count; i++)
                    {
                        Answer aux = new Answer();

                        aux.IsCorrect = view.NewAnswers[i].IsCorrect;
                        aux.QuestionID = question.QuestionID;
                        aux.Text = view.NewAnswers[i].Text;

                        _answerRepository.InsertAnswer(aux);

                    }

                }
                else
                {

                    for (var i = 0; i < view.NewSpecialties.Count; i++)
                    {
                        view.NewQuestion.SpecialtyID = int.Parse(view.NewSpecialties[i]);
                        view.NewQuestion.QuestionID = 0;
                        var date = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss_fffffff");
                        var question = new Question();
                        
                        question.QuestionID = view.NewQuestion.QuestionID;
                        question.QuestionText = view.NewQuestion.QuestionText;
                        question.CountryID = view.NewQuestion.CountryID;
                        question.CareerID = view.NewQuestion.CareerID;
                        question.SpecialtyID = view.NewQuestion.SpecialtyID;
                        question.UniqueIdentity = view.NewQuestion.UniqueIdentity;
                        question.Explanation = view.NewQuestion.Explanation;
                        question.QuestionImage = view.NewQuestion.QuestionImage;
                        question.ExplanationImage = view.NewQuestion.ExplanationImage;

                        if ((view.File != null) && _bufferedFileUploadService.UploadFile(view.File, date))
                            view.NewQuestion.QuestionImage = view.File.FileName.Replace(System.IO.Path.GetExtension(view.File.FileName), "") + date + System.IO.Path.GetExtension(view.File.FileName);
                        
                        date = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss_fffffff");

                        if ((view.AnswerFile != null) && _bufferedFileUploadService.UploadFile(view.AnswerFile, date))
                            view.NewQuestion.ExplanationImage = view.AnswerFile.FileName.Replace(System.IO.Path.GetExtension(view.AnswerFile.FileName), "") + date + System.IO.Path.GetExtension(view.AnswerFile.FileName);

                        question = _questionRepository.InsertQuestion(question);

                        for (int ind = 0; ind < view.NewKeyWords.Count; ind++)
                        {

                            view.NewKeyWords[ind].KeyWordID = 0;
                            view.NewKeyWords[ind].QuestionID = question.QuestionID;
                            view.NewKeyWords[ind].Text = view.NewKeyWords[ind].Text;

                            _keyWordRepository.GetOrSave(view.NewKeyWords[ind]);

                        }

                        for (int ind = 0; ind < view.NewAnswers.Count; ind++)
                        {
                            Answer aux = new Answer();

                            aux.AnswerID = 0;

                            aux.IsCorrect = view.NewAnswers[ind].IsCorrect;
                            aux.QuestionID = question.QuestionID;
                            aux.Text = view.NewAnswers[ind].Text;

                            _answerRepository.InsertAnswer(aux);

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                //Log ex


            }
            return RedirectToAction("Questions", "Grids");
        }

        [Route("ImportQuestions")]
        [HttpGet]
        public IActionResult ImportQuestions()
        {
            return View("ImportQuestions");
        }

        [Route("UpdateQuestionView")]
        [HttpGet]
        public IActionResult UpdateQuestionView(int id)
        {
            var question = _questionRepository.GetById(id);

            QuestionViewViewModel view = new QuestionViewViewModel();
            view.Question = question;
            view.Answers = _answerRepository.GetAnswersForQuestion(id);
            view.KeyWords = _keyWordRepository.GetKeyWordsForQuestion(id);
            view.Careers = _careerRepository.GetAll();
            view.Categories = _categoryRepository.GetAll();
            view.Specialties = _specialtyRepository.GetAll();
            view.Countries = _countryRepository.GetAll();


            return View("Questions", view);
        }

        [Route("UpdateQuestion")]
        [HttpPost]
        public IActionResult UpdateQuestion(QuestionViewViewModel view)
        {
            try
            {
                var date = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss_fffffff");

                if ((view.File != null) && _bufferedFileUploadService.UploadFile(view.File, date))
                    view.Question.QuestionImage = view.File.FileName.Replace(System.IO.Path.GetExtension(view.File.FileName), "") + date + System.IO.Path.GetExtension(view.File.FileName);

                if ((view.AnswerFile != null) && _bufferedFileUploadService.UploadFile(view.AnswerFile, date))
                    view.Question.ExplanationImage = view.AnswerFile.FileName.Replace(System.IO.Path.GetExtension(view.AnswerFile.FileName), "") + date + System.IO.Path.GetExtension(view.AnswerFile.FileName);

                _questionRepository.UpdateQuestion(view.Question);

            }
            catch (Exception ex)
            {
                //Log ex
            }
            foreach (var answer in view.NewAnswers)
            {
                answer.QuestionID = view.Question.QuestionID;

                _answerRepository.InsertAnswer(answer);
            }

            foreach (var kw in view.NewKeyWords)
            {
                KeyWord aux = new KeyWord();

                aux.QuestionID = view.Question.QuestionID;
                aux.Text = kw.Text;

                _keyWordRepository.GetOrSave(aux);

            }

            return RedirectToAction("Questions", "Grids");
        }

        [Route("DeleteQuestion")]
        [HttpGet]
        public IActionResult DeleteQuestion(int id)
        {
            _questionRepository.DeleteQuestion(id);
            return RedirectToAction("Questions", "Grids");
        }

        #endregion  

        #region Reports

        [Route("DeleteReport")]
        [HttpGet]
        public IActionResult DeleteReport(int id)
        {
            _reportRepository.DeleteReport(id);
            return RedirectToAction("Reports", "Grids");
        }

        #endregion

        #region Specialties

        [Route("NewSpecialty")]
        [HttpPost]
        public IActionResult NewSpecialty(SpecialtyGridViewModel view)
        {
            var e = _specialtyRepository.GetOrSave(view.NewSpecialty);
            return RedirectToAction("Specialties", "Grids");
        }

        [Route("UpdateSpecialty")]
        [HttpGet]
        public IActionResult UpdateSpecialty(int id)
        {
            var specialty = _specialtyRepository.GetById(id);

            SpecialtyViewViewModel view = new SpecialtyViewViewModel();
            view.Specialty = specialty;
            view.Careers = _careerRepository.GetAll();
            view.Countries = _countryRepository.GetAll();


            return View("Specialties", view);
        }

        [Route("UpdateSpecialty")]
        [HttpPost]
        public IActionResult UpdateSpecialty(SpecialtyViewViewModel view)
        {
            _specialtyRepository.UpdateSpecialty(view.Specialty);
            return RedirectToAction("Specialties", "Grids");
        }

        [Route("DeleteSpecialty")]
        [HttpGet]
        public IActionResult DeleteSpecialty(int id)
        {
            _specialtyRepository.DeleteSpecialty(id);
            return RedirectToAction("Specialties", "Grids");
        }

        #endregion

        #region Users
        [Route("CheckUsernameDisponibility")]
        [HttpGet]
        public bool CheckUsernameDisponibility(string name)
        {
            return _userRepository.CheckUserNameDisponibility(name);

        }

        [Route("CheckEmailDisponibility")]
        [HttpGet]
        public bool CheckEmailDisponibility(string email)
        {
            return _userRepository.CheckEmailDisponibility(email);

        }

        [Route("NewUser")]
        [HttpPost]
        public IActionResult NewUser(UserGridViewModel view)
        {
            view.NewUser.Password = BCrypt.Net.BCrypt.HashPassword(view.NewUser.Password);
            var user = _userRepository.InsertUser(view.NewUser);
            foreach (var e in view.NewRoles)
            {
                UserRole userRole = new UserRole();

                userRole.UserID = user.UserID;
                userRole.RoleID = int.Parse(e);
                userRole.Enable = true;

                _userRoleRepository.InsertOrUpdateUserRole(userRole);
            }
            return RedirectToAction("Users", "Grids");
        }

        [HttpPost]
        [Route("EnableOrDisableUser")]
        public IActionResult EnableOrDisableUser(int id)
        {
            var u = _userRepository.EnableOrDisable(id);
            return Ok(u.Enable.ToString());
        }

        [Route("UpdateUser")]
        [HttpGet]
        public IActionResult Users(int id)
        {
            var user = _userRepository.GetById(id);
            var roles = _roleRepository.GetAll();

            UserViewViewModel view = new UserViewViewModel();
            view.User = user;
            view.Roles = roles;


            return View("Users", view);
        }

        [Route("UpdateUser")]
        [HttpPost]
        public IActionResult UpdateUser(UserViewViewModel view)
        {
            var pre = _userRepository.GetById(view.User.UserID);

            if (pre.Password != (view.User.Password))
                view.User.Password = BCrypt.Net.BCrypt.HashPassword(view.User.Password);
            else
                view.User.Password = null;

            var user = _userRepository.UpdateUser(view.User);

            //old
            foreach (var e in user.UserRoles)
            {

                UserRole userRole = new UserRole();

                userRole.UserID = user.UserID;
                userRole.RoleID = e.RoleID;
                userRole.Enable = false;

                _userRoleRepository.InsertOrUpdateUserRole(userRole);
            }

            //new
            foreach (var e in view.NewRoles)
            {

                UserRole userRole = new UserRole();

                userRole.UserID = user.UserID;
                userRole.RoleID = int.Parse(e);
                userRole.Enable = true;

                _userRoleRepository.InsertOrUpdateUserRole(userRole);
            }

            return RedirectToAction("Users", "Grids");
        }

        [Route("DeleteUser")]
        [HttpGet]
        public IActionResult DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
            return RedirectToAction("Users", "Grids");
        }


        #endregion

        #region  NonActions

        [Route("ImportQuestions")]
        [HttpPost]
        public IActionResult ImportQuestions(IFormFile file)
        {

            string pattern = @"[##____##]+";
            Regex rgx = new Regex(pattern);
            try
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var stream = System.IO.File.Open(file.FileName, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {

                        while (reader.Read()) //Each row of the file
                        {

                            if (reader.GetValue(0).ToString() != "Pregunta" && reader.GetValue(1).ToString() != "Opcion correcta" && reader.GetValue(2).ToString() != "Incorrecta")
                            {

                                string[] fields;

                                var questionsID = new List<int>();

                                var countryName = "Argentina";

                                Country country = new Country();
                                country.Name = countryName;

                                int countryID = _countryRepository.GetOrSave(country).CountryID;


                                var careerName = "Medicina";
                                Career career = new Career();
                                career.Name = careerName;
                                career.CountryID = countryID;
                                int careerID = _careerRepository.GetOrSave(career).CareerID;

                                var categoryName = reader.GetValue(6) != null ? reader.GetValue(6).ToString() : ""; //fields[6];
                                Category category = new Category();
                                category.Name = categoryName;
                                int categoryID = _categoryRepository.GetOrSave(category).CategoryID;


                                var specialties = reader.GetValue(5) != null ? reader.GetValue(5).ToString() : ""; // fields[5];

                                string[] specialtiesSplited = rgx.Split(specialties);

                                foreach (string s in specialtiesSplited)
                                {
                                    if (!s.Equals(""))
                                    {
                                        Specialty specialty = new Specialty();

                                        specialty.Name = s;
                                        specialty.CareerID = careerID;
                                        specialty.CountryID = countryID;

                                        int specialtyID = _specialtyRepository.GetOrSave(specialty).SpecialtyID;

                                        var questionText = reader.GetValue(0) != null ? reader.GetValue(0).ToString() : ""; // fields[0];
                                        var questionImage = reader.GetValue(7) != null ? reader.GetValue(7).ToString() : ""; // fields[7];
                                        var explanationImage = reader.GetValue(8) != null ? reader.GetValue(8).ToString() : ""; // fields[8];
                                        var explanation = reader.GetValue(4) != null ? reader.GetValue(4).ToString() : ""; // fields[4];

                                        Question question = new Question();
                                        question.QuestionText = questionText;
                                        question.CountryID = countryID;
                                        question.CareerID = careerID;
                                        question.SpecialtyID = specialtyID;
                                        question.CategoryID = categoryID;
                                        question.Explanation = explanation;
                                        question.QuestionImage = questionImage;
                                        question.ExplanationImage = explanationImage;

                                        int questionID = _questionRepository.InsertQuestion(question).QuestionID;

                                        questionsID.Add(questionID);

                                    }

                                }

                                foreach (var qID in questionsID)
                                {
                                    var correctAnswer = reader.GetValue(1) != null ? reader.GetValue(1).ToString() : ""; // fields[1];
                                    var incorrectAnswers = reader.GetValue(2) != null ? reader.GetValue(2).ToString() : ""; // fields[2];

                                    Answer answer = new Answer();
                                    answer.Text = correctAnswer;
                                    answer.QuestionID = qID;
                                    answer.IsCorrect = true;

                                    _answerRepository.InsertAnswer(answer);

                                    string[] incorrectAnswersSplited = rgx.Split(incorrectAnswers);

                                    foreach (string ia in incorrectAnswersSplited)
                                    {

                                        if (!ia.Equals(""))
                                        {
                                            Answer incorrectAnswer = new Answer();

                                            incorrectAnswer.Text = ia;
                                            incorrectAnswer.QuestionID = qID;
                                            incorrectAnswer.IsCorrect = false;
                                            incorrectAnswer.Ratio = 0;

                                            _answerRepository.InsertAnswer(incorrectAnswer);
                                        }
                                    }

                                    var keyWords = reader.GetValue(3) != null ? reader.GetValue(3).ToString() : ""; // fields[3];
                                    string[] keyWordsSplited = rgx.Split(keyWords);

                                    foreach (string kw in keyWordsSplited)
                                    {
                                        if (!kw.Equals(""))
                                        {
                                            KeyWord keyWord = new KeyWord();

                                            keyWord.Text = kw;
                                            keyWord.QuestionID = qID;

                                            _keyWordRepository.GetOrSave(keyWord);


                                        }

                                    }

                                }
                            }

                        }
                    }
                }

                /*
                               using (var fileStream = file.OpenReadStream())
                               using (var reader = new StreamReader(fileStream))
                               {
                                   string row;
                                   while ((row = reader.ReadLine()) != null)
                                   {
                                       if (row != "Pregunta,Opcion correcta,Incorrecta,Palabras clave,Explicacion,Especialidad,Caso clinico / Fundamento,Imagen pregunta,Imagen respuesta")
                                       {

                                           TextFieldParser parser = new TextFieldParser(new StringReader(row));

                                           parser.HasFieldsEnclosedInQuotes = true;
                                           parser.SetDelimiters(",");

                                           string[] fields;

                                           while (!parser.EndOfData)
                                           {
                                               var questionsID = new List<int>();

                                               fields = parser.ReadFields();
                                               var countryName = "Argentina";

                                               Country country = new Country();
                                               country.Name = countryName;

                                               int countryID = _countryRepository.GetOrSave(country).CountryID;


                                               var careerName = "Medicina";
                                               Career career = new Career();
                                               career.Name = careerName;
                                               career.CountryID = countryID;
                                               int careerID = _careerRepository.GetOrSave(career).CareerID;

                                               var categoryName = fields[6];
                                               Category category = new Category();
                                               category.Name = categoryName;
                                               int categoryID = _categoryRepository.GetOrSave(category).CategoryID;


                                               var specialties = fields[5];

                                               string[] specialtiesSplited = rgx.Split(specialties);

                                               foreach (string s in specialtiesSplited)
                                               {
                                                   if (!s.Equals(""))
                                                   {
                                                       Specialty specialty = new Specialty();

                                                       specialty.Name = s;
                                                       specialty.CareerID = careerID;
                                                       specialty.CountryID = countryID;

                                                       int specialtyID = _specialtyRepository.GetOrSave(specialty).SpecialtyID;

                                                       var questionText = fields[0];
                                                       var questionImage = fields[7];
                                                       var explanationImage = fields[8];
                                                       var explanation = fields[4];

                                                       Question question = new Question();
                                                       question.QuestionText = questionText;
                                                       question.CountryID = countryID;
                                                       question.CareerID = careerID;
                                                       question.SpecialtyID = specialtyID;
                                                       question.CategoryID = categoryID;
                                                       question.Explanation = explanation;
                                                       question.QuestionImage = questionImage;
                                                       question.ExplanationImage = explanationImage;

                                                       int questionID = _questionRepository.InsertQuestion(question).QuestionID;

                                                       questionsID.Add(questionID);

                                                   }

                                               }

                                               foreach (var qID in questionsID)
                                               {
                                                   var correctAnswer = fields[1];
                                                   var incorrectAnswers = fields[2];

                                                   Answer answer = new Answer();
                                                   answer.Text = correctAnswer;
                                                   answer.QuestionID = qID;
                                                   answer.isCorrect = true;

                                                   _answerRepository.InsertAnswer(answer);

                                                   string[] incorrectAnswersSplited = rgx.Split(incorrectAnswers);

                                                   foreach (string ia in incorrectAnswersSplited)
                                                   {

                                                       if (!ia.Equals(""))
                                                       {
                                                           Answer incorrectAnswer = new Answer();

                                                           incorrectAnswer.Text = ia;
                                                           incorrectAnswer.QuestionID = qID;
                                                           incorrectAnswer.isCorrect = false;

                                                           _answerRepository.InsertAnswer(incorrectAnswer);
                                                       }
                                                   }

                                                   var keyWords = fields[3];
                                                   string[] keyWordsSplited = rgx.Split(keyWords);

                                                   foreach (string kw in keyWordsSplited)
                                                   {
                                                       if (!kw.Equals(""))
                                                       {
                                                           KeyWord keyWord = new KeyWord();

                                                           keyWord.Text = kw;
                                                           keyWord.QuestionID = qID;

                                                           _keyWordRepository.GetOrSave(keyWord);


                                                       }

                                                   }

                                               }




                                           }
                                           parser.Close();
                                       }

                                   }
                               }
                           */

            }
            catch (Exception ex)
            {
                var remoteIpAddress = HttpContext.Connection.RemoteIpAddress;

                Log log = new Log();
                log.lvl = (int)LogLevel.Critical;
                log.FileName = "EditionController";
                log.Method = "ÍmportQuestions";
                log.ShortMessage = ex.Message;
                log.FullMessage = ex.StackTrace + "\n" + ex.InnerException;
                log.IpAddress = remoteIpAddress.ToString();
                log.Date = DateTime.Now;

                _logRepository.SaveLog(log);
            }


            return RedirectToAction("Questions", "Grids");
        }

        #endregion

    }
}