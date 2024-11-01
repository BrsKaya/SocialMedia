using SocialMediaApp.Entity;

namespace SocialMediaApp.Data.Abstract{

    public interface ICommentRepository{
        IQueryable<Comment> Comments {get;}
        
        void CreateComment(Comment comment);
    }
}