using SocialMediaApp.Data.Abstract;
using SocialMediaApp.Data.Concreate;
using SocialMediaApp.Data.Concreate.EfCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Data.EfCore;
using SocialMediaApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SocialMediaContext>(options =>{
    options.UseSqlite(builder.Configuration["ConnectionStrings:Sql_connection"]);
});

builder.Services.AddScoped<IPostRepository,EfPostRepository>();
builder.Services.AddScoped<ICommentRepository,EfCommentRepository>();
builder.Services.AddScoped<IUserRepository,EfUserRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options=>{
    options.LoginPath ="/Users/Login";
});


var app = builder.Build();

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


SeedData.TestVerileriniDoldur(app);

app.MapControllerRoute( 
    name: "default",
    pattern: "{controller= Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "post_details",
    pattern: "posts/{url}",
    defaults: new {controller = "Posts", action = "Details"}
);

app.MapControllerRoute(
    name: "user_profile",
    pattern: "profile/{username}",
    defaults: new {controller = "Users", action = "Profile"}
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Posts}/{action=Index}/{id?}"
);
app.Run();



////
/*using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Data.Abstract;
using SocialMediaApp.Data.Concreate;
using SocialMediaApp.Data.EfCore;

var builder = WebApplication.CreateBuilder(args);

// DbContext eklenmesi ve bağlantı ayarları
builder.Services.AddDbContext<SocialMediaContext>(options => {
    options.UseSqlite(builder.Configuration.GetConnectionString("Sql_connection"));
});


// Cookie ayarlarının yapılandırılması
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login"; // Giriş sayfası
    options.AccessDeniedPath = "/Account/AccessDenied"; // Yetkisiz erişim durumunda yönlendirme
    options.ExpireTimeSpan = TimeSpan.FromDays(14); // Cookie süresi
});

// MVC ve Razor Pages için servislerin eklenmesi
builder.Services.AddControllersWithViews();

// Repository bağımlılıklarının eklenmesi (Dependency Injection)
builder.Services.AddScoped<IPostRepository, EfPostRepository>();
builder.Services.AddScoped<ICommentRepository, EfCommentRepository>();
builder.Services.AddScoped<IUserRepository, EfUserRepository>();

// Kimlik doğrulama servisi eklenmesi
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Kimlik doğrulama
app.UseAuthorization(); // Yetkilendirme


// Route ayarları
app.MapControllerRoute(
    name: "post_details",
    pattern: "posts/{url}",
    defaults: new { controller = "Posts", action = "Details" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Posts}/{action=Index}/{id?}"
);

app.Run();*/



/////
//// güncelleme öncesi için notlar
///
//

/*using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Data.Abstract;
using SocialMediaApp.Data.Concreate;
using SocialMediaApp.Data.Concreate.EfCore;
using SocialMediaApp.Data.EfCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); //Controller ve Views ilişkilendirilmesi

builder.Services.AddIdentity<User, IdentityRole>();
    builder.Services.AddEntityFrameworkStores<SocialMediaContext>();
    builder.Services.AddDefaultTokenProviders();


builder.Services.AddDbContext<SocialMediaContext>(options => {
    options.UseSqlite(builder.Configuration["ConnectionStrings:Sql_connection"]);
});

builder.Services.AddScoped<IPostRepository,EfPostRepository>();
builder.Services.AddScoped<ICommentRepository,EfCommentRepository>();
builder.Services.AddScoped<IUserRepository,EfUserRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

SeedData.TestVerileriniDoldur(app);

app.MapControllerRoute(
    name: "post_details",
    pattern: "posts/{url}",
    defaults: new {controller = "Posts", action = "Details"}
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Posts}/{action=Index}/{id?}"
);

app.Run();
*/ 
