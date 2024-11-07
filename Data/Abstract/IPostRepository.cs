using SocialMediaApp.Entity;

namespace SocialMediaApp.Data.Abstract
{

    public interface IPostRepository
    {
        IQueryable<Post> Posts { get; }
        void CreatePost(Post post);
        void EditPost(Post post);
    }

}