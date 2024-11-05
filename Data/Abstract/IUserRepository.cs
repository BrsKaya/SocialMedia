using SocialMediaApp.Entity;

namespace SocialMediaApp.Data.Abstract{

    public interface IUserRepository{
        IQueryable<User> Users {get;}
        
        void CreateUser(User user);
    }
}