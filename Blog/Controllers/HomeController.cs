using System.Diagnostics;
using Blog.Models;
using Blog.Services;
using Microsoft.AspNetCore.Mvc;
using Blog.ViewModels;
namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IBlogService _blogService;

        public HomeController(IBlogService blogService, ILogger<HomeController> logger)
        {
            _blogService = blogService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _blogService.GetAllPostsAsync();

            var postViewModels = posts.Select(p => new PostViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Subtitle = p.Subtitle,
                Content = p.Content,
                ImagePath = p.ImagePath,
                Author = p.Author,
                IsPublished = p.IsPublished,
                PublishedDate = p.PublishedDate
            }).ToList();

            var viewModel = new PostListViewModel
            {
                Posts = postViewModels
            };

            return View(viewModel);
        }
        public async Task<IActionResult> ListOfBlogs()
        {
            var blogs = await _blogService.GetAllPostsAsync();
            return View(blogs);
        }






        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
