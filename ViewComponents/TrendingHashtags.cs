using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Data.Abstract;

namespace SocialMediaApp.ViewComponents{
    public class TrendingHashtags: ViewComponent{
        private IPostRepository _postRepository;
        public TrendingHashtags(IPostRepository postRepository){
            _postRepository = postRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var mostCommentedHashtags = await _postRepository.GetMostCommentedHashtagsAsync(5);
            return View(mostCommentedHashtags);
        }

    }
}