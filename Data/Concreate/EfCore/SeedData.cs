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
                            Url = "111111",
                            Comment = new List<Comment> {
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
