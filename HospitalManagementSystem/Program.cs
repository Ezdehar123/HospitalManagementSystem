using Hospital.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Hospital.Utilities;
using Hospital.Repositories.Implementation;
using Hospital.Repositories.Interfaces;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.AspNetCore.Identity.UI.Services;
using Hospital.Models;
using Hospital.Services;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//builder.Services.AddDbContext<ApplicatopnDbContext>(options => {
//    options.UseSqlServer("server=localhost\\SQLEXPRESS;database=hospitaldb;trusted_connection=true;");
//});


builder.Services.AddDbContext<ApplicatopnDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<ApplicatopnDbContext>();

builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddTransient<UnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddTransient<IHospitalInfo, HospitalInfoService>();

builder.Services.AddRazorPages();


//options => options.SignIn.RequireConfirmedAccount = true

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
DataSedding();
app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();
app.MapRazorPages();    
app.MapControllerRoute(
    name: "default",
    pattern: "{Area=admin}/{controller=Hospitals}/{action=Index}/{id?}");

app.Run();

void DataSedding()
{

    using (var scope = app.Services.CreateScope())

    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}

//void DataSeeding()
//{ using (var scope = app.Services.CreateScope())
//    {
//        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
//        dbInitializer.Initialize();
//    }
//}

