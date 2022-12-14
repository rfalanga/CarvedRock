using CarvedRock.Admin.Data;
using CarvedRock.Admin.Logic;
using CarvedRock.Admin.Repository;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using CarvedRock.Admin.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AdminContextConnection") ?? throw new InvalidOperationException("Connection string 'AdminContextConnection' not found.");
//Erik changed the value for AdminContextConnection in appsettings.json, so he reasoned that we could comment out the preceeding line.
//However, then he changed the AddDbContext<AdminContext> call, which is NOT in this scafforded code!! So, I'm leaving the 
//definition for connectionString alone.
//Now I'm wondering if this is another instance in which the scaffolded code is wrong?  12/17/2022


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();

builder.Services.AddDbContext<ProductDbContext>();

builder.Services.AddDbContext<AdminContext>(options => options.UseSqlite(connectionString));  // I mistakenly did not include this, Erik Dahl corrected this mistake
                                                                                              // At this point connectionString == cr-auth.db
builder.Services.AddDefaultIdentity<AdminUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;

    // code Erik entered from a snippet - I got it from the downloaded code
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;

});

builder.Services.AddDefaultIdentity<AdminUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AdminContext>();
builder.Services.AddScoped<ICarvedRockRepository, CarvedRockRepository>();
builder.Services.AddScoped<IProductLogic, ProductLogic>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var ctx = services.GetRequiredService<ProductDbContext>();
    ctx.Database.Migrate();

    //added lines for authentication database
    var userCtx = services.GetRequiredService<AdminContext>();
    userCtx.Database.Migrate();

    if (app.Environment.IsDevelopment())
    {
        ctx.SeedInitialData();
    }
}

// NOTE: Erik commended out the "if", "{" and "}". 

// Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
//}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();    //to help with partial views
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
