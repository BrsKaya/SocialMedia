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
                        new User {UserName = "BarisKaya", Image = "pp.png"},
                        new User {UserName = "Deneme", Image = "pp.png"}
                    );
                    context.SaveChanges();
                }
                if(!context.Posts.Any()){
                    context.Posts.AddRange(
                        new Post {
                            Content = "Yeni sosyal medyamızın ilk gönderisi!!!",
                            PublishedOn = DateTime.Now.AddDays(-10),
                            UserId = 1
                        }
                    );
                    context.SaveChanges();
                }
            }

        }
    }
}