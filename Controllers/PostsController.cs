using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Data.Abstract;
using SocialMediaApp.Data.EfCore;
using SocialMediaApp.Models;

namespace SocialMediaApp.Controllers{
    public class PostsController : Controller{
        private IPostRepository _postRepository;
        public PostsController(IPostRepository postRepository){
            _postRepository = postRepository;
        }
        public IActionResult Index(){
            return View(
                new PostsViewModel{
                    Posts = _postRepository.Posts.ToList()
                }
            );
        }

    }
}