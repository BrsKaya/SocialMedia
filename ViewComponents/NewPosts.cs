using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Data.Abstract;

namespace SocialMediaApp.ViewComponents{
    public class NewPosts : ViewComponent{
        private IPostRepository _postRepository;
        public NewPosts(IPostRepository postRepository){
            _postRepository = postRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(){
            return View(await _postRepository.Posts.OrderByDescending(p=>p.PublishedOn).Take(5).ToListAsync());
        }

    }
}