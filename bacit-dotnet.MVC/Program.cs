
using bacit_dotnet.MVC.DataAccess;
using bacit_dotnet.MVC.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;


public class Program
{

    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddTransient<SqlConnector>();

        builder.Services.AddSingleton<IUserRepository, SqlUserRepository>();
        builder.Services.AddSingleton<ISuggestionRepository, SqlSuggestionRepository>();
        builder.Services.AddSingleton<ITeamRepository, SqlTeamRepository>();
        builder.Services.AddSingleton<ICategoryRepository, SqlCategoryRepository>();

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/login";
                options.AccessDeniedPath = "/denied";
                
            });
        var app = builder.Build();
         
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        { 
            app.UseExceptionHandler("/Home/Error"); 
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        //app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapControllers();   


        app.Run();
    }
}