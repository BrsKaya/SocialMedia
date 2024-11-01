using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Data.Abstract;
using SocialMediaApp.Data.EfCore;
using SocialMediaApp.Entity;
using SocialMediaApp.Models;

namespace SocialMediaApp.Controllers
{
    public class PostsController : Controller
    {
        private IPostRepository _postRepository;
        private ICommentRepository _commentRepository;
        public PostsController(IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }
        public async Task<IActionResult> Index()
        {
            var posts = _postRepository.Posts;
            return View(
                new PostsViewModel
                {
                    Posts = await posts.ToListAsync()
                }
            );
        }

        public async Task<IActionResult> Details(string url)
        {
            var post = await _postRepository.Posts.Include(x => x.Comment).ThenInclude(x => x.User).FirstOrDefaultAsync(p => p.Url == url);

            if (post == null)
            {
                return NotFound();
            }

            post.Comment = post.Comment.OrderByDescending(c => c.PublishedOn).ToList();

            return View(post);
        }




        [HttpPost]
        [Route("posts/AddCommentPage")]
        public JsonResult AddCommentPage(int PostId, string UserName, string Text)
        {
            var entity = new Comment
            {
                Text = Text,
                PublishedOn = DateTime.Now,
                PostId = PostId,
                User = new User { UserName = UserName, Image = "pp.png" }
            };
            
            _commentRepository.CreateComment(entity);
            
            return Json(new
            {
                username = entity.User.UserName,      
                text = entity.Text,                    
                publishedOn = entity.PublishedOn,      
                avatar = entity.User.Image             
            });
        }


    }
}