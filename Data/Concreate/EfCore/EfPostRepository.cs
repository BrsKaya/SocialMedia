using SocialMediaApp.Data.Abstract;
using SocialMediaApp.Data.Concreate.EfCore;
using SocialMediaApp.Entity;
using SocialMediaApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Data.EfCore;

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
        public void EditPost(Post post){
            var entity = _context.Posts.FirstOrDefault(i=>i.PostId == post.PostId);

            if(entity != null){
                entity.Title = post.Title;
                entity.Description = post.Description;
                entity.Content = post.Content;
                entity.Url = post.Url;
                entity.IsActive = post.IsActive;

                _context.SaveChanges();
            }
        }

        public void EditPost(Post post, int[] PostId)
        {
             var entity = _context.Posts.FirstOrDefault(i=>i.PostId == post.PostId);

            if(entity != null){
                entity.Title = post.Title;
                entity.Description = post.Description;
                entity.Content = post.Content;
                entity.Url = post.Url;
                entity.IsActive = post.IsActive;

                

                _context.SaveChanges();
            }
        }
    }
}