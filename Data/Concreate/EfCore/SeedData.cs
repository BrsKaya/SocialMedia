/*using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaApp.Data.EfCore;
using SocialMediaApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaApp.Data.Concreate.EfCore {
    public static class SeedData {
        public static async Task TestVerileriniDoldur(IApplicationBuilder app) {
            using (var scope = app.ApplicationServices.CreateScope()) {
                var context = scope.ServiceProvider.GetRequiredService<SocialMediaContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Veritabanında bekleyen migrasyonları uygulama
                if (context.Database.GetPendingMigrations().Any()) {
                    context.Database.Migrate();
                }

                // Rollerin eklenmesi
                if (!await roleManager.RoleExistsAsync("Admin")) {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }
                if (!await roleManager.RoleExistsAsync("User")) {
                    await roleManager.CreateAsync(new IdentityRole("User"));
                }

                // Kullanıcıların eklenmesi
                if (!context.Users.Any()) {
                    var user1 = new User { UserName = "BarisKaya", Email = "baris@example.com", Image = "pp.png" };
                    var user2 = new User { UserName = "Umut", Email = "umut@example.com", Image = "pp.png" };
                    var user3 = new User { UserName = "Sena", Email = "sena@example.com", Image = "pp.png" };
                    var user4 = new User { UserName = "Mehmet", Email = "mehmet@example.com", Image = "pp.png" };

                    await userManager.CreateAsync(user1, "Password123!");
                    await userManager.CreateAsync(user2, "Password123!");
                    await userManager.CreateAsync(user3, "Password123!");
                    await userManager.CreateAsync(user4, "Password123!");

                    await userManager.AddToRoleAsync(user1, "Admin");
                    await userManager.AddToRoleAsync(user2, "User");
                    await userManager.AddToRoleAsync(user3, "User");
                    await userManager.AddToRoleAsync(user4, "User");
                }
            
                // Gönderi eklenmesi
                if (!context.Posts.Any()) {
                    context.Posts.Add(new Post {
                        Content = "Yeni sosyal medyamızın ilk gönderisi!!!",
                        PublishedOn = DateTime.Now.AddDays(-10),
                        UserId = Convert.ToInt32(context.Users.First().Id),
                        Url = "comments",
                        Comment = new List<Comment> {
                            new Comment { Text = "Harika!!", PublishedOn = DateTime.Now, UserId = Convert.ToInt32(context.Users.Skip(1).First().Id) },
                            new Comment { Text = "Muhteşem!!", PublishedOn = DateTime.Now, UserId = Convert.ToInt32(context.Users.Skip(2).First().Id) },
                            new Comment { Text = "Başarılar!!", PublishedOn = DateTime.Now, UserId = Convert.ToInt32(context.Users.Skip(3).First().Id) }
                        }
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}*/




//güncelleme öncesi
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Data.EfCore;
using SocialMediaApp.Entity;

namespace SocialMediaApp.Data.Concreate.EfCore {
    public static class SeedData {
        public static void TestVerileriniDoldur(IApplicationBuilder app) {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<SocialMediaContext>();

            if (context != null) {
                if (context.Database.GetPendingMigrations().Any()) {
                    context.Database.Migrate();
                }
                if (!context.Users.Any()) {
                    context.Users.AddRange(
                        new User { UserName = "BarisKaya", Image = "pp.png", UserId = 1 },
                        new User { UserName = "Umut", Image = "pp.png", UserId = 2 },
                        new User { UserName = "Sena", Image = "pp.png", UserId = 3 },
                        new User { UserName = "Mehmet", Image = "pp.png", UserId = 4 }
                    );
                    context.SaveChanges();
                }
                if (!context.Posts.Any()) {
                    context.Posts.AddRange(
                        new Post {
                            Content = "Yeni sosyal medyamızın ilk gönderisi!!!",
                            PublishedOn = DateTime.Now.AddDays(-10),
                            UserId = 1,

                            Url = "comments",
                            Comments = new List<Comment> {

                                new Comment { Text = "Harika!!", PublishedOn = DateTime.Now, UserId = 2 },
                                new Comment { Text = "Muhteşem!!", PublishedOn = DateTime.Now, UserId = 3 },
                                new Comment { Text = "Başarılar!!", PublishedOn = DateTime.Now, UserId = 4 }
                            }
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
