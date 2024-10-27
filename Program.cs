using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Data.Abstract;
using SocialMediaApp.Data.Concreate;
using SocialMediaApp.Data.Concreate.EfCore;
using SocialMediaApp.Data.EfCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); //Controller ve Views ilişkilendirilmesi

builder.Services.AddDbContext<SocialMediaContext>(options => {
    options.UseSqlite(builder.Configuration["ConnectionStrings:Sql_connection"]);
});

builder.Services.AddScoped<IPostRepository,EfPostRepository>();

var app = builder.Build();

app.UseStaticFiles();

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
