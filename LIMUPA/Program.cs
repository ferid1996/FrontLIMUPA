using LIMUPA.Models;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string connectionstring = "server=DESKTOP-E4M97SJ;database=LimupaDb;Trusted_Connection=true;MultipleActiveResultSets=true";
builder.Services.AddDbContext<DataContext>(op=>op.UseSqlServer(connectionstring));
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(X =>
//{
//    X.LoginPath = "register/login";   
//    X.LogoutPath = "register/logout";
//    X.LoginPath = "Admin/user/login";
//    X.LogoutPath = "Admin/user/logout";
//}); 

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

app.UseRouting();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=User}/{action=Login}/{id?}"
    );

    endpoints.MapControllerRoute(
       name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );


});


     
 

app.Run();
