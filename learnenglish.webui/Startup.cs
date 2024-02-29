using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learnenglish.webui
{
  public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
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
                name:"login",
                pattern:"/account/login",
                defaults: new {controller="Account",action="Login"}
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
                name:"CreateContent",
                pattern:"/Admin/Showing",
                defaults: new {controller="Admin",action="Showing"}
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
        });

    }
}
}