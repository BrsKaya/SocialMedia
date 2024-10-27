using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Data.EfCore;
using SocialMediaApp.Entity;

namespace SocialMediaApp.Data.Concreate.EfCore{
    public static class SeedData{
        public static void TestVerileriniDoldur(IApplicationBuilder app){  
            var context =  app.ApplicationServices.CreateScope().ServiceProvider.GetService<SocialMediaContext>(); // SocialMedia içerisindeki tabloları kontrol etme 

            if(context != null){ //İçerik boş değilse
                if(context.Database.GetPendingMigrations().Any()){ //Veritabanına gidip verileri süzme
                    context.Database.Migrate(); // Database içerisindeki verileri kullan
                }
                if(!context.Users.Any()){    //Users verisi yoksa 
                    context.Users.AddRange(   //tanımlanacak verileri ekle
                        new User {UserName = "BarisKaya", Image = "pp.png",UserId = 1},
                        new User {UserName = "Umut", Image = "pp.png",UserId = 2},
                        new User {UserName = "Sena", Image = "pp.png",UserId = 3},
                        new User {UserName = "Mehmet", Image = "pp.png",UserId = 4}
                    );
                    context.SaveChanges();
                }
                if(!context.Posts.Any()){
                    context.Posts.AddRange(
                        new Post {
                            Content = "Yeni sosyal medyamızın ilk gönderisi!!!",
                            PublishedOn = DateTime.Now.AddDays(-10),
                            UserId = 1,
                            Url = "comments",
                            Comment = new List<Comment>{
                                new Comment {Text = "Harika!!", PublishedOn = new DateTime(), UserId = 2},
                                new Comment {Text = "Muhteşem!!", PublishedOn = new DateTime(), UserId = 3},
                                new Comment {Text = "Başarılar!!", PublishedOn = new DateTime(), UserId = 4}
                            }
                        }
                    );
                    context.SaveChanges();
                }
            }

        }
    }
}