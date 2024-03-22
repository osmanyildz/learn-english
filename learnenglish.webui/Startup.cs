using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learnenglish.data.Abstract;
using learnenglish.data.Concrete.EfCore;
using learnenglish.webui.EmailServices;
using learnenglish.webui.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace learnenglish.webui
{
  public class Startup
{
     private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    // public Startup(IConfiguration configuration)
    // {
    //     Configuration = configuration;
    // }

    // public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationContext>(options=>options.UseSqlite("Data Source = learnenglishDB"));
        services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

        services.AddControllersWithViews();

        services.AddScoped<IQuizRepository,EfCoreQuizRepository>();
        services.AddScoped<IAnswerRepository,EfCoreAnswerRepository>();
        services.AddScoped<ILessonRepository,EfCoreLessonRepository>();
        services.AddScoped<ILevelRepository,EfCoreLevelRepository>();
        services.AddScoped<IEmailSender, SmtpEmailSender>(i=> 
        new SmtpEmailSender(
            _configuration["EmailSender:Host"],
            _configuration.GetValue<int>("EmailSender:Port"),
            _configuration.GetValue<bool>("EmailSender:EnableSSL"),
            _configuration["EmailSender:UserName"],
            _configuration["EmailSender:Password"]
            )
        );
        services.Configure<IdentityOptions>(options=>{
            options.User.RequireUniqueEmail=true;
            options.SignIn.RequireConfirmedEmail=true;
            options.SignIn.RequireConfirmedPhoneNumber=false;
        });
        services.ConfigureApplicationCookie(options=>{
            options.LoginPath="/Account/Login";
            options.LogoutPath="/Account/Logout";
            options.AccessDeniedPath="/Account/AccessDenied";
             options.SlidingExpiration=true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.Cookie = new CookieBuilder(){
                    HttpOnly=true,
                    Name=".LearnEnglish.Security.Cookie"
                };
        });

        services.AddRazorPages();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }
        app.UseAuthentication();
        // app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name:"home",
                pattern:"/",
                defaults: new {controller="Home", action="Index"}
            );
             endpoints.MapControllerRoute(
                name:"index",
                pattern:"/home/index",
                defaults: new {controller="Home", action="Index"}
            );
              endpoints.MapControllerRoute(
                name:"logout",
                pattern:"/Account/Logout",
                defaults: new {controller="Account", action="Logout"}
            );
            endpoints.MapControllerRoute(
                name:"login",
                pattern:"/account/login",
                defaults: new {controller="Account",action="Login"}
            );
              endpoints.MapControllerRoute(
                name:"ResetPassword",
                pattern:"/Account/ResetPassword",
                defaults: new {controller="Account",action="ResetPassword"}
            );
               endpoints.MapControllerRoute(
                name:"EditProfile",
                pattern:"/Account/EditProfile",
                defaults: new {controller="Account",action="EditProfile"}
            );
          endpoints.MapControllerRoute(
                name:"register",
                pattern:"/account/register",
                defaults: new {controller="Account",action="Register"}
            );
             endpoints.MapControllerRoute(
                name:"CreateContent",
                pattern:"/Admin/CreateContent",
                defaults: new {controller="Admin",action="CreateContent"}
            );
             endpoints.MapControllerRoute(
                name:"Statistics",
                pattern:"/Admin/Statistics",
                defaults: new {controller="Admin",action="Statistics"}
            );
             endpoints.MapControllerRoute(
                name:"Show",
                pattern:"/Admin/Showing",
                defaults: new {controller="Admin",action="Showing"}
            );
             endpoints.MapControllerRoute(
                name:"Profile",
                pattern:"/Account/Profile",
                defaults: new {controller="Account",action="Profile"}
            );
            endpoints.MapControllerRoute(
                name:"UploadImage",
                pattern:"/Admin/UploadImage",
                defaults: new {controller="Admin",action="UploadImage"}
            );
    
             endpoints.MapControllerRoute(
                name:"CreateQuiz",
                pattern:"/Admin/CreateQuiz",
                defaults: new {controller="Admin",action="CreateQuiz"}
            );
             endpoints.MapControllerRoute(
                name:"ShowQuiz",
                pattern:"/Quiz/ShowQuiz",
                defaults: new {controller="Quiz",action="ShowQuiz"}
            );
            endpoints.MapControllerRoute(
                name:"BeforeQuiz",
                pattern:"/Quiz/BeforeQuiz",
                defaults: new {controller="Quiz",action="BeforeQuiz"}
            );
            endpoints.MapControllerRoute(
                name:"LessonDelete",
                pattern:"/Admin/QuizList",
                defaults: new {controller="Admin",action="QuizList"}
            );
            endpoints.MapControllerRoute(
                name:"QuizDelete",
                pattern:"/Admin/QuizDelete/{id}",
                defaults: new {controller="Admin",action="QuizDelete"}
            );
           
              endpoints.MapControllerRoute(
                name:"LessonDelete",
                pattern:"/Admin/QuizEdit/{id?}",
                defaults: new {controller="Admin",action="QuizEdit"}
            );
            endpoints.MapControllerRoute(
                name:"EmailConfirm",
                pattern:"/Account/ConfirmEmail",
                defaults: new {controller="Account",action="ConfirmEmail"}
            );
             endpoints.MapControllerRoute(
                name:"LessonContent",
                pattern:"/Admin/LessonList",
                defaults: new {controller="Admin",action="LessonList"}
            );
             endpoints.MapControllerRoute(
                name:"AfterQuiz",
                pattern:"/Quiz/AfterQuiz",
                defaults: new {controller="Quiz",action="AfterQuiz"}
            );
              endpoints.MapControllerRoute(
                name:"LevelInformation",
                pattern:"/Lesson/LevelInformation",
                defaults: new {controller="Lesson",action="LevelInformation"}
            );
           endpoints.MapControllerRoute(
            name:"ForgotPassword",
            pattern:"/Account/ForgotPassword",
            defaults: new {controller="Account",action="ForgotPassword"}
           );
               endpoints.MapControllerRoute(
                name:"LessonEdit",
                pattern:"/Admin/LessonEdit/{id?}",
                defaults: new {controller="Admin",action="LessonEdit"}
            );
            
             endpoints.MapControllerRoute(
                name:"LessonContent",
                pattern:"/Lesson/LessonContent/{id?}",
                defaults: new {controller="Lesson",action="LessonContent"}
            );
             endpoints.MapControllerRoute(
                name:"LessonDelete",
                pattern:"/Admin/LessonDelete/{id}",
                defaults: new {controller="Admin",action="LessonDelete"}
            );
        });

    }
}
}