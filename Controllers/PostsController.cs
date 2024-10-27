using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult>Details(string url){
            return View(await _postRepository.Posts.Include(x=>x.Comment).ThenInclude(x=>x.User).FirstOrDefaultAsync(p=>p.Url == url));
        }

    }
}