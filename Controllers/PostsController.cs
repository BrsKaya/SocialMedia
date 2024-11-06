using System.Security.Claims;
using SocialMediaApp.Data.Abstract;
using SocialMediaApp.Data.Concreate.EfCore;
using SocialMediaApp.Entity;
using SocialMediaApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SocialMediaApp.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;

        public PostsController(IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }

        public async Task<IActionResult> Index()
        {
            var posts = _postRepository.Posts.Where(i => i.IsActive);
            return View(new PostsViewModel { Posts = await posts.ToListAsync() });
        }

        public async Task<IActionResult> Details(string url)
        {
            var post = await _postRepository.Posts
                .Include(x => x.User)
                .Include(x => x.Comments).ThenInclude(x => x.User)
                .FirstOrDefaultAsync(p => p.Url == url);

            if (post == null)
            {
                return NotFound();
            }

            post.Comments = post.Comments.OrderByDescending(c => c.PublishedOn).ToList();

            return View(post);
        }

        [HttpPost]
        public JsonResult AddComment(int postId, string text)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = User.FindFirstValue(ClaimTypes.Name);
            var avatar = User.FindFirstValue(ClaimTypes.UserData);

            var entity = new Comment
            {
                PostId = postId,
                Text = text,
                PublishedOn = DateTime.Now,
                UserId = int.Parse(userId ?? "")
            };

            _commentRepository.CreateComment(entity);

            return Json(new
            {
                username,
                text,
                entity.PublishedOn,
                avatar
            });
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(PostCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                _postRepository.CreatePost(
                    new Post
                    {
                        Title = model.Title,
                        Content = model.Content,
                        Description = model.Description,
                        Url = model.Url,
                        UserId = int.Parse(userId ?? ""),
                        PublishedOn = DateTime.Now,
                        Image = "1.png",
                        IsActive = false
                    }
                );

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> List()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
            var role = User.FindFirstValue(ClaimTypes.Role);

            var posts = _postRepository.Posts;

            if (string.IsNullOrEmpty(role))
            {
                posts = posts.Where(i => i.UserId == userId);
            }

            return View(await posts.ToListAsync());
        }

        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _postRepository.Posts.FirstOrDefault(x => x.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(new PostCreateViewModel
            {
                PostId = post.PostId,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
                Url = post.Url,
                IsActive = post.IsActive
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(PostCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entityToUpdate = new Post
                {
                    PostId = model.PostId,
                    Title = model.Title,
                    Description = model.Description,
                    Content = model.Content,
                    Url = model.Url
                };

                if (User.FindFirstValue(ClaimTypes.Role) == "admin")
                {
                    entityToUpdate.IsActive = model.IsActive;
                }

                _postRepository.EditPost(entityToUpdate);
                return RedirectToAction("List");
            }

            return View(model);
        }
    }
}


/*using System.Security.Claims;
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
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = User.FindFirstValue(ClaimTypes.Name);
             var avatar = User.FindFirstValue(ClaimTypes.UserData);

            var entity = new Comment
            {
                Text = Text,
                PublishedOn = DateTime.Now,
                PostId = PostId,
                UserId = int.Parse(UserId ?? "")
                //User = new User { UserName = UserName, Image = "pp.png" }
            };
            
            _commentRepository.CreateComment(entity);
            
            return Json(new
            {
                username,
                Text,
                entity.PublishedOn,
                avatar       
            });
        }


    }
}*/