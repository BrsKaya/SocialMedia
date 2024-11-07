using SocialMediaApp.Data.Abstract;
using SocialMediaApp.Data.EfCore;
using SocialMediaApp.Entity;

namespace SocialMediaApp.Data.Concreate{
    public class EfUserRepository : IUserRepository
    {
        private SocialMediaContext _context;
        public EfUserRepository(SocialMediaContext context){
            _context = context;
        }
        public IQueryable<User> Users => _context.Users;

        public void CreateUser(User User)
        {
            _context.Users.Add(User);
            _context.SaveChanges();
        }
    }
}