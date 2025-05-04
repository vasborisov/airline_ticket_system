using Airline_Ticket_System.Configurations;
using AirlineTicketSystem.Data;
using AirlineTicketSystem.Data.Entities;
using AirlineTicketSystem.Repositories;
using AirlineTicketSystem.Services.Interfaces;
using AirlineTicketSystem.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

public class Program
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {

        // Configure the DbContext with SQL Server and the connection string
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Account/Login";
            options.LogoutPath = "/Account/Logout";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            options.SlidingExpiration = true;
        });

        builder.Services.AddControllersWithViews();

        builder.Services.Configure<AdminUserSettings>(builder.Configuration.GetSection("AdminUser"));

        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IFlightService, FlightService>();
        builder.Services.AddScoped<IPassengerService, PassengerService>();
    }

    public static void ConfigureApp(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        DatabaseSeedData.Initialize(app);

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Flight}/{action=Index}/{id?}");

    }

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder);

        var app = builder.Build();
        ConfigureApp(app);
        app.Run();
        
    }
}