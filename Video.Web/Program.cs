using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Video.Application;
using Video.Domain.Entities;
using Video.Domain.Utilities;
using Video.Infrastructure;
using Video.Infrastructure.Persistence;
 

var builder = WebApplication.CreateBuilder(args);

//Services configuration
builder.Services.AddPersistanceServices(builder.Configuration);
builder.Services.AddApplicationServices();

// Add services to the container. 
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

// Identity Services - Authentication and authorization

builder.Services.AddScoped<ApplicationUser>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(/*options => options.SignIn.RequireConfirmedAccount = true*/)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager<SignInManager<ApplicationUser>>()
    .AddDefaultTokenProviders()
    .AddClaimsPrincipalFactory<CustomUserClaimsPrincipalFactory>();

builder.Services.AddAuthentication()
    .AddFacebook(facebookOptions =>
    {
        facebookOptions.AppId = builder.Configuration.GetSection("FacebookSettings:AppId").Get<string>();
        facebookOptions.AppSecret = builder.Configuration.GetSection("FacebookSettings:AppSecret").Get<string>();
    });

// Email Services
builder.Services.AddScoped<IEmailSender, EmailSender>();
 
 
// Application Cookies
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";

});

// Session cache
builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(100);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});

 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    app.Use(async (context, next) =>
    {
        await next();
        if (context.Response.StatusCode == 404)
        {
            context.Request.Path = "/Home";
            await next();
        }
    });
}
else
{
    app.UseDeveloperExceptionPage();
}

//Invoke function to seed database
PersistanceContainer.SeedDatabase(app.Services.CreateAsyncScope());

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.UseSession();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();