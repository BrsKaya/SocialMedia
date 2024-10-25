using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Entity;

namespace SocialMediaApp.Data.EfCore{
    public class SocialMediaContext : DbContext{
        public SocialMediaContext(DbContextOptions<SocialMediaContext> options):base(options){}

        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<User> Users => Set<User>();
    }
}