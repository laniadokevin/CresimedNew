using Cresimed.Core.Interfaces;
using Cresimed.Data;
using Cresimed.Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<CresimedDBContext>(options => {
    //options.UseLazyLoadingProxies();
    options.UseSqlServer(builder.Configuration.GetConnectionString("CresimedDBContext"));
    options.ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning));

});

builder.Services.AddScoped<IAnswerRepository, AnswerRepository>(); 
builder.Services.AddTransient<IBufferedFileUploadService, BufferedFileUploadLocalService>();
builder.Services.AddScoped<ICareerRepository, CareerRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IExamRepository, ExamRepository>();
builder.Services.AddScoped<IFaqRepository, FaqRepository>();
builder.Services.AddScoped<IKeyWordRepository, KeyWordRepository>();
builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
 

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options =>
              {
                  options.LoginPath = "/Admin/Account/Index";
                  options.LogoutPath = "/Admin/Account/SignOut";
                  options.AccessDeniedPath = "/Admin/Account/AccessDenied";
              });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection(); 
app.UseStaticFiles(new StaticFileOptions
{
    //RequestPath = "/wwwroot"
});
app.UseAuthentication();

app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "admin/{controller=Home}/{action=Index}/{id?}");

app.Run();
