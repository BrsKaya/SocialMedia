using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Data.Abstract;
using SocialMediaApp.Data.EfCore;

namespace SocialMediaApp.Controllers{
    public class PostsController : Controller{
        private IPostRepository _repository;
        public PostsController(IPostRepository repository){
            _repository = repository;
        }
        public IActionResult Index(){
            return View(_repository.Posts.ToList());
        }

    }
}