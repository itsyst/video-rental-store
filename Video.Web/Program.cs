using Video.Application;
using Video.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Video.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

//Services configuration
builder.Services.AddPersistanceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddDefaultIdentity<IdentityUser>(/*options => options.SignIn.RequireConfirmedAccount = true*/)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Invoke function to seed database
PersistanceContainer.SeedDatabase(app.Services.CreateAsyncScope());

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();