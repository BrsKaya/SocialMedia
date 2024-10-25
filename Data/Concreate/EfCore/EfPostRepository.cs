using SocialMediaApp.Data.Abstract;
using SocialMediaApp.Data.EfCore;
using SocialMediaApp.Entity;

namespace SocialMediaApp.Data.Concreate{
    public class EfPostRepository : IPostRepository
    {
        private SocialMediaContext _context;
        public EfPostRepository(SocialMediaContext context){
            _context = context;
        }
        public IQueryable<Post> Posts => _context.Posts;

        public void CreatePost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }
    }
}