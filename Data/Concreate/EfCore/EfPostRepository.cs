using SocialMediaApp.Data.Abstract;
using SocialMediaApp.Data.Concreate.EfCore;
using SocialMediaApp.Entity;
using SocialMediaApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Data.EfCore;
using System.Text.RegularExpressions;


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
                entity.Content = post.Content;
                entity.Url = post.Url;
                entity.IsActive = post.IsActive;          

                _context.SaveChanges();
            }
        }

         public async Task<Dictionary<string, int>> GetMostCommentedHashtagsAsync(int count)
    {
        var posts = await _context.Posts
            .Include(p => p.Comments) // Yorum ilişkisini dahil ediyoruz
            .Where(p => p.Content.Contains("#")) // İçeriğinde hashtag olan gönderiler
            .ToListAsync();

        // Hashtagleri çıkar ve yorum sayısına göre grupla
        var hashtagsWithCounts = posts
            .SelectMany(post =>
                Regex.Matches(post.Content, @"#\w+") // Regex ile hashtagleri bul
                    .Select(match => new { Hashtag = match.Value, CommentCount = post.Comments.Count }))
            .GroupBy(x => x.Hashtag)
            .Select(group => new { Hashtag = group.Key, TotalComments = group.Sum(x => x.CommentCount) })
            .OrderByDescending(x => x.TotalComments)
            .Take(count) // İlk 'count' kadar hashtag
            .ToDictionary(x => x.Hashtag, x => x.TotalComments);

        return hashtagsWithCounts;
    }
    }
}