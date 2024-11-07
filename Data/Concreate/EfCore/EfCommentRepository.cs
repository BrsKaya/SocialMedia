using SocialMediaApp.Data.Abstract;
using SocialMediaApp.Data.Concreate.EfCore;
using SocialMediaApp.Data.EfCore;
using SocialMediaApp.Entity;

namespace SocialMediaApp.Data.Concreate{
    public class EfCommentRepository : ICommentRepository
    {
        private SocialMediaContext _context;
        public EfCommentRepository(SocialMediaContext context){
            _context = context;
        }
        public IQueryable<Comment> Comments => _context.Comments;

        public void CreateComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
    }
}