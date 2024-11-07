using SocialMediaApp.Entity;

namespace SocialMediaApp.Data.Abstract
{

    public interface IPostRepository
    {
        IQueryable<Post> Posts { get; }

        void CreatePost(Post post);
<<<<<<< HEAD
        void EditPost(Post post);
=======

         void EditPost(Post post);
        void EditPost(Post post, int[] PostId);

>>>>>>> 2655c73d3ab076940c29c620feca9caf2bbf2684
    }
}